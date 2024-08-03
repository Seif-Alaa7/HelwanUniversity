using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.IRepository
{
    public interface IDepartmentSubjectsRepository
    {
        List<DepartmentSubjects> subjectsByDepartment(int id);
        bool Exist(DepartmentSubjects model);
        void Add(DepartmentSubjects model);
        void Save();

    }
}
