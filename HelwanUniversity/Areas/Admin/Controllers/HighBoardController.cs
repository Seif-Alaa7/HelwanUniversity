using Data.Repository.IRepository;
using HelwanUniversity.Services;
using Microsoft.AspNetCore.Mvc;
using ViewModels;

namespace HelwanUniversity.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HighBoardController : Controller
    {
        private readonly IHighBoardRepository highBoardRepository;
        private readonly ICloudinaryService cloudinaryService;

        public HighBoardController(IHighBoardRepository highBoardRepository, ICloudinaryService cloudinaryService)
        {
            this.highBoardRepository = highBoardRepository;
            this.cloudinaryService = cloudinaryService;
        }
        public IActionResult Index()
        {
            var Highboards = highBoardRepository.GetAll();
            ViewData["President"] = highBoardRepository.GetPresident();
            return View(Highboards);
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
                highBoardVM.Picture = await cloudinaryService.UploadFile(highBoardVM.FormFile, highboard.Picture, "An error occurred while uploading the photo. Please try again.");
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
            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            highBoardRepository.Delete(id);
            highBoardRepository.Save();

            return RedirectToAction("Index");
        }
        public IActionResult DisplayDean()
        {
            return View();
        }
        public IActionResult DisplayHead()
        {
            return View();
        }
    }
}
