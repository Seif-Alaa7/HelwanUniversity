using Microsoft.AspNetCore.Mvc;

namespace HelwanUniversity.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
