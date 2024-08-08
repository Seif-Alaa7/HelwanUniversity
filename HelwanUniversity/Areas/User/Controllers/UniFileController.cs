﻿using Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace HelwanUniversity.Areas.User.Controllers
{
    [Area("User")]
    public class UniFileController : Controller
    {
        private readonly IUniFileRepository uniFileRepository;

        public UniFileController(IUniFileRepository uniFileRepository)
        {
            this.uniFileRepository = uniFileRepository;
        }

        //Display image & Video
        public IActionResult News()
        {
            var videos = uniFileRepository.GetAllVideos();
            return View(videos);
        }
        public IActionResult EmbededLink()
        {
            return View();
        }
    }
}
