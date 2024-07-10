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
        void Delete(Doctor doctor);
        void Update(Doctor doctor);
        void Add(Doctor doctor);
        void Save();
    }
}
