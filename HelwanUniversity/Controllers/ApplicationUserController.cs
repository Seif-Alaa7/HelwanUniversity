using Microsoft.AspNetCore.Mvc;

namespace HelwanUniversity.Controllers
{
    public class ApplicationUserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
