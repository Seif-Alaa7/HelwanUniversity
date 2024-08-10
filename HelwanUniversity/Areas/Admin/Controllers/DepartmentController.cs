using Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;
using ViewModels;

namespace HelwanUniversity.Areas.Admin.Controllers
{
    [Area("Admin")]
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

            ViewData["Head"] = highBoardRepository.GetName(Department.HeadId);
            ViewBag.Subjects = departmentSubjectsRepository.subjectsByDepartment(id);
            ViewData["Students"] = departmentRepository.GetStudentCount(id);

            ViewBag.StudentsBySubject = departmentSubjectsRepository.StudentCounts(ViewBag.Subjects);
            ViewBag.DoctorNames = doctorRepository.GetName(ViewBag.Subjects);

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

            if (departmentVM.FacultyId == department.FacultyId)
            {
               return RedirectToAction("Details","Faculty", new { id = department.FacultyId });
            };
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
