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
    public class AcademicRecordsRepository : IAcademicRecordsRepository
    {
        private readonly ApplicationDbContext context;

        public AcademicRecordsRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void Add(AcademicRecords academicRecords)
        {
            context.academicRecords.Add(academicRecords);
        }

        public void Update(AcademicRecords academicRecords)
        {
            context.academicRecords.Update(academicRecords);
        }

        public void Delete(AcademicRecords academicRecords)
        {
            context.academicRecords.Remove(academicRecords);
        }

        public AcademicRecords GetOne(int Id)
        {
            var AcademicRecords = context.academicRecords
                .Find(Id);
            return AcademicRecords;
        }

        public List<AcademicRecords> GetAll()
        {
            var AcademicRecords = context.academicRecords
                .ToList();
            return AcademicRecords;
        }

        public void Save()
        {
            context.SaveChanges();
        }
        public decimal CalculateGpaSemester(int studentId, Semester semester)
        {
            var academicRecord = context.academicRecords
                 .FirstOrDefault(ar => ar.StudentId == studentId && ar.Semester == semester);

            if (academicRecord == null || academicRecord.RecordedHours == 0)
                return 0;

            return academicRecord.SemesterPoints / academicRecord.RecordedHours;
        }
        public decimal CalculateGPATotal(int studentId)
        {
            var academicRecord = context.academicRecords
                .Where(ar => ar.StudentId == studentId)
                .GroupBy(ar => ar.StudentId)
                .Select(g => new
                {
                    TotalPoints = g.Sum(ar => ar.TotalPoints),
                    TotalHours = g.Sum(ar => ar.TotalHours)
                })
                .FirstOrDefault();

            if (academicRecord == null || academicRecord.TotalHours == 0)
                return 0;

            return academicRecord.TotalPoints / academicRecord.TotalHours;
        }
        public void DeleteByStudent(int studentId)
        {
           var link =  context.academicRecords.FirstOrDefault(x=>x.StudentId == studentId);
           context.academicRecords.Remove(link);
        }
        public AcademicRecords GetStudent(int id)
        {
            var Student = context.academicRecords.FirstOrDefault(x => x.StudentId == id);
            return Student;
        }
        public Dictionary<int, (Level Level, Semester Semester)> GetLevelANDSemester(List<Student> students)
        {
            var StudentsDictionary = context.academicRecords.ToList()
                  .ToDictionary(x => x.StudentId, x => new { x.Level, x.Semester });

            var records = new Dictionary<int, (Level Level, Semester Semester)>();
            foreach (var student in students)
            {
                if (StudentsDictionary.TryGetValue(student.Id, out var record))
                {
                    records[student.Id] = (record.Level, record.Semester);
                }
            }
            return records;
        }
        public IQueryable<AcademicRecords> GetStudents(int Departmentid,int Facultyid,Level level)
        {
            var list = context.academicRecords
                  .Include(ar => ar.Student)
                  .Where(ar => ar.Student.DepartmentId == Departmentid
                               && ar.Level == level
                               && ar.Student.Department.FacultyId == Facultyid);

            return list;
        }
    }
}
