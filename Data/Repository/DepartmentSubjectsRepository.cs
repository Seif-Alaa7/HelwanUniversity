using Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Models;
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
    }
}
