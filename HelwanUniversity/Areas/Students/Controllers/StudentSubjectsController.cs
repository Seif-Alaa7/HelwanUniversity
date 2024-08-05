using Microsoft.AspNetCore.Mvc;

namespace HelwanUniversity.Areas.Students.Controllers
{
    public class StudentSubjectsController : Controller
    {
        [Area("Students")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
