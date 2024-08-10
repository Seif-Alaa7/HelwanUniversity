using Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace HelwanUniversity.Areas.Students.Controllers
{
    [Area("Students")]
    public class StudentController : Controller
    {
        private readonly IStudentRepository studentRepository;
        private readonly IDepartmentRepository departmentRepository;
        private readonly IFacultyRepository faculty;
        private readonly IUniversityRepository universityRepository;
        private readonly IAcademicRecordsRepository academicRecordsRepository;  

        public StudentController(IStudentRepository studentRepository , IDepartmentRepository departmentRepository
            ,IFacultyRepository faculty, IUniversityRepository universityRepository,IAcademicRecordsRepository academicRecordsRepository)
        {
            this.studentRepository = studentRepository;
            this.departmentRepository = departmentRepository;
            this.faculty = faculty;
            this.universityRepository = universityRepository;
            this.academicRecordsRepository = academicRecordsRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details(int id)
        {
            var studentDatails = studentRepository.GetOne(id);
            var department = departmentRepository.DepartmentByStudent(id);

            if (department != null)
            {
                var facultyData = faculty.FacultyByDepartment(department.Id);
                ViewData["Faculty"] = facultyData;
            }
            else
            {
                ViewData["Faculty"] = null;
            }
            ViewData["FormBifurcation"] = universityRepository.Get().GoogleForm;
            return View(studentDatails);
        }
        public IActionResult StudentsByDepartment(int id)
        {
            var students = studentRepository.GetStudents(id).ToList();

            ViewBag.Records = academicRecordsRepository.GetLevelANDSemester(students);
            ViewData["DepartmentName"] = departmentRepository.GetOne(id)?.Name;

            return View(students);
        }

    }
}
