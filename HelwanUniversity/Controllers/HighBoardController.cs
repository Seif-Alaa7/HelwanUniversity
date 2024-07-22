using Microsoft.AspNetCore.Mvc;

namespace HelwanUniversity.Controllers
{
    public class HighBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
