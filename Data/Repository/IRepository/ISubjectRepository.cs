using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.IRepository
{
    public interface ISubjectRepository
    {
        List<Subject> GetAll();
        Subject GetOne(int Id);
        void Delete(Subject subject);
        void Update(Subject subject);
        void Add(Subject subject);
        List<SelectListItem> Select();
        void Save();
        bool ExistSubject(string Subject);
        List<Subject> GetSubjects(int studentID);
        IQueryable<Subject> SubjectsByDoctor(int id);
        string GetName(int id);
        Level GetLevel(int id);
        Semester GetSemester(int id);
        Dictionary<int, string> GetName(List<Subject> subjects);
        List<int> GetIds(List<Subject> subjects);
        List<Subject> SubjectsByDepartment(int Departmentid, Level level);
    }
}
