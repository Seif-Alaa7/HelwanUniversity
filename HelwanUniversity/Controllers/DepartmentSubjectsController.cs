using Microsoft.AspNetCore.Mvc;

namespace HelwanUniversity.Controllers
{
    public class DepartmentSubjectsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
