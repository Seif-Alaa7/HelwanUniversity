using Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;
using HelwanUniversity.Controllers;

namespace HelwanUniversity.Areas.Students.Controllers
{
    [Area("Students")]
    public class DepartmentSubjectsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
    }
}
