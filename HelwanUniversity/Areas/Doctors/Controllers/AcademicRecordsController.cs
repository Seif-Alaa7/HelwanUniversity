using Microsoft.AspNetCore.Mvc;

namespace HelwanUniversity.Areas.Doctors.Controllers
{
    [Area("Doctors")]
    public class AcademicRecordsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
