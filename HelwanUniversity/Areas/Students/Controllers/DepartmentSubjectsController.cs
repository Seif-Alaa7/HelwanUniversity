using Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace HelwanUniversity.Areas.Students.Controllers
{
    [Area("Students")]
    public class DepartmentSubjectsController : Controller
    {
        private readonly IDepartmentRepository departmentRepository;
        private readonly ISubjectRepository subjectRepository;
        private readonly IDepartmentSubjectsRepository departmentSubjectsRepository;
        private readonly IAcademicRecordsRepository academicRecordsRepository;

        public DepartmentSubjectsController(IDepartmentRepository departmentRepository, ISubjectRepository subjectRepository,
            IDepartmentSubjectsRepository departmentSubjectsRepository
            , IAcademicRecordsRepository academicRecordsRepository)
        {
            this.departmentRepository = departmentRepository;
            this.subjectRepository = subjectRepository;
            this.departmentSubjectsRepository = departmentSubjectsRepository;
            this.academicRecordsRepository = academicRecordsRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add(int id)
        {
            ViewData["DepartId"] = id;
            ViewData["Subjects"] = subjectRepository.Select();
            return View();
        }
        public IActionResult SaveAdd(DepartmentSubjects model)
        {
            var ExistDepartmentSubject = departmentSubjectsRepository.Exist(model);

            if (ExistDepartmentSubject)
            {
                ModelState.AddModelError("SubjectId", "This Subject is Already Exist in this Department..");
                ViewData["Subjects"] = subjectRepository.Select();
                return View("Add");
            }
            else
            {
                var DepartmentSubject = new DepartmentSubjects()
                {
                    DepartmentId = model.DepartmentId,
                    SubjectId = model.SubjectId,
                };
                departmentSubjectsRepository.Add(DepartmentSubject);
                departmentSubjectsRepository.Save();

            }
            return RedirectToAction("Details", "Department", new { area = "Admin", id = model.DepartmentId });
        }
        public IActionResult Delete(int subjectId, int departmentId)
        {
            var link = departmentSubjectsRepository.DeleteRelation(subjectId, departmentId);

            if (link == null)
            {
                TempData["ErrorMessage"] = "The relationship between the subject and department could not be found.";
                return RedirectToAction("Details", "Department", new { area = "Admin", id = departmentId });

            }
            departmentSubjectsRepository.Delete(link);
            departmentSubjectsRepository.Save();

            return RedirectToAction("Details", "Department", new { area = "Admin", id = departmentId });
        }
        public IActionResult DisplaySubjects(int id)
        {

            var department = departmentRepository.DepartmentByStudent(id);
            var level = academicRecordsRepository.GetAll().FirstOrDefault(x => x.StudentId == id).Level;
            var semester = academicRecordsRepository.GetAll().FirstOrDefault(x => x.StudentId == id).Semester;

            ViewData["StudentId"] = id;
            var StudentSubjects = departmentSubjectsRepository.StudentSubjects(level, semester, department.Id);
            return View(StudentSubjects);
        }
    }
}
