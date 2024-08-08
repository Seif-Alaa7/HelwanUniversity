using Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace HelwanUniversity.Areas.Students.Controllers
{
    [Area("Students")]
    public class DepartmentSubjectsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
    }
}
