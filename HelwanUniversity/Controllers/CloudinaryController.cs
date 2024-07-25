using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;

namespace HelwanUniversity.Controllers
{
    public class CloudinaryController : Controller
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryController(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<string> UploadFile(IFormFile file, string currentUrl, string errorMessage)
        {
            if (file != null && file.Length > 0)
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, file.OpenReadStream())
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return uploadResult.SecureUrl.AbsoluteUri;
                }
                else
                {
                    throw new Exception(errorMessage);
                }
            }
            return currentUrl;
        }
    }
}
