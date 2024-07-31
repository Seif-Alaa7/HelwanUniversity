using Data.Repository;
using Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;
using ViewModels.FacultyVMs;

namespace HelwanUniversity.Controllers
{
    public class FacultyController : Controller
    {
        private readonly IFacultyRepository facultyRepository;

        public FacultyController(IFacultyRepository facultyRepository)
        {
            this.facultyRepository = facultyRepository;
        }
        public IActionResult Index()
        {
            var faculties =facultyRepository.GetAll().ToList();
            return View(faculties);
        }
        public IActionResult Details(int id)
        {
            var faculty = facultyRepository.GetOne(id);
            if (faculty == null)
            {
                return NotFound();
            }
            return View(faculty);
        }
        public IActionResult Edit(int id)
        {
            var Faculty = facultyRepository.GetOne(id);
            var FacultyVM = new FacultyVm
            {
                DeanId = Faculty.DeanId,
                Id = Faculty.Id,
                Description = Faculty.Description,
                Logo = Faculty.Logo,
                Name = Faculty.Name,
                Picture = Faculty.Picture,
                ViewCount = Faculty.ViewCount
            };
            return View(FacultyVM);
        }
        [HttpPost]
        public IActionResult SaveEdit(FacultyVm faculty)
        {
            var Faculty = facultyRepository.GetOne(faculty.Id);

            Faculty.Description = faculty.Description;
            Faculty.Id = faculty.Id;
            Faculty.DeanId = faculty.DeanId;
            Faculty.Logo = faculty.Logo;
            Faculty.Name = faculty.Name;
            Faculty.Picture = faculty.Picture;
            Faculty.ViewCount = faculty.ViewCount;

            facultyRepository.Update(Faculty);
            facultyRepository.Save();
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Faculty faculty)
        {
            facultyRepository.Delete(faculty);
            facultyRepository.Save();

            return RedirectToAction("Index");
        }
    }
}
