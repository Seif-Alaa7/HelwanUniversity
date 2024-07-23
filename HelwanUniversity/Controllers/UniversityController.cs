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
            var UNI = universityRepository.Get();

            //Mapping            
            var UNIvm = new UniversityVM()
            {
                Name = UNI.Name,
                Logo = UNI.Logo,
                MainPicture = UNI.MainPicture,
                Description = UNI.Description,
                FacebookPage = UNI.FacebookPage,
                LinkedInPage = UNI.LinkedInPage,
                MainPage = UNI.MainPage,
                ContactMail = UNI.ContactMail,
                ViewCount = UNI.ViewCount,
            };
            return View(UNIvm);
        }
        public async Task<IActionResult> SaveUpdateAsync(UniversityVM New_UNIvm)
        {
            if (ModelState.IsValid)
            {
                //LOGO
                if (New_UNIvm.LogoFile != null && New_UNIvm.LogoFile.Length > 0)
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(New_UNIvm.LogoFile.FileName,New_UNIvm.LogoFile.OpenReadStream())
                    };
                    var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                    New_UNIvm.Logo = uploadResult.SecureUrl.AbsoluteUri;
                };

                if (New_UNIvm.MainPictureFile != null && New_UNIvm.MainPictureFile.Length > 0)
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(New_UNIvm.MainPictureFile.FileName, New_UNIvm.MainPictureFile.OpenReadStream())
                    };
                    var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                    New_UNIvm.MainPicture = uploadResult.SecureUrl.AbsoluteUri;
                };

                var UNI = universityRepository.Get();

                //Update Changes
                UNI.Name = New_UNIvm.Name;
                UNI.Logo = New_UNIvm.Logo;
                UNI.MainPicture = New_UNIvm.MainPicture;
                UNI.Description = New_UNIvm.Description;
                UNI.FacebookPage = New_UNIvm.FacebookPage;
                UNI.LinkedInPage = New_UNIvm.LinkedInPage;
                UNI.MainPage = New_UNIvm.MainPage;
                UNI.ContactMail = New_UNIvm.ContactMail;
                UNI.ViewCount = New_UNIvm.ViewCount;

                universityRepository.Update(UNI);

                return RedirectToAction("Index", "Home");
            }
            return View("Update",New_UNIvm);
        }
    }
}
