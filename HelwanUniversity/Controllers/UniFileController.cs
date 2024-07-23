using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;
using ViewModels.UniFileVMs;
using Data.Repository;

namespace HelwanUniversity.Controllers
{
    public class UniFileController : Controller
    {
        private readonly IUniFileRepository uniFileRepository;
        private readonly Cloudinary _cloudinary;

        public UniFileController(IUniFileRepository uniFileRepository, Cloudinary cloudinary)
        {
            this.uniFileRepository = uniFileRepository;
            this._cloudinary = cloudinary;

        }
        //Delete image & Video
        public IActionResult News()
        {
            var videos = uniFileRepository.GetAllVideos();
            return View(videos);
        }
        public IActionResult DisplayVideo(int id)
        {
            var video = uniFileRepository.GetFile(id);
            return View(video);
        }
        [HttpPost]
        public IActionResult AddVideo()
        {
            return View();
        }
        public IActionResult SaveVideo(UniFileVM uniFileVM)
        {
            if (ModelState.IsValid)
            {
                var file = new UniFile()
                {
                    File = uniFileVM.File,
                    ContentType = uniFileVM.ContentType,
                };
                uniFileRepository.Add(file);

                return RedirectToAction("News");
            }
            return View("AddVideo",uniFileVM);
        }
        [HttpPost]
        public IActionResult AddImage()
        {
            return View();
        }
        public async Task<IActionResult> SaveImgAsync(UniFileVM uniFileVM)
        {
            if (ModelState.IsValid)
            {
                if (uniFileVM.ImgPath != null && uniFileVM.ImgPath.Length > 0)
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(uniFileVM.ImgPath.FileName, uniFileVM.ImgPath.OpenReadStream())
                    };

                    var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                    uniFileVM.File = uploadResult.SecureUrl.AbsoluteUri;
                };
                var file = new UniFile()
                {
                    File = uniFileVM.File,
                    ContentType = uniFileVM.ContentType,
                };
                uniFileRepository.Add(file);

                return RedirectToAction("Index", "Home");
            }
            return View("AddImage", uniFileVM);
        }
        [HttpPost]
        public IActionResult UpdateVideo(int id)
        {
            var Video = uniFileRepository.GetFile(id);

            //Mapping
            var VideoVM = new UniFileVM2()
            {
                Id = Video.Id,
                File = Video.File,
                ContentType = Video.ContentType
            };
            return View(VideoVM);
        }
        public IActionResult SaveUpdateVideo(UniFileVM2 New_VideoVM)
        {
            if (ModelState.IsValid)
            {
                var video = uniFileRepository.GetFile(New_VideoVM.Id);

                //Update Changes
                video.File = New_VideoVM.File;
                uniFileRepository.Update(video);

                return RedirectToAction("News");
            }
            return View("UpdateVideo",New_VideoVM);
        }
        [HttpPost]
        public IActionResult UpdateImage(int id)
        {
            var Img = uniFileRepository.GetFile(id);

            //Mapping
            var ImgVM = new UniFileVM2()
            {
                Id = Img.Id,
                File = Img.File,
                ContentType = Img.ContentType
            };
            return View(ImgVM);
        }
        public async Task<IActionResult> SaveUpdateImageAsync(UniFileVM2 New_ImgVM)
        {
            if (ModelState.IsValid)
            {
                if (New_ImgVM.ImgPath != null && New_ImgVM.ImgPath.Length > 0)
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(New_ImgVM.ImgPath.FileName, New_ImgVM.ImgPath.OpenReadStream())
                    };
                    var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                    New_ImgVM.File = uploadResult.SecureUrl.AbsoluteUri;
                };
                var IMG = uniFileRepository.GetFile(New_ImgVM.Id);

                //Update Change
                IMG.File = New_ImgVM.File;
                uniFileRepository.Update(IMG);

                return RedirectToAction("Index", "Home");
            }
            return View("UpdateImage",New_ImgVM);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var File = uniFileRepository.GetFile(id);
            if (ModelState.IsValid)
            {
                return View(File);
            }
            return View(File);
        }
        public IActionResult ConfirmDelete(int id)
        {
            var File = uniFileRepository.GetFile(id);
            var FileType = File.ContentType;

            uniFileRepository.Delete(File);
            if(FileType == Models.Enums.Filetype.IMG)
            {
                return RedirectToAction("Index","Home"); 
            }
            else
            {
                return RedirectToAction("News");
            }
        }
    }
}
