using Microsoft.AspNetCore.Mvc;

namespace HelwanUniversity.Areas.Doctors.Controllers
{
    [Area("Doctors")]
    public class DepartmentSubjectsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
    }
}
