using Data.Repository;
using Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Options;
using Models;
using Models.Enums;
using Newtonsoft.Json.Linq;
using ViewModels;

namespace HelwanUniversity.Controllers
{
    public class SubjectController : Controller
    {
        private readonly ISubjectRepository subjectRepository;
        private readonly IDoctorRepository doctorRepository;
        private readonly IDepartmentRepository departmentRepository;
        private readonly IDepartmentSubjectsRepository departmentSubjectsRepository;
        private readonly IStudentRepository studentRepository;
        private readonly IAcademicRecordsRepository academicRecordsRepository;
        public SubjectController(ISubjectRepository subject,IDoctorRepository doctorRepository,
            IUniFileRepository uniFileRepository,IDepartmentRepository department,
            IDepartmentSubjectsRepository department1, IStudentRepository student,IAcademicRecordsRepository academicRecords)
        {
            this.subjectRepository = subject;
            this.doctorRepository = doctorRepository;
            this.departmentRepository = department;
            this.departmentSubjectsRepository = department1;
            this.studentRepository = student;
            this.academicRecordsRepository = academicRecords;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Edit(int id, int departmentId)
        {
            var subject = subjectRepository.GetOne(id);


            var subjectVM = new SubjectVM()
            {
                Id = id,
                Name = subject.Name,
                Level = subject.Level,  
                Semester = subject.Semester,
                DoctorId = subject.DoctorId,
                SubjectHours = subject.SubjectHours,
                StudentsAllowed = subject.StudentsAllowed,
                summerStatus = subject.summerStatus,
                subjectType = subject.subjectType,  
                Salary = subject.Salary,
                departmentId = departmentId,
                OriginalDepartmentId = departmentId
            };
            var departments = departmentRepository.Select();

            foreach (var department in departments)
            {
                if (department.Value == departmentId.ToString())
                {
                    department.Selected = true;
                }
            }

            ViewBag.Departments = departments;

            ViewData["DoctorNames"] = doctorRepository.Select();

            return View(subjectVM);
        }
        public IActionResult SaveEdit(SubjectVM model)
        {
            var subject = subjectRepository.GetOne(model.Id);
            if (model.Name != subject.Name)
            {
                var exist = subjectRepository.ExistSubject(model.Name);
                if (exist)
                {
                    ModelState.AddModelError("Name", "This Name is Already Exist");
                    return View("Edit", model);
                }
            }
            subject.Name = model.Name;
            subject.Level = model.Level;
            subject.Semester = model.Semester;
            subject.StudentsAllowed = model.StudentsAllowed;
            subject.summerStatus = model.summerStatus;
            subject.subjectType = model.subjectType;
            subject.Salary = model.Salary;

            subjectRepository.Update(subject);
            subjectRepository.Save();

            var department = departmentRepository.GetOne(model.departmentId);
            var departmentOld = departmentRepository.GetOne(model.OriginalDepartmentId);

            var departmentSubjectOld = new DepartmentSubjects()
            {
                SubjectId = subject.Id,
                DepartmentId = departmentOld.Id,
            };

            var departmentSubject = new DepartmentSubjects()
            {
                DepartmentId = department.Id,
                SubjectId = subject.Id,
            };

            var exists = departmentSubjectsRepository.Exist(departmentSubject);
            if (exists)
            {
                return RedirectToAction("Details", "Department", new { id = departmentSubject.DepartmentId });
            }
            departmentSubjectsRepository.Delete(departmentSubjectOld);
            departmentSubjectsRepository.Add(departmentSubject);
            departmentRepository.Save();

            return RedirectToAction("Details", "Department", new { id = departmentSubject.DepartmentId });
        }
        public IActionResult DeleteForever(int id,int Departmentid)
        {
            var Departments = departmentSubjectsRepository.SubjectDepartments(id);
            foreach (var department in Departments)
            {
                departmentSubjectsRepository.Delete(department);
            }
            var subject = subjectRepository.GetOne(id);
            subjectRepository.Delete(subject);
            departmentSubjectsRepository.Save();

            return RedirectToAction("Details", "Department", new { id = Departmentid });
        }
        public IActionResult ResultsRegisteration(int id)
        {
            var Subjects = subjectRepository.GetSubjects(id);
            ViewBag.DoctorNames = doctorRepository.GetName(Subjects);
            var department = departmentRepository.DepartmentByStudent(id);
            ViewData["StudentId"] = id;
            ViewData["departmentName"] = department.Name;

            return View(Subjects);
        }
    }
}
