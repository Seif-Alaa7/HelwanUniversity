using Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace HelwanUniversity.Areas.Students.Controllers
{
    [Area("Students")]
    public class HighBoardController : Controller
    {
        private readonly IHighBoardRepository highBoardRepository;

        public HighBoardController(IHighBoardRepository highBoardRepository)
        {
            this.highBoardRepository = highBoardRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details(int id)
        {
            var HighboardDatails = highBoardRepository.GetOne(id);
            return View(HighboardDatails);
        }
    }
}
