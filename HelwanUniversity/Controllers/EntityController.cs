﻿using Data;
using Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using ViewModels;

namespace HelwanUniversity.Controllers
{
    public class EntityController : Controller
    {
        private readonly ISubjectRepository subjectRepository;
        private readonly IFacultyRepository facultyRepository;
        private readonly ApplicationDbContext context;
        private readonly CloudinaryController cloudinaryController;
        private readonly IDepartmentRepository departmentRepository;
        private readonly IDoctorRepository doctorRepository;
        private readonly IHighBoardRepository highBoardRepository;


        public EntityController(ISubjectRepository subjectRepository ,
            IFacultyRepository facultyRepository, 
            ApplicationDbContext context,
            CloudinaryController cloudinaryController,
            IDepartmentRepository department,
            IDoctorRepository doctorRepository,
            IHighBoardRepository highBoardRepository
            )
        {
            this.subjectRepository = subjectRepository;
            this.facultyRepository = facultyRepository;
            this.context = context;
            this.cloudinaryController = cloudinaryController;   
            this.departmentRepository = department;
            this.doctorRepository = doctorRepository;
            this.highBoardRepository = highBoardRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Add()
        {
            LoadPageData();

            return View(new AddEntity());
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddEntity entity)
        {
            if (!ModelState.IsValid)
            {
                LoadPageData();
                return View(entity);
            }
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

                case "FacultyVm":
                    try
                    {
                        entity.LogoPath = await cloudinaryController.UploadFile(entity.Logo, string.Empty, "There was an error uploading the cinema logo. Please try again.");
                        entity.PicturePath = await cloudinaryController.UploadFile(entity.Picture, string.Empty, "There was an error uploading the cinema Picture. Please try again.");

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
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                        LoadPageData();
                        return View("Add", entity);
                    }
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
                    LoadPageData();
                    return View(entity);
            }
            TempData["SuccessMessage"] = "Success !";
            return RedirectToAction("Index" , "Faculty");
        }
        private void LoadPageData()
        {
            ViewBag.EntityTypes = new List<string> { "Department", "FacultyVm", "Subject" };

            ViewData["Heads"] = highBoardRepository.selectHeads();
            ViewData["Faculties"] = facultyRepository.Select();
            ViewData["Deans"] = highBoardRepository.selectDeans();
            ViewData["Doctors"] = doctorRepository.Select();
            ViewData["Departments"] = departmentRepository.Select();
        }
    }
}
