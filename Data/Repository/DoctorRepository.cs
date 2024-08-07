using Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class DoctorRepository :IDoctorRepository
    {
        private readonly ApplicationDbContext context;

        public DoctorRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void Add(Doctor doctor)
        {
            context.Doctors.Add(doctor);
        }

        public void Update(Doctor doctor)
        {
            context.Doctors.Update(doctor);
        }

        public void Delete(int id)
        {
            var doctor = GetOne(id);
            context.Doctors.Remove(doctor);
        }

        public Doctor GetOne(int Id)
        {
            var doctor = context.Doctors
                .Find(Id);
            return doctor;
        }
        public List<SelectListItem> Select()
        {
            var options =  context.Doctors.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Name

            }).ToList();
            return options;
        }

        public List<Doctor> GetAll()
        {
            var doctors = context.Doctors
                .ToList();
            return doctors;
        }

        public void Save()
        {
            context.SaveChanges();
        }
        public bool ExistName(string Name)
        {
            var exist = context.Doctors.Any(x => x.Name == Name);
            return exist;
        }
        public Dictionary<int , string> GetName(List<Subject> subjects)
        {
            var doctorDictionary = context.Doctors
                .ToList().ToDictionary(x => x.Id, x => x.Name);

            var doctorNames = new Dictionary<int, string>();
            foreach (var subject in subjects)
            {
                string doctorName;
                if (doctorDictionary.TryGetValue(subject.DoctorId, out doctorName))
                {
                    doctorNames[subject.DoctorId] = doctorName;
                }
            }
            return doctorNames;
        }
        public Dictionary<int, string> GetName(List<DepartmentSubjects> subjects)
        {
            var doctorDictionary = context.Doctors
                .ToList().ToDictionary(x => x.Id, x => x.Name);

            var doctorNames = new Dictionary<int, string>();
            foreach (var subject in subjects)
            {
                string doctorName;
                if (doctorDictionary.TryGetValue(subject.Subject.DoctorId, out doctorName))
                {
                    doctorNames[subject.Subject.DoctorId] = doctorName;
                }
            }
            return doctorNames;
        }
        public Dictionary<int, string> Dict()
        {
            var Dict = context.Doctors
                .ToList().ToDictionary(x => x.Id, x => x.Name);

            return Dict;
        }
    }
}
