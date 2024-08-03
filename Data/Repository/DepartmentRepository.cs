using Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext context;

        public DepartmentRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void Add(Department department)
        {
            context.Departments.Add(department);
        }

        public void Update(Department department)
        {
            context.Departments.Update(department);
        }

        public void Delete(Department department)
        {
            context.Departments.Remove(department);
        }

        public Department GetOne(int Id)
        {
            var department = context.Departments
                .Find(Id);
            return department;
        }

        public List<Department> GetAll()
        {
            var departments = context.Departments
                .ToList();
            return departments;
        }
        public List<SelectListItem> Select()
        {
            var options =  context.Departments.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Name,

            }).ToList();
            return options;
        }
        public void Save()
        {
            context.SaveChanges();
        }
        public int GetStudentCount(int? studentDepartmentId)
        {
            if (studentDepartmentId == null)
            {
                throw new ArgumentNullException(nameof(studentDepartmentId), "Department ID cannot be null.");
            }

            var department = context.Departments
                                    .Include(d => d.Students)
                                    .SingleOrDefault(d => d.Id == studentDepartmentId);

            if (department == null)
            {
                throw new InvalidOperationException($"Department with ID {studentDepartmentId} not found.");
            }

            return department.Students.Count;
        }
        public bool Exist(string Name)
        {
            var existDepartment = context.Departments.Any(d => d.Name == Name);
            return existDepartment;
        }
        public bool ExistHeadInDepartment(int headId)
        {
            var existHeadInDepartment = context.Departments.Any(x => x.HeadId == headId);
            return existHeadInDepartment;
        }
    }
}
