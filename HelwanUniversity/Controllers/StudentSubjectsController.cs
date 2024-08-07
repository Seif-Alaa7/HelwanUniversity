using Data;
using Data.Repository;
using Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Enums;
using System.Data;
using ViewModels;

namespace HelwanUniversity.Controllers
{
    public class StudentSubjectsController : Controller
    {
        private readonly IStudentSubjectsRepository studentSubjectsRepository;
        private readonly IAcademicRecordsRepository academicRecordsRepository;
        private readonly ISubjectRepository subjectRepository;
        private readonly IStudentRepository studentRepository;
        private readonly IDoctorRepository doctorRepository;
        private readonly IDepartmentRepository departmentRepository;
        private readonly ApplicationDbContext Context;
        public StudentSubjectsController(IStudentSubjectsRepository studentSubjectsRepository, ApplicationDbContext context,
            IAcademicRecordsRepository academicRecordsRepository, ISubjectRepository subjectRepository, IStudentRepository studentRepository,
            IDoctorRepository doctorRepository, IDepartmentRepository departmentRepository)
        {
            this.studentSubjectsRepository = studentSubjectsRepository;
            this.Context = context;
            this.academicRecordsRepository = academicRecordsRepository;
            this.subjectRepository = subjectRepository;
            this.studentRepository = studentRepository;
            this.doctorRepository = doctorRepository;
            this.departmentRepository = departmentRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddSubject(int studentId, int subjectId)
        {
            var exists = studentSubjectsRepository.Exist(studentId, subjectId);
            if (exists)
            {
                ModelState.AddModelError("", "This subject is already registered.");
                return RedirectToAction("DisplaySubjects", "DepartmentSubjects", new { Studentid = studentId });
            }

            var studentSubject = new StudentSubjects
            {
                StudentId = studentId,
                SubjectId = subjectId,
                Degree = 0,
                Grade = Grade.F
            };

            // Add Student Subject
            studentSubjectsRepository.Add(studentSubject);
            studentSubjectsRepository.Save();

            // Calculate Academic Records
            UpdateAcademicRecords(studentId);

            return RedirectToAction("SubjectRegsitered", new { id = studentId });
        }
        public IActionResult DeleteSubject(int studentId, int subjectId)
        {
            var links = studentSubjectsRepository.FindStudent(studentId);
            if (links.Count() == 1)
            {
                academicRecordsRepository.DeleteByStudent(studentId);
                studentRepository.Delete(studentId);

                studentRepository.Save();

                foreach (var model in links)
                {
                    studentSubjectsRepository.Delete(model);
                    studentSubjectsRepository.Save();
                }

                // Update Academic Records
                UpdateAcademicRecords(studentId);

                return RedirectToAction("SubjectRegsitered", new { id = studentId });
            }
            else
            {
                var link = studentSubjectsRepository.GetOne(studentId, subjectId);
                if (link == null)
                {
                    ModelState.AddModelError("", "you Can't Delete Subject because you Did not Add");
                    return RedirectToAction("DisplaySubjects", "DepartmentSubjects", new { Studentid = studentId });
                }
                else
                {
                    studentSubjectsRepository.Delete(link);
                    studentSubjectsRepository.Save();

                    // Update Academic Records
                    UpdateAcademicRecords(studentId);
                }
            }
            return RedirectToAction("SubjectRegsitered", new { id = studentId });
        }
        public IActionResult SubjectRegsitered(int id)
        {
            var Subjects = subjectRepository.GetSubjects(id);

            var doctorDictionary = doctorRepository.GetAll()
                            .ToDictionary(x => x.Id, x => x.Name);

            var doctorNames = new Dictionary<int, string>();
            foreach (var subject in Subjects)
            {
                string doctorName;
                if (doctorDictionary.TryGetValue(subject.DoctorId, out doctorName))
                {
                    doctorNames[subject.DoctorId] = doctorName;
                }
            }
            var department = departmentRepository.DepartmentByStudent(id);
            ViewData["departmentName"] = department.Name;
            ViewBag.DoctorNames = doctorNames;
            return View(Subjects);
        }
        public IActionResult DisplayDegrees(int id)
        {
            var studentSubjects = studentSubjectsRepository.FindStudent(id);
            var Subjects = subjectRepository.GetSubjects(id);

            var SubjectsDictionary = subjectRepository.GetAll()
                            .ToDictionary(x => x.Id, x => x.Name);

            var SubjectNames = new Dictionary<int, string>();
            foreach (var subject in Subjects)
            {
                string SubjectName;
                if (SubjectsDictionary.TryGetValue(subject.Id, out SubjectName))
                {
                    SubjectNames[subject.Id] = SubjectName;
                }
            }
            ViewBag.SubjectNames = SubjectNames;

            var AcademicRecords = Context.academicRecords.FirstOrDefault(x => x.StudentId == id);

            ViewData["AcademicRecords"] = AcademicRecords;

            return View(studentSubjects);
        }
        public IActionResult AddDegree(int Studentid, int Subjectid)
        {
            var ModelVM = new StudentSubjectsVM()
            {
                StudentId = Studentid,
                SubjectId = Subjectid
            };
            return View(ModelVM);


        }
        public IActionResult SaveAdd(StudentSubjectsVM modelVM)
        {
            var StudentSubject = studentSubjectsRepository.GetOne(modelVM.StudentId, modelVM.SubjectId);
            StudentSubject.Degree = modelVM.Degree;
            StudentSubject.Grade = studentSubjectsRepository.CalculateGrade(modelVM.Degree);

            studentSubjectsRepository.Update(StudentSubject);
            studentSubjectsRepository.Save();

            // Update Academic Records
            UpdateAcademicRecords(modelVM.StudentId);

            return RedirectToAction("DisplayDegrees", new { id = modelVM.StudentId });
        }
        private void UpdateAcademicRecords(int studentId)
        {
            var creditHours = studentSubjectsRepository.CalculateCreditHours(studentId);
            var semester = studentSubjectsRepository.Calculatesemester(creditHours);
            var level = studentSubjectsRepository.CalculateLevel(creditHours);
            var totalPoints = studentSubjectsRepository.CalculateTotalPoints(studentId);
            var semesterPoints = studentSubjectsRepository.CalculateSemesterPoints(studentId, semester);
            var recordedHours = studentSubjectsRepository.CalculateRecordedHours(studentId, semester);
            var totalHours = studentSubjectsRepository.CalculateTotalHours(studentId);
            var gpaSemester = academicRecordsRepository.CalculateGpaSemester(studentId, semester);
            var gpaTotal = academicRecordsRepository.CalculateGPATotal(studentId);

            var academicRecords = Context.academicRecords.FirstOrDefault(x => x.StudentId == studentId);

            if (academicRecords != null)
            {
                academicRecords.CreditHours = creditHours;
                academicRecords.Semester = semester;
                academicRecords.Level = level;
                academicRecords.TotalPoints = totalPoints;
                academicRecords.SemesterPoints = semesterPoints;
                academicRecords.RecordedHours = recordedHours;
                academicRecords.TotalHours = totalHours;
                academicRecords.GPASemester = gpaSemester;
                academicRecords.GPATotal = gpaTotal;

                academicRecordsRepository.Update(academicRecords);
                academicRecordsRepository.Save();
            }
        }
    }
}
