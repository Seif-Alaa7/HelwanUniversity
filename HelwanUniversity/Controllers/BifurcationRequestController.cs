using Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;
using ViewModels;

namespace HelwanUniversity.Controllers
{
    public class BifurcationRequestController : Controller
    {
        private readonly IDepartmentRepository departmentRepository;
      private readonly IBifurcationRequestRepository bifurcationRequestRepository;
        public BifurcationRequestController(IDepartmentRepository department, IBifurcationRequestRepository bifurcationRequestRepository)
        {
            this.departmentRepository = department;
            this.bifurcationRequestRepository = bifurcationRequestRepository;
        }
             public IActionResult Index()
             {
                 return View();
             }
             public IActionResult AddRequest(int Facultyid,int StudentId)
             {
                 var DepartmentCounts = departmentRepository.DepartmentsFaculty(Facultyid);
                 ViewData["Counts"] = DepartmentCounts;


                 ViewData["DepartmentsOfFaculty"] = departmentRepository.DepartmentsSelect(Facultyid);

                 var Request = new BifurcationRequestVM()
                 {
                     StudentId = StudentId,
                     DepartmentIds = new List<int>(new int[DepartmentCounts])
                 };
                  return View(Request);
             }
        public IActionResult SaveAdd(BifurcationRequestVM request)
        {

            if (request.DepartmentIds.GroupBy(id => id).Any(g => g.Count() > 1))
            {
                ModelState.AddModelError("", "Duplicate departments are not allowed.");
                return View("AddRequest", request);
            }


            foreach (var departmentId in request.DepartmentIds)
            {
                var bifurcationRequest = new BifurcationRequest()
                {
                    StudentId = request.StudentId,
                    DepartmentId = departmentId,
                    Rank = request.DepartmentIds.IndexOf(departmentId) + 1
                };

                bifurcationRequestRepository.Add(bifurcationRequest);
            }
            bifurcationRequestRepository.Save();
            return RedirectToAction("Details", "Student", new { id = request.StudentId });
        }
    }
}
