using Microsoft.AspNetCore.Mvc;

namespace HelwanUniversity.Controllers
{
    public class FacultyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
