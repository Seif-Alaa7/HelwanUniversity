using Microsoft.AspNetCore.Mvc;

namespace HelwanUniversity.Areas.Doctors.Controllers
{
    public class StudentSubjectsController : Controller
    {
        [Area("Doctors")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
