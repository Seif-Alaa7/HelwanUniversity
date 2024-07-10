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
            context.Universities.Add(university);
        }

        public void Update(University university)
        {
            context.Universities.Update(university);
        }

        public void Delete(University university)
        {
            context.Universities.Remove(university);
        }

        public University GetOne(int Id)
        {
            var university = context.Universities
                .Find(Id);
            return university;
        }

        public List<University> GetAll()
        {
            var universities = context.Universities
                .ToList();
            return universities;
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
