using Microsoft.AspNetCore.Mvc;

namespace HelwanUniversity.Areas.Admin.Controllers
{
    public class StudentSubjectsController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
