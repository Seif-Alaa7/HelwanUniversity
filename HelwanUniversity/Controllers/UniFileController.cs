﻿using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;
using ViewModels.UniFileVMs;
using Data.Repository;
using System.Net.Mime;
using ViewModels.FacultyVMs;

namespace HelwanUniversity.Controllers
{
    public class UniFileController : Controller
    {
        private readonly IUniFileRepository uniFileRepository;
        private readonly CloudinaryController _cloudinaryController;

        public UniFileController(IUniFileRepository uniFileRepository, CloudinaryController _cloudinaryController)
        {
            this.uniFileRepository = uniFileRepository;
            this._cloudinaryController = _cloudinaryController;

        }

        //Display image & Video
        public IActionResult News()
        {
            var videos = uniFileRepository.GetAllVideos();
            return View(videos);
        }
        public IActionResult   EmbededLink()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddVideo()
        {
            var Videovm = new UniFileVM()
            {
                File = "",
                ContentType = Models.Enums.Filetype.Video
            };
            return View(Videovm);
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
            var imgvm = new UniFileVM()
            {
                File = Empty.ToString(),
                ContentType = Models.Enums.Filetype.IMG
            };

            return View(imgvm);
        }
        [HttpPost]
        public async Task<IActionResult> SaveImg(UniFileVM uniFileVM)
        {
                try
                {
                    uniFileVM.File = await _cloudinaryController.UploadFile(uniFileVM.ImgPath, string.Empty, "An error occurred while uploading the photo. Please try again.");

                    var file = new UniFile
                    {
                        File = uniFileVM.File,
                        ContentType = uniFileVM.ContentType,
                    };

                    uniFileRepository.Add(file);
                    uniFileRepository.Save();

                    return RedirectToAction("DispalyImages");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View("AddImage", uniFileVM);
                }
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

            if (Img == null)
            {
                return NotFound();
            }
            // Mapping
            var ImgVM = new UniFileVM2
            {
                Id = Img.Id,
                File = Img.File,
                ContentType = Img.ContentType
            };
            return View(ImgVM);
        }
        [HttpPost]
        public async Task<IActionResult> SaveUpdate(UniFileVM2 newImgVM)
        {
            var IMG = uniFileRepository.GetFile(newImgVM.Id);

            try
            {
                    newImgVM.File = await _cloudinaryController.UploadFile(newImgVM.ImgPath, IMG.File, "An error occurred while uploading the photo. Please try again.");

                    //Update Change
                    IMG.File = newImgVM.File;
                    uniFileRepository.Update(IMG);
                    uniFileRepository.Save();

                return RedirectToAction("DispalyImages");
            }
            catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View("UpdateImage", newImgVM);
                }
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var file = uniFileRepository.GetFile(id);

            if (file == null)
            {
                return NotFound();
            }

            var fileType = file.ContentType;
            uniFileRepository.Delete(file);
            uniFileRepository.Save();

            return fileType == Models.Enums.Filetype.IMG
                ? RedirectToAction("DispalyImages")
                : RedirectToAction("News");
        }

        public IActionResult DispalyImages()
        {
            var Imgs = uniFileRepository.GetAllImages();
            return View(Imgs);
        }
    }
}
