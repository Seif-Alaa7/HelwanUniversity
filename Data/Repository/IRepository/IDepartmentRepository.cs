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
        void Save();
    }
}
