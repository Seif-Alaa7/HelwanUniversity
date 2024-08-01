using Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
