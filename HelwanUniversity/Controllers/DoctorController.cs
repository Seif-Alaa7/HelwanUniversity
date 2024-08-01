using Data.Repository;
using Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Enums;
using System.Net;
using ViewModels;

namespace HelwanUniversity.Controllers
{
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
        public IActionResult Edit(int id)
        {
            var doctor = doctorRepository.GetOne(id);
            var doctorVM = new DoctorVM
            {
                Id = id,
                Name = doctor.Name,
                Address = doctor.Address,
                JobTitle = doctor.JobTitle,
                Picture = doctor.Picture,
                Gender = doctor.Gender,
                Religion = doctor.Religion
            };
            return View(doctorVM);
        }
        [HttpPost]
        public IActionResult SaveEdit(DoctorVM doctorVM)
        {
            var doctor = doctorRepository.GetOne(doctorVM.Id);

            doctor.Id = doctorVM.Id;
            doctor.Name = doctorVM.Name;
            doctor.Address = doctorVM.Address;
            doctor.JobTitle = doctorVM.JobTitle;
            doctor.Picture = doctorVM.Picture;
            doctor.Gender = doctorVM.Gender;
            doctor.Religion = doctorVM.Religion;

            doctorRepository.Update(doctor);
            doctorRepository.Save();
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Doctor doctor)
        {
            doctorRepository.Delete(doctor);
            doctorRepository.Save();

            return RedirectToAction("Index");
        }
    }
}
