using Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Enums;
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
        public List<SelectListItem> Select()
        {
            var list = context.Subjects.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Name
            }).ToList();

            return list;
        }
        public void Save()
        {
            context.SaveChanges();
        }
        public bool ExistSubject(string Subject)
        {
            var exist = context.Subjects.Any(x => x.Name == Subject);
            return exist;
        }
        public List<Subject> GetSubjects(int studentID)
        {
            try
            {
                var subjects = context.StudentSubjects
                                      .Where(x => x.StudentId == studentID)
                                      .Include(ds => ds.Subject)
                                      .AsNoTracking()
                                      .ToList();

                var list = subjects.Select(x => x.Subject).ToList();
                return list;

            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching subjects for student ID {studentID}", ex);
            }
        }
    }
}
