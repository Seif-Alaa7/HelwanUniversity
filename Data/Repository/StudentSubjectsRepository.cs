using Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class StudentSubjectsRepository : IStudentSubjectsRepository
    {
        private readonly ApplicationDbContext context;
        public StudentSubjectsRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
    }
}
