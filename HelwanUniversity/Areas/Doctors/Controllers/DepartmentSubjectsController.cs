﻿using Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace HelwanUniversity.Areas.Doctors.Controllers
{
    [Area("Doctors")]
    public class DepartmentSubjectsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
    }
}
