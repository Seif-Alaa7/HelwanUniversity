using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using ViewModels;

namespace HelwanUniversity.Controllers
{
    public class UniversityController : Controller
    {
        private readonly IUniversityRepository universityRepository;
        private readonly CloudinaryController _cloudinaryController;

        public UniversityController(IUniversityRepository universityRepository, CloudinaryController _cloudinaryController)
        {
            this.universityRepository = universityRepository;
            this._cloudinaryController = _cloudinaryController;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Update()
        {
            var university = universityRepository.Get();

            // Mapping
            var universityVM = new UniversityVM
            {
                Name = university.Name,
                Logo = university.Logo,
                MainPicture = university.MainPicture,
                Description = university.Description,
                FacebookPage = university.FacebookPage,
                LinkedInPage = university.LinkedInPage,
                MainPage = university.MainPage,
                ContactMail = university.ContactMail,
                HistoricalBackground = university.HistoricalBackground,
                ViewCount = university.ViewCount,
            };
            return View(universityVM);

        }
        public async Task<IActionResult> SaveUpdateAsync(UniversityVM newUniVm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    newUniVm.Logo = await _cloudinaryController.UploadFile(newUniVm.LogoFile, newUniVm.Logo, "An error occurred while uploading the logo. Please try again.");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                try
                {
                    newUniVm.MainPicture = await _cloudinaryController.UploadFile(newUniVm.MainPictureFile, newUniVm.MainPicture, "An error occurred while uploading the photo. Please try again.");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                var uni = universityRepository.Get();

                uni.Name = newUniVm.Name;
                uni.Logo = newUniVm.Logo;
                uni.MainPicture = newUniVm.MainPicture;
                uni.Description = newUniVm.Description;
                uni.FacebookPage = newUniVm.FacebookPage;
                uni.LinkedInPage = newUniVm.LinkedInPage;
                uni.MainPage = newUniVm.MainPage;
                uni.ContactMail = newUniVm.ContactMail;
                uni.HistoricalBackground = newUniVm.HistoricalBackground;
                uni.ViewCount = newUniVm.ViewCount;

                universityRepository.Update(uni);
                universityRepository.Save();

                return RedirectToAction("Index");
            }
            return View("Update", newUniVm);
        }
    }
}
