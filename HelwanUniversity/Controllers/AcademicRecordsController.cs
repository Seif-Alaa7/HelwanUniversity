using Microsoft.AspNetCore.Mvc;

namespace HelwanUniversity.Controllers
{
    public class AcademicRecordsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
