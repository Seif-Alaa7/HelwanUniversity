using Data.Repository.IRepository;
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

        public void Delete(Doctor doctor)
        {
            context.Doctors.Remove(doctor);
        }

        public Doctor GetOne(int Id)
        {
            var doctor = context.Doctors
                .Find(Id);
            return doctor;
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
    }
}
