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
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return Content("File not selected.");
            }

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream())
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            return Content($"File uploaded successfully: {uploadResult.SecureUrl}");
        }
    }
}
