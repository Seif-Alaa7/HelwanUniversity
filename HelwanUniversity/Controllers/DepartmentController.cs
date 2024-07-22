using Microsoft.AspNetCore.Mvc;

namespace HelwanUniversity.Controllers
{
    public class DepartmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
