using Data.Repository.IRepository;
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
    public class DepartmentSubjectsRepository : IDepartmentSubjectsRepository
    {
        private readonly ApplicationDbContext context;
        public DepartmentSubjectsRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public List<DepartmentSubjects> subjectsByDepartment(int id)
        {
            var subjects = context.DepartmentSubjects
                          .Where(ds => ds.DepartmentId == id)
                          .Include(ds => ds.Subject)
                          .ToList();
            return subjects;
        }
        public bool Exist(DepartmentSubjects model)
        {
            var ExistDepartmentSubject = context.DepartmentSubjects.Any(a => a.DepartmentId == model.DepartmentId && a.SubjectId == model.SubjectId);
            return ExistDepartmentSubject;
        }
        public void Add(DepartmentSubjects model)
        {
            context.DepartmentSubjects.Add(model);
        }
        public List<DepartmentSubjects> GetAll()
            {
                var list = context.DepartmentSubjects.ToList(); 
               return list;
            }
        public void Delete(DepartmentSubjects model)
        {
            context.DepartmentSubjects.Remove(model);
        }
        public void Save()
        {
            context.SaveChanges();
        }
        public List<DepartmentSubjects> SubjectDepartments(int subjectId)
        {
            var Departments = context.DepartmentSubjects.Where(x=>x.SubjectId == subjectId).ToList();
            return Departments;
        }
        public DepartmentSubjects? DeleteRelation(int subjectId, int DepartmentId)
        {
            var Relation = context.DepartmentSubjects.FirstOrDefault(x => x.SubjectId == subjectId && x.DepartmentId == DepartmentId);
            return Relation;
        }
        public List<DepartmentSubjects> StudentSubjects(Level level, Semester semester, int DepartmentId)
        {
            var Subjects = context.DepartmentSubjects.Include(x => x.Subject).Where(x => x.DepartmentId == DepartmentId);
            var Studentsubject = Subjects.Where(x => x.Subject.Level == level && x.Subject.Semester == semester).ToList();

            return Studentsubject;
        }
    }
}
