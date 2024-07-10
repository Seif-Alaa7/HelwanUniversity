using Data.Repository.IRepository;
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

        public Faculty GetOne(int Id)
        {
            var faculty = context.Faculties
                .Find(Id);
            return faculty;
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
