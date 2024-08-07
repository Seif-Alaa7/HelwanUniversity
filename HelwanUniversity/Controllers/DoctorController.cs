using Data.Repository;
using Data.Repository.IRepository;
using HelwanUniversity.Services;
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
        private readonly ICloudinaryService cloudinaryService;

        public DoctorController(IDoctorRepository doctorRepository, ICloudinaryService cloudinaryService)
        {
            this.doctorRepository = doctorRepository;
            this.cloudinaryService = cloudinaryService;
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
        public async Task<IActionResult> SaveEdit(DoctorVM doctorVM)
        {
            var doctor = doctorRepository.GetOne(doctorVM.Id);

            if(doctor.Name != doctorVM.Name)
            {
                var exist = doctorRepository.ExistName(doctorVM.Name);
                if(exist)
                {
                    ModelState.AddModelError("Name", "This Name is Already Exist");
                    return View("Edit",doctorVM);  
                }
            }

            try
            {
                doctorVM.Picture = await cloudinaryService.UploadFile(doctorVM.FormFile, doctor.Picture, "An error occurred while uploading the photo. Please try again.");

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View("Edit", doctorVM);
            }
            doctor.Id = doctorVM.Id;
            doctor.Name = doctorVM.Name;
            doctor.Address = doctorVM.Address;
            doctor.JobTitle = doctorVM.JobTitle;
            doctor.Picture = doctorVM.Picture;
            doctor.Gender = doctorVM.Gender;
            doctor.Religion = doctorVM.Religion;

            doctorRepository.Update(doctor);
            doctorRepository.Save();

            return RedirectToAction("Details", new { id = doctor.Id });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            
            doctorRepository.Delete(id);
            doctorRepository.Save();

            return RedirectToAction("Index", "University");
        }
    }
}
