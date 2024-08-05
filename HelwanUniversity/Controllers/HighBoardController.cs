using Data.Repository;
using Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System.Numerics;
using ViewModels;

namespace HelwanUniversity.Controllers
{
    public class HighBoardController : Controller
    {
        private readonly IHighBoardRepository highBoardRepository;
        private readonly CloudinaryController cloudinaryController;

        public HighBoardController(IHighBoardRepository highBoardRepository, CloudinaryController cloudinaryController)
        {
            this.highBoardRepository = highBoardRepository;
            this.cloudinaryController = cloudinaryController;
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
        public async Task<IActionResult> SaveEdit(HighBoardVM highBoardVM)
        {
            var highboard = highBoardRepository.GetOne(highBoardVM.Id);

            if (highboard.Name != highBoardVM.Name)
            {
                var exist = highBoardRepository.ExistName(highBoardVM.Name);
                if (exist)
                {
                    ModelState.AddModelError("Name", "This Name is already exists");
                    highBoardVM.Picture = highboard.Picture;
                    return View("Edit", highBoardVM);
                }
            }

            if (highboard.JobTitle != highBoardVM.JobTitle)
            {
                if (highBoardVM.JobTitle == Models.Enums.JobTitle.VP_For_AcademicAffairs
                    || highBoardVM.JobTitle == Models.Enums.JobTitle.VP_For_Finance
                    || highBoardVM.JobTitle == Models.Enums.JobTitle.President)
                {
                    var exist = highBoardRepository.ExistJop(highBoardVM.JobTitle);
                    if (exist)
                    {
                        ModelState.AddModelError("JobTitle", "This job is already exists");
                        highBoardVM.Picture = highboard.Picture;
                        return View("Edit", highBoardVM);
                    }
                }
            }

            try
            {
                highBoardVM.Picture = await cloudinaryController.UploadFile(highBoardVM.FormFile, highboard.Picture, "An error occurred while uploading the photo. Please try again.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                highBoardVM.Picture = highboard.Picture;
                return View("Edit", highBoardVM);
            }


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
