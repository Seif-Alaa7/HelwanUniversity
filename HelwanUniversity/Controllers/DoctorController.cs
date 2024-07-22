using Microsoft.AspNetCore.Mvc;

namespace HelwanUniversity.Controllers
{
    public class DoctorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
