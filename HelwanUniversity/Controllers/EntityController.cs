using Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using ViewModels;

namespace HelwanUniversity.Controllers
{
    public class EntityController : Controller
    {
        private readonly ISubjectRepository subjectRepository;
        private readonly IDepartmentRepository departmentRepository;
        private readonly IFacultyRepository facultyRepository;

        public EntityController(ISubjectRepository subjectRepository , IDepartmentRepository departmentRepository , IFacultyRepository facultyRepository)
        {
            this.subjectRepository = subjectRepository;
            this.departmentRepository = departmentRepository;
            this.facultyRepository = facultyRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.EntityTypes = new List<string> { "Department", "Faculty", "Subject" };
            return View(new AddEntity());
        }
        [HttpPost]
        public IActionResult Add(AddEntity entity)
        {
            /*if (!ModelState.IsValid)
            {
                ViewBag.EntityTypes = new List<string> { "Department", "Faculty", "Subject" };
                return View(entity);
            }*/

            switch (entity.EntityType)
            {
                case "Department":
                    var department = new Department
                    {
                        Name = entity.Name,
                        HeadId = entity.HeadId ?? 0,
                        FacultyId = entity.FacultyId ?? 0,
                        Allowed = entity.Allowed ?? 0
                    };
                    departmentRepository.Add(department);
                    departmentRepository.Save();
                    break;

                case "Faculty":
                    var faculty = new Faculty
                    {
                        Name = entity.Name,
                        DeanId = entity.DeanId ?? 0,
                        Logo = entity.LogoPath,
                        Picture = entity.PicturePath,
                        Description = entity.Description,
                        ViewCount = entity.ViewCount ?? 0
                    };
                    facultyRepository.Add(faculty);
                    facultyRepository.Save();
                    break;

                case "Subject":
                    var subject = new Subject
                    {
                        Name = entity.Name,
                        DepartmentId = entity.DepartmentId ?? 0,
                        DoctorId = entity.DoctorId ?? 0,
                        SubjectHours = entity.SubjectHours ?? 0,
                        StudentsAllowed = entity.StudentsAllowed ?? 0,
                        Level = entity.Level ?? 0,
                        Semester = entity.Semester ?? 0,
                        summerStatus = entity.SummerStatus ?? 0,
                        subjectType = entity.SubjectType ?? 0,
                        Salary = entity.Salary ?? 0
                    };
                    subjectRepository.Add(subject);
                    subjectRepository.Save();
                    break;
                default:
                    ModelState.AddModelError("", "An Error");
                    ViewBag.EntityTypes = new List<string> { "Department", "Faculty", "Subject" };
                    return View(entity);
            }
            TempData["SuccessMessage"] = "Success !";
            return RedirectToAction("Index" , "Faculty");
        }

    }
}
