using Data.Repository.IRepository;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext context;

        public StudentRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void Add(Student student)
        {
            context.Students.Add(student);
        }

        public void Update(Student student)
        {
            context.Students.Update(student);
        }

        public void Delete(int id)
        {
            var student = GetOne(id);
            context.Students.Remove(student);
        }

        public Student GetOne(int Id)
        {
            var student = context.Students
                .Find(Id);
            return student;
        }

        public List<Student> GetAll()
        {
            var students = context.Students
                .ToList();
            return students;
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
