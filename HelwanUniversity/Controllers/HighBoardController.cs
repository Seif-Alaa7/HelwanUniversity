using Data.Repository;
using Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Numerics;
using ViewModels;

namespace HelwanUniversity.Controllers
{
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
        public IActionResult Edit(int id)
        {
            var highboard = highBoardRepository.GetOne(id);
            var highboardVM = new HighBoardVM
            {
                Id = id,
                Name = highboard.Name,
                Description = highboard.Description,
                JobTitle = highboard.JobTitle,
                Picture = highboard.Picture
            };
            return View(highboardVM);
        }
        [HttpPost]
        public IActionResult SaveEdit(HighBoardVM highBoardVM)
        {
            var highboard = highBoardRepository.GetOne(highBoardVM.Id);

            highboard.Id = highBoardVM.Id;
            highboard.Description = highBoardVM.Description;
            highboard.Name = highBoardVM.Name;
            highboard.JobTitle = highBoardVM.JobTitle;
            highboard.Picture = highBoardVM.Picture;

            highBoardRepository.Update(highboard);
            highBoardRepository.Save();
            return RedirectToAction("Details", new { id = highboard.Id });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            highBoardRepository.Delete(id);
            highBoardRepository.Save();

            return RedirectToAction("Index" , "University");
        }
    }
}
