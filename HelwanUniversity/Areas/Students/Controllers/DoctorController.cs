using Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace HelwanUniversity.Areas.Students.Controllers
{
    [Area("Students")]
    public class DoctorController : Controller
    {
        private readonly IDoctorRepository doctorRepository;

        public DoctorController(IDoctorRepository doctorRepository)
        {
            this.doctorRepository = doctorRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details(int id)
        {
            var DoctorDatails = doctorRepository.GetOne(id);
            return View(DoctorDatails);
        }
    }
}
