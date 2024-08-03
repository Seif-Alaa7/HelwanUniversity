using Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using ViewModels;

namespace HelwanUniversity.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository departmentRepository;
        private readonly IHighBoardRepository highBoardRepository;
        private readonly IDepartmentSubjectsRepository departmentSubjectsRepository;
        private readonly IStudentSubjectsRepository studentSubjectsRepository;
        private readonly IDoctorRepository doctorRepository;
        private readonly IFacultyRepository facultyRepository;
        public DepartmentController(IDepartmentRepository departmentRepository,IHighBoardRepository highBoardRepository,
            IDepartmentSubjectsRepository departmentSubjects,IStudentSubjectsRepository studentSubjects,IDoctorRepository doctorRepository,IFacultyRepository faculty)
        {
            this.departmentRepository = departmentRepository;
            this.highBoardRepository = highBoardRepository;
            this.departmentSubjectsRepository = departmentSubjects;
            this.studentSubjectsRepository = studentSubjects;
            this.doctorRepository = doctorRepository;
            this.facultyRepository = faculty;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details(int id)
        {
            var Department = departmentRepository.GetOne(id);
            var doctorDictionary = doctorRepository.GetAll()
                               .ToDictionary(x => x.Id, x => x.Name);
            ViewData["Head"] = highBoardRepository.GetOne(Department.HeadId)?.Name;
            ViewBag.Subjects = departmentSubjectsRepository.subjectsByDepartment(id);
            ViewData["Students"] = departmentRepository.GetStudentCount(id);

          var studentsBySubject = new Dictionary<int, int>();
            foreach (var subject in ViewBag.Subjects)
            {
                int studentCount = studentSubjectsRepository.StudentBySubject(subject.Subject.Id).Count;
                studentsBySubject[subject.Subject.Id] = studentCount;
            }
            ViewBag.StudentsBySubject = studentsBySubject;

            var doctorNames = new Dictionary<int, string>();
            foreach (var subject in ViewBag.Subjects)
            {
                string doctorName;
                if (doctorDictionary.TryGetValue(subject.Subject.DoctorId, out doctorName))
                {
                    doctorNames[subject.Subject.DoctorId] = doctorName;
                }
            }
            ViewBag.DoctorNames = doctorNames;

            return View(Department);
        }
        public IActionResult Edit(int id)
        {
            var department = departmentRepository.GetOne(id);

            ViewData["Heads"] = highBoardRepository.selectHeads();
            ViewData["Faculities"] = facultyRepository.Select();

            //Mapping
            var departmentVM = new DepartmentVM()
            {
                Id = id,
                HeadId = department.HeadId,
                Name = department.Name,
                FacultyId = department.FacultyId,
                Allowed = department.Allowed,
            };

            return View(departmentVM);
        }
        public IActionResult SaveEdit(DepartmentVM departmentVM)
        {
            var department = departmentRepository.GetOne(departmentVM.Id);
            if(departmentVM.Name != department.Name)
            {
                var exist = departmentRepository.Exist(departmentVM.Name);
                if(exist)
                {
                    ModelState.AddModelError("Name", "Error, you try to change department name to an existing name. Try another name.");

                    ViewData["Heads"] = highBoardRepository.selectHeads();
                    ViewData["Faculities"] = facultyRepository.Select();

                    return View("Edit");
                }
            };

            if(department.HeadId != departmentVM.HeadId) 
            {
                var Exist = departmentRepository.ExistHeadInDepartment(departmentVM.HeadId);
                if (Exist)
                {
                    ModelState.AddModelError("HeadId", "This person is already a head of a registered department.");

                    ViewData["Heads"] = highBoardRepository.selectHeads();
                    ViewData["Faculities"] = facultyRepository.Select();

                    return View("Edit");
                }
            };
                department.HeadId = departmentVM.HeadId;
                department.Name = departmentVM.Name;
                department.FacultyId = departmentVM.FacultyId;
                department.Allowed = departmentVM.Allowed;

                departmentRepository.Update(department);
                departmentRepository.Save();

                return RedirectToAction("Details", new {id = department.Id});
        }
        public IActionResult Delete(int id)
        {
            var department = departmentRepository.GetOne(id);

            var departmentSubjects = departmentSubjectsRepository.GetAll().Where(ds => ds.DepartmentId == id).ToList();
            foreach(var Department in departmentSubjects)
            {
               departmentSubjectsRepository.Delete(Department);
               departmentRepository.Save();
            }
            departmentRepository.Delete(department);
            departmentRepository.Save();

            return RedirectToAction("Details", "Faculty", new { id = department.FacultyId });
        }
    }
}
