using Microsoft.AspNetCore.Mvc;

namespace HelwanUniversity.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AcademicRecordsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
