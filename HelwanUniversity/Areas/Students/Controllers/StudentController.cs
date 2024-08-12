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

        public StudentController(IStudentRepository studentRepository , IDepartmentRepository departmentRepository
            ,IFacultyRepository faculty, IUniversityRepository universityRepository)
        {
            this.studentRepository = studentRepository;
            this.departmentRepository = departmentRepository;
            this.faculty = faculty;
            this.universityRepository = universityRepository;
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
    }
}
