using Data.Repository;
using Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace HelwanUniversity.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DepartmentSubjectsController : Controller
    {
        private readonly IDepartmentRepository departmentRepository;
        private readonly ISubjectRepository subjectRepository;
        private readonly IDepartmentSubjectsRepository DepartsubjectsRepository;
        private readonly IAcademicRecordsRepository academicRecordsRepository;
        public DepartmentSubjectsController(IDepartmentRepository department,ISubjectRepository subject,IDepartmentSubjectsRepository repository,IAcademicRecordsRepository academicRecordsRepository)
        {
            this.departmentRepository = department;
            this.subjectRepository = subject;
            this.DepartsubjectsRepository = repository;
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
            var ExistDepartmentSubject = DepartsubjectsRepository.Exist(model);

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
                DepartsubjectsRepository.Add(DepartmentSubject);
                DepartsubjectsRepository.Save();

            }
            return RedirectToAction("Details", "Department", new { area = "Admin", id = model.DepartmentId });
        }
        public IActionResult Delete(int subjectId, int departmentId)
        {
            var link = DepartsubjectsRepository.DeleteRelation(subjectId, departmentId);

            if (link == null)
            {
                TempData["ErrorMessage"] = "The relationship between the subject and department could not be found.";
                return RedirectToAction("Details", "Department", new { area = "Admin", id = departmentId });

            }

            DepartsubjectsRepository.Delete(link);
            DepartsubjectsRepository.Save();

            return RedirectToAction("Details", "Department", new { area = "Admin", id = departmentId });
        }
        public IActionResult DisplaySubjects(int Studentid)
        {

            var department = departmentRepository.DepartmentByStudent(Studentid);
            var level = academicRecordsRepository.GetAll().FirstOrDefault(x => x.StudentId == Studentid).Level;
            var semester = academicRecordsRepository.GetAll().FirstOrDefault(x => x.StudentId == Studentid).Semester;

            ViewData["StudentId"] = Studentid;
            ViewData["departmentName"] = department.Name;
            var StudentSubjects = DepartsubjectsRepository.StudentSubjects(level , semester ,department.Id);
            return View(StudentSubjects);
        }
    }
}
