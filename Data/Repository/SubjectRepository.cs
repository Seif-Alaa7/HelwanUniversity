using Data.Repository.IRepository;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly ApplicationDbContext context;

        public SubjectRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void Add(Subject subject)
        {
            context.Subjects.Add(subject);
        }

        public void Update(Subject subject)
        {
            context.Subjects.Update(subject);
        }

        public void Delete(Subject subject)
        {
            context.Subjects.Remove(subject);
        }

        public Subject GetOne(int Id)
        {
            var subject = context.Subjects
                .Find(Id);
            return subject;
        }

        public List<Subject> GetAll()
        {
            var subjects = context.Subjects
                .ToList();
            return subjects;
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
