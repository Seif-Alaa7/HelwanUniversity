using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.IRepository
{
    public interface IStudentRepository
    {
        List<Student> GetAll();
        Student GetOne(int Id);
        void Delete(int id);
        void Update(Student student);
        void Add(Student student);
        void Save();
        bool Exist(string Name);
        bool ExistPhone(string Phone);
    }
}
