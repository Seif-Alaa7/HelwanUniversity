using Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace HelwanUniversity.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository departmentRepository;
        private readonly IHighBoardRepository highBoardRepository;
        private readonly IDepartmentSubjectsRepository departmentSubjectsRepository;
        private readonly IStudentSubjectsRepository studentSubjectsRepository;
        private readonly IDoctorRepository doctorRepository;
        public DepartmentController(IDepartmentRepository departmentRepository,IHighBoardRepository highBoardRepository,
            IDepartmentSubjectsRepository departmentSubjects,IStudentSubjectsRepository studentSubjects,IDoctorRepository doctorRepository)
        {
            this.departmentRepository = departmentRepository;
            this.highBoardRepository = highBoardRepository;
            this.departmentSubjectsRepository = departmentSubjects;
            this.studentSubjectsRepository = studentSubjects;
            this.doctorRepository = doctorRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details(int id)
        {
            var Department = departmentRepository.GetOne(id);
            var doctorDictionary = doctorRepository.GetAll()
                               .ToDictionary(x => x.Id, x => x.Name);
            ViewData["Head"] = highBoardRepository.GetOne(Department.HeadId)?.Name;
            ViewBag.Subjects = departmentSubjectsRepository.subjectsByDepartment(id);
            ViewData["Students"] = departmentRepository.GetStudentCount(id);

          var studentsBySubject = new Dictionary<int, int>();
            foreach (var subject in ViewBag.Subjects)
            {
                int studentCount = studentSubjectsRepository.StudentBySubject(subject.Subject.Id).Count;
                studentsBySubject[subject.Subject.Id] = studentCount;
            }
            ViewBag.StudentsBySubject = studentsBySubject;

            var doctorNames = new Dictionary<int, string>();
            foreach (var subject in ViewBag.Subjects)
            {
                string doctorName;
                if (doctorDictionary.TryGetValue(subject.Subject.DoctorId, out doctorName))
                {
                    doctorNames[subject.Subject.DoctorId] = doctorName;
                }
            }
            ViewBag.DoctorNames = doctorNames;

            return View(Department);
        }
    }
}
