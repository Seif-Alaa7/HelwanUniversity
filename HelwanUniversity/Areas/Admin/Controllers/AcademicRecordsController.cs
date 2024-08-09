using Data.Repository;
using Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Enums;

namespace HelwanUniversity.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AcademicRecordsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
