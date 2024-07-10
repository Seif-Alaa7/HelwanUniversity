using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.IRepository
{
    public interface IFacultyRepository
    {
        List<Faculty> GetAll();
        Faculty GetOne(int Id);
        void Delete(Faculty faculty);
        void Update(Faculty faculty);
        void Add(Faculty faculty);
        void Save();
    }
}
