using Data.Repository;
using Data.Repository.IRepository;
using HelwanUniversity.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using ViewModels;

namespace HelwanUniversity.Areas.Doctors.Controllers
{
    [Area("Doctors")]
    public class StudentController : Controller
    {
        private readonly IStudentRepository studentRepository;
        private readonly IDepartmentRepository departmentRepository;
        private readonly IFacultyRepository faculty;
        private readonly ICloudinaryService cloudinaryService;
        private readonly IUniversityRepository universityRepository;
        private readonly IAcademicRecordsRepository academicRecordsRepository;  
        private readonly IFacultyRepository facultyRepository;
        private readonly IDoctorRepository doctorRepository;
        private readonly IHighBoardRepository highBoardRepository;
        public StudentController(IStudentRepository studentRepository , IDepartmentRepository departmentRepository,IFacultyRepository faculty,
            ICloudinaryService cloudinaryService,IUniversityRepository universityRepository,
            IAcademicRecordsRepository academicRecordsRepository,
            IFacultyRepository facultyRepository,IHighBoardRepository highBoardRepository,IDoctorRepository doctor)
        {
            this.studentRepository = studentRepository;
            this.departmentRepository = departmentRepository;
            this.faculty = faculty;
            this.cloudinaryService = cloudinaryService;
            this.universityRepository = universityRepository;
            this.academicRecordsRepository = academicRecordsRepository;
            this.facultyRepository = facultyRepository;
            this.highBoardRepository = highBoardRepository;
            this.doctorRepository = doctor;
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
            if(universityRepository.Get().GoogleForm != null)
            {
                ViewData["FormBifurcation"] = universityRepository.Get().GoogleForm;
            }
            else
            {
               return NotFound();   
            }
            return View(studentDatails);
        }
        public IActionResult StudentsByDepartment(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var Highboard = highBoardRepository.GetAll().FirstOrDefault(h => h.ApplicationUserId == userId);
            var Doctor = doctorRepository.GetAll().FirstOrDefault(h => h.ApplicationUserId == userId);
            if (Doctor != null)
            {
                ViewData["Doctor"] = Doctor;
            }
            else
            {
                ViewData["Doctor"] = Highboard;
            }
            var students = studentRepository.GetStudents(id).ToList();

            ViewBag.Records = academicRecordsRepository.GetLevelANDSemester(students);
            ViewData["DepartmentName"] = departmentRepository.GetOne(id)?.Name;
            ViewData["FacultyName"] = facultyRepository.FacultyByDepartment(id).Name;

            return View(students);
        }
    }
}
