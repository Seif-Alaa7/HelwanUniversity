using Data.Repository;
using Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;
using ViewModels;

namespace HelwanUniversity.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentRepository studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details(int id)
        {
            var studentDatails = studentRepository.GetOne(id);
            return View(studentDatails);
        }
        public IActionResult Edit(int id)
        {
            var student = studentRepository.GetOne(id);
            var studentVM = new StudentVM
            {
                Id = id,
                Name = student.Name,
                DepartmentId = student.DepartmentId,
                Address = student.Address,
                AdmissionDate = student.AdmissionDate,
                BirthDate = student.BirthDate,
                Gender = student.Gender,
                Nationality = student.Nationality,
                PaymentFees = student.PaymentFees,
                PaymentFeesDate = student.PaymentFeesDate,
                PhoneNumber = student.PhoneNumber,
                Picture = student.Picture,
                Religion = student.Religion
            };
            return View(studentVM);
        }
        [HttpPost]
        public IActionResult SaveEdit(StudentVM studentVM)
        {
            var student = studentRepository.GetOne(studentVM.Id);

            student.PhoneNumber = studentVM.PhoneNumber;
            student.Address = studentVM.Address;
            student.AdmissionDate = studentVM.AdmissionDate;
            student.BirthDate = studentVM.BirthDate;
            student.Name = studentVM.Name;
            student.Picture = studentVM.Picture;
            student.Gender = studentVM.Gender;
            student.Religion = studentVM.Religion;
            student.DepartmentId = studentVM.DepartmentId;
            student.Nationality = studentVM.Nationality;
            student.PaymentFees = studentVM.PaymentFees;
            student.PaymentFeesDate = studentVM.PaymentFeesDate;
            student.Id = studentVM.Id;

            studentRepository.Update(student);
            studentRepository.Save();
            return RedirectToAction("Index", "University");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            studentRepository.Delete(id);
            studentRepository.Save();

            return RedirectToAction("Index" , "University");
        }
    }
}
