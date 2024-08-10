using Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace HelwanUniversity.Areas.Students.Controllers
{
    [Area("Students")]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository departmentRepository;
        private readonly IDepartmentSubjectsRepository departmentSubjectsRepository;
        private readonly IHighBoardRepository highBoardRepository;
        private readonly IDoctorRepository doctorRepository;
        public DepartmentController(IDepartmentRepository departmentRepository, IDepartmentSubjectsRepository departmentSubjectsRepository,
            IHighBoardRepository highBoardRepository,IDoctorRepository doctorRepository)
        {
            this.departmentRepository = departmentRepository;
            this.departmentSubjectsRepository = departmentSubjectsRepository;
            this.highBoardRepository = highBoardRepository;
            this.doctorRepository = doctorRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details(int id)
        {
            var Department = departmentRepository.GetOne(id);

            ViewData["Head"] = highBoardRepository.GetName(Department.HeadId);
            ViewBag.Subjects = departmentSubjectsRepository.subjectsByDepartment(id);
            ViewData["Students"] = departmentRepository.GetStudentCount(id);

            ViewBag.StudentsBySubject = departmentSubjectsRepository.StudentCounts(ViewBag.Subjects);
            ViewBag.DoctorNames = doctorRepository.GetName(ViewBag.Subjects);

            return View(Department);
        }
    }
}
