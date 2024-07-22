using Microsoft.AspNetCore.Mvc;

namespace HelwanUniversity.Controllers
{
    public class UniFileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
