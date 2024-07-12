using Data.Repository.IRepository;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class UniversityRepository : IUniversityRepository
    {
        private readonly ApplicationDbContext context;

        public UniversityRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void Add(University university)
        {
            context.University.Add(university);
        }

        public void Update(University university)
        {
            context.University.Update(university);
        }

        public void Delete(University university)
        {
            context.University.Remove(university);
        }

        public University GetOne(int Id)
        {
            var university = context.University
                .Find(Id);
            return university;
        }

        public List<University> GetAll()
        {
            var universities = context.University
                .ToList();
            return universities;
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
