using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.IRepository
{
    public interface IDepartmentRepository
    {
        List<Department> GetAll();
        Department GetOne(int Id);
        void Delete(Department department);
        void Update(Department department);
        void Add(Department department);
        List<SelectListItem> Select();
        void Save();
        bool Exist(string Name);
        bool ExistHeadInDepartment(int headId);
        int GetStudentCount(int? studentDepartmentId);
        IEnumerable<Department> GetDepartmentsByCollegeId(int collegeId);
        int DepartmentsFaculty(int Facultyid);
        Department? DepartmentByStudent(int StudentId);
        IQueryable<SelectListItem> DepartmentsSelect(int Facultyid);
        Dictionary<int, string> Dict();
        Dictionary<int, List<(int Id, string Name)>> GetDepartmentsByFaculty(List<Faculty> facultyList);
        string GetName(int id);
        Department DepartmentIncludeFaculty(int id);
    }
}
