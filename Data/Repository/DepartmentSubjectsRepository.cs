using Data.Repository.IRepository;
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
    }
}
