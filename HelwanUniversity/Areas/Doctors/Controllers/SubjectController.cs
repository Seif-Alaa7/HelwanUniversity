using Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;
using ViewModels;

namespace HelwanUniversity.Areas.Doctors.Controllers
{
    [Area("Doctors")]
    public class SubjectController : Controller
    {
        private readonly ISubjectRepository subjectRepository;
        private readonly IDoctorRepository doctorRepository;
        private readonly IDepartmentRepository departmentRepository;
        public SubjectController(ISubjectRepository subject,IDoctorRepository doctorRepository,
            IUniFileRepository uniFileRepository,IDepartmentRepository department)
        {
            this.subjectRepository = subject;
            this.doctorRepository = doctorRepository;
            this.departmentRepository = department;
        }
        public IActionResult Index()
        {
            return View();
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
