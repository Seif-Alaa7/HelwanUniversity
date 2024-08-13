﻿using Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using ViewModels;

namespace HelwanUniversity.Areas.Doctors.Controllers
{
    [Area("Doctors")]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository departmentRepository;
        private readonly IHighBoardRepository highBoardRepository;
        private readonly IDepartmentSubjectsRepository departmentSubjectsRepository;
        private readonly IDoctorRepository doctorRepository;
        private readonly IFacultyRepository facultyRepository;
        public DepartmentController(IDepartmentRepository departmentRepository,IHighBoardRepository highBoardRepository,
            IDepartmentSubjectsRepository departmentSubjects,IDoctorRepository doctorRepository,IFacultyRepository faculty)
        {
            this.departmentRepository = departmentRepository;
            this.highBoardRepository = highBoardRepository;
            this.departmentSubjectsRepository = departmentSubjects;
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
        public IActionResult Students(int id)
        {
            var Faculty = facultyRepository.GetFacultybyDean(id);
            var Departments = departmentRepository.GetDepartmentsByCollegeId(Faculty.Id);

            ViewData["FacultyName"] = Faculty.Name;
            return View(Departments);
        }
    }
}
