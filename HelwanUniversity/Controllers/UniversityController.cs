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
        private readonly Cloudinary _cloudinary;

        public UniversityController(IUniversityRepository universityRepository, Cloudinary cloudinary)
        {
            this.universityRepository = universityRepository;
            this._cloudinary = cloudinary;
        }
        public IActionResult Index()
        {
            return View(); //Done by Home Controller(Index) : Home Page
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
                ViewCount = university.ViewCount,
            };

            return View(universityVM);

        }
        public async Task<IActionResult> SaveUpdateAsync(UniversityVM newUniVm)
        {
            if (!ModelState.IsValid)
            {
                return View("Update", newUniVm);
            }

            newUniVm.Logo = await UploadImageAsync(newUniVm.LogoFile, newUniVm.Logo, "An error occurred while uploading the logo. Please try again.");
            newUniVm.MainPicture = await UploadImageAsync(newUniVm.MainPictureFile, newUniVm.MainPicture, "An error occurred while uploading the photo. Please try again.");

            if (newUniVm.Logo == null || newUniVm.MainPicture == null)
            {
                return View("Update", newUniVm);
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
            uni.ViewCount = newUniVm.ViewCount;

            universityRepository.Update(uni);
            universityRepository.Save();

            return RedirectToAction("Index", "Home");
        }

        private async Task<string> UploadImageAsync(IFormFile file, string currentUrl, string errorMessage)
        {
            if (file != null && file.Length > 0)
            {
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, file.OpenReadStream())
                };
                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return uploadResult.SecureUrl.AbsoluteUri;
                }
                ModelState.AddModelError("", errorMessage);
            }
            return currentUrl;
        }
    }
}
