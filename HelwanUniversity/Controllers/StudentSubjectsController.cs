using Microsoft.AspNetCore.Mvc;

namespace HelwanUniversity.Controllers
{
    public class StudentSubjectsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
