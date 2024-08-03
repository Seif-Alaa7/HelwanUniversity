using Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using ViewModels;

namespace HelwanUniversity.Controllers
{
    public class DepartmentSubjectsController : Controller
    {
        private readonly IDepartmentRepository departmentRepository;
        private readonly ISubjectRepository subjectRepository;
        public DepartmentSubjectsController(IDepartmentRepository department,ISubjectRepository subject)
        {
            this.departmentRepository = department;
            this.subjectRepository = subject;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add(int id) 
        {
            ViewData["DepartId"] = id;
            ViewData["Subjects"] = 
            return View();
        }
        public IActionResult SaveAdd() 
        { 
            return View();
        }
    }
}
