﻿using Microsoft.AspNetCore.Mvc;
using Models;
using HelwanUniversity.Controllers;
using HelwanUniversity.Controllers;
using HelwanUniversity.Controllers;

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
