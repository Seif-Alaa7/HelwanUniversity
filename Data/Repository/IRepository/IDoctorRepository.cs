﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.IRepository
{
    public interface IDoctorRepository
    {
        List<Doctor> GetAll();
        Doctor GetOne(int Id);
        void Delete(int id);
        void Update(Doctor doctor);
        void Add(Doctor doctor);
        List<SelectListItem> Select();
        void Save();
        bool ExistName(string Name);
        Dictionary<int, string> GetName(List<Subject> subjects);
        Dictionary<int, string> Dict();
        Dictionary<int, string> GetName(List<DepartmentSubjects> subjects);
        Dictionary<int, List<string>> GetSubjects(List<Doctor> Doctors);
        public Dictionary<int, List<string>> GetDepartments(List<Doctor> doctors);
        Dictionary<int, List<string>> GetColleges(List<Doctor> doctors);
    }
}
