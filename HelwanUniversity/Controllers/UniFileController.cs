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

        //Display image & Video
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
        [HttpGet]
        public IActionResult AddVideo()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SaveVideo(UniFileVM uniFileVM)
        {
            if (ModelState.IsValid)
            {
                var file = new UniFile
                {
                    File = uniFileVM.File,
                    ContentType = uniFileVM.ContentType,
                };
                uniFileRepository.Add(file);
                uniFileRepository.Save();

                return RedirectToAction("News");
            }
            return View("AddVideo",uniFileVM);
        }
        [HttpGet]
        public IActionResult AddImage()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SaveImgAsync(UniFileVM uniFileVM)
        {
            if (ModelState.IsValid)
            {
                if (uniFileVM.ImgPath != null && uniFileVM.ImgPath.Length > 0)
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(uniFileVM.ImgPath.FileName, uniFileVM.ImgPath.OpenReadStream())
                    };

                    var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                    if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        uniFileVM.File = uploadResult.SecureUrl.AbsoluteUri;
                    }
                    else
                    {
                        ModelState.AddModelError("", "An error occurred while uploading the photo. Please try again.");
                        return View("AddImage", uniFileVM);
                    }

                }
                var file = new UniFile
                {
                    File = uniFileVM.File,
                    ContentType = uniFileVM.ContentType,
                };
                uniFileRepository.Add(file);
                uniFileRepository.Save();

                return RedirectToAction("Index", "Home");
            }
            return View("AddImage", uniFileVM);
        }
        [HttpGet]
        public IActionResult UpdateVideo(int id)
        {
            var Video = uniFileRepository.GetFile(id);

            //Mapping
            var VideoVM = new UniFileVM2
            {
                Id = Video.Id,
                File = Video.File,
                ContentType = Video.ContentType
            };
            return View(VideoVM);
        }
        [HttpPost]
        public IActionResult SaveUpdateVideo(UniFileVM2 newVideoVM)
        {
            if (ModelState.IsValid)
            {
                var video = uniFileRepository.GetFile(newVideoVM.Id);

                //Update Changes
                video.File = newVideoVM.File;
                uniFileRepository.Update(video);
                uniFileRepository.Save();

                return RedirectToAction("News");
            }
            return View("UpdateVideo", newVideoVM);
        }
        [HttpGet]
        public IActionResult UpdateImage(int id)
        {
            var Img = uniFileRepository.GetFile(id);

            //Mapping
            var ImgVM = new UniFileVM2
            {
                Id = Img.Id,
                File = Img.File,
                ContentType = Img.ContentType
            };
            return View(ImgVM);
        }
        [HttpPost]
        public async Task<IActionResult> SaveUpdateImageAsync(UniFileVM2 newImgVM)
        {
            if (ModelState.IsValid)
            {
                if (newImgVM.ImgPath != null && newImgVM.ImgPath.Length > 0)
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(newImgVM.ImgPath.FileName, newImgVM.ImgPath.OpenReadStream())
                    };
                    var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                    if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        newImgVM.File = uploadResult.SecureUrl.AbsoluteUri;
                    }
                    else
                    {
                        ModelState.AddModelError("", "An error occurred while uploading the photo. Please try again.");
                        return View("UpdateImage", newImgVM);
                    }

                };
                var IMG = uniFileRepository.GetFile(newImgVM.Id);

                //Update Change
                IMG.File = newImgVM.File;
                uniFileRepository.Update(IMG);
                uniFileRepository.Save();

                return RedirectToAction("Index", "Home");
            }
            return View("UpdateImage", newImgVM);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var File = uniFileRepository.GetFile(id);
            if (ModelState.IsValid)
            {
                return View(File);
            }
            return View(File);
        }
        [HttpPost]
        public IActionResult ConfirmDelete(int id)
        {
            var File = uniFileRepository.GetFile(id);
            var fileType = File.ContentType;

            uniFileRepository.Delete(File);
            uniFileRepository.Save();
            if(fileType == Models.Enums.Filetype.IMG)
            {
                return RedirectToAction("Index", "Home"); 
            }
            else
            {
                return RedirectToAction("News");
            }
        }
    }
}
