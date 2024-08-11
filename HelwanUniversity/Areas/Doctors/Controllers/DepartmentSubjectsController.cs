using Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace HelwanUniversity.Areas.Doctors.Controllers
{
    [Area("Doctors")]
    public class DepartmentSubjectsController : Controller
    {
        private readonly IDepartmentRepository departmentRepository;
        private readonly IDepartmentSubjectsRepository DepartsubjectsRepository;
        private readonly IAcademicRecordsRepository academicRecordsRepository;
        public DepartmentSubjectsController(IDepartmentRepository department, IDepartmentSubjectsRepository repository
            , IAcademicRecordsRepository academicRecordsRepository)
        {
            this.departmentRepository = department;
            this.DepartsubjectsRepository = repository;
            this.academicRecordsRepository = academicRecordsRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult DisplaySubjects(int Studentid)
        {

            var department = departmentRepository.DepartmentByStudent(Studentid);
            var level = academicRecordsRepository.GetAll().FirstOrDefault(x => x.StudentId == Studentid).Level;
            var semester = academicRecordsRepository.GetAll().FirstOrDefault(x => x.StudentId == Studentid).Semester;

            ViewData["StudentId"] = Studentid;
            ViewData["departmentName"] = department.Name;
            var StudentSubjects = DepartsubjectsRepository.StudentSubjects(level, semester, department.Id);
            return View(StudentSubjects);
        }

    }
}
