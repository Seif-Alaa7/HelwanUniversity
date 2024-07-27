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
    public class FacultyRepository : IFacultyRepository
    {
        private readonly ApplicationDbContext context;

        public FacultyRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void Add(Faculty faculty)
        {
            context.Faculties.Add(faculty);
        }

        public void Update(Faculty faculty)
        {
            context.Faculties.Update(faculty);
        }

        public void Delete(Faculty faculty)
        {
            context.Faculties.Remove(faculty);
        }

        public Faculty GetOne(int id)
        {
            var faculty = context.Faculties
                .Include(f => f.Departments)
                .FirstOrDefault(f => f.Id == id);



            return (faculty);
        }

        public List<Faculty> GetAll()
        {
            var faculties = context.Faculties
                .ToList();
            return faculties;
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
