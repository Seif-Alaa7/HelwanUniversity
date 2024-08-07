using Data.Repository;
using Data.Repository.IRepository;
using HelwanUniversity.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System.Numerics;
using ViewModels;

namespace HelwanUniversity.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentRepository studentRepository;
        private readonly IDepartmentRepository departmentRepository;
        private readonly IFacultyRepository faculty;
        private readonly ICloudinaryService cloudinaryService;

        public StudentController(IStudentRepository studentRepository , IDepartmentRepository departmentRepository,IFacultyRepository faculty,ICloudinaryService cloudinaryService)
        {
            this.studentRepository = studentRepository;
            this.departmentRepository = departmentRepository;
            this.faculty = faculty;
            this.cloudinaryService = cloudinaryService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details(int id)
        {
            var studentDatails = studentRepository.GetOne(id);
            var department = departmentRepository.DepartmentByStudent(id);

            if (department != null)
            {
                var facultyData = faculty.FacultyByDepartment(department.Id);
                ViewData["Faculty"] = facultyData;
            }
            else
            {
                ViewData["Faculty"] = null;
            }

            return View(studentDatails);
        }
        public IActionResult Edit(int id)
        {
            var student = studentRepository.GetOne(id);
            ViewBag.Departments = new SelectList(departmentRepository.GetAll(), "Id", "Name");
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
        public async Task<IActionResult> SaveEdit(StudentVM studentVM)
        {
            var student = studentRepository.GetOne(studentVM.Id);

            if(studentVM.Name != student.Name)
            {
                var Exist = studentRepository.Exist(studentVM.Name);
                if (Exist)
                {
                    ModelState.AddModelError("Name", "This Name is Already Exist");
                    ViewBag.Departments = new SelectList(departmentRepository.GetAll(), "Id", "Name");
                    return View("Edit", studentVM);
                }
            }
            if(studentVM.PhoneNumber != student.PhoneNumber)
            {
                var Exist = studentRepository.ExistPhone(studentVM.PhoneNumber);
                if (Exist)
                {
                    ModelState.AddModelError("PhoneNumber", "This Phone is Already Exist");
                    ViewBag.Departments = new SelectList(departmentRepository.GetAll(), "Id", "Name");
                    return View("Edit", studentVM);
                }
            }
            try
            {
                studentVM.Picture = await cloudinaryService.UploadFile(studentVM.FormFile,student.Picture, "An error occurred while uploading the photo. Please try again.");

            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ViewBag.Departments = new SelectList(departmentRepository.GetAll(), "Id", "Name");
                return View("Edit",studentVM);
            }

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

            studentRepository.Update(student);
            studentRepository.Save();
            return RedirectToAction("Details", new { id = student.Id });
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
