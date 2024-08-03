using Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class StudentSubjectsRepository : IStudentSubjectsRepository
    {
        private readonly ApplicationDbContext context;
        public StudentSubjectsRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public List<StudentSubjects> StudentBySubject(int id)
        {
            var students = context.StudentSubjects
                          .Where(ds => ds.SubjectId == id)
                          .Include(ds => ds.Student)
                          .ToList();
            return students;
        }
    }
}
