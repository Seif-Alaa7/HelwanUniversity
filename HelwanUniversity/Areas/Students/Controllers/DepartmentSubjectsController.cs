using Microsoft.AspNetCore.Mvc;

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
