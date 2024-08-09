using Models;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.IRepository
{
    public interface IAcademicRecordsRepository
    {
        List<AcademicRecords> GetAll();
        AcademicRecords GetOne(int Id);
        void Delete(AcademicRecords academicRecords);
        void Update(AcademicRecords academicRecords);
        void Add(AcademicRecords academicRecords);
        void Save();
        decimal CalculateGpaSemester(int studentId, Semester semester);
        decimal CalculateGPATotal(int studentId);
        void DeleteByStudent(int studentId);
        AcademicRecords GetStudent(int id);
        Dictionary<int, (Level Level, Semester Semester)> GetLevelANDSemester(List<Student> students);
        IQueryable<AcademicRecords> GetStudents(int Departmentid, int Facultyid, Level level);
    }
}
