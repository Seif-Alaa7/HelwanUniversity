using Microsoft.AspNetCore.Mvc;

namespace HelwanUniversity.Controllers
{
    public class SubjectController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
