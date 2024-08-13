using Data.Repository;
using Data.Repository.IRepository;
using HelwanUniversity.Services;
using Microsoft.AspNetCore.Mvc;
using ViewModels;

namespace HelwanUniversity.Areas.Students.Controllers
{
    [Area("Students")]
    public class StudentController : Controller
    {
        private readonly IStudentRepository studentRepository;
        private readonly IDepartmentRepository departmentRepository;
        private readonly IFacultyRepository faculty;
        private readonly IUniversityRepository universityRepository;
        private readonly ICloudinaryService cloudinaryService;

        public StudentController(IStudentRepository studentRepository , IDepartmentRepository departmentRepository
            ,IFacultyRepository faculty, IUniversityRepository universityRepository , ICloudinaryService cloudinaryService)
        {
            this.studentRepository = studentRepository;
            this.departmentRepository = departmentRepository;
            this.faculty = faculty;
            this.universityRepository = universityRepository;
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
            ViewData["FormBifurcation"] = universityRepository.Get().GoogleForm;
            return View(studentDatails);
        }
        public IActionResult ChangePicture(int id)
        {
            var student = studentRepository.GetOne(id);
            var ModelVM = new Picture()
            {
                Id = id,
                MainPicture = student.Picture
            };

            return View(ModelVM);
        }
        [HttpPost]
        public async Task<IActionResult> SaveChange(Picture ModelVM)
        {
            var student = studentRepository.GetOne(ModelVM.Id);
            try
            {
                ModelVM.MainPicture = await cloudinaryService.UploadFile(ModelVM.MainPictureFile, student.Picture, "An error occurred while uploading the Picture. Please try again.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View("ChangePicture", ModelVM);
            }
            student.Picture = ModelVM.MainPicture;
            studentRepository.Update(student);
            studentRepository.Save();

            return RedirectToAction("Details", new { id = student.Id });
        }
    }
}
