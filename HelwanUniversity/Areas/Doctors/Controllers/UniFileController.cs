using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;
using ViewModels.UniFileVMs;
using Data.Repository;
using System.Net.Mime;
using ViewModels.FacultyVMs;
using HelwanUniversity.Controllers;

namespace HelwanUniversity.Areas.Doctors.Controllers
{
    [Area("Doctors")]
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
