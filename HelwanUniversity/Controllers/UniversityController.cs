using Microsoft.AspNetCore.Mvc;

namespace HelwanUniversity.Controllers
{
    public class UniversityController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
