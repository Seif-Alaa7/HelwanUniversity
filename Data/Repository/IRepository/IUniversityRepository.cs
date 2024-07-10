using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.IRepository
{
    public interface IUniversityRepository
    {
        List<University> GetAll();
        University GetOne(int Id);
        void Delete(University university);
        void Update(University university);
        void Add(University university);
        void Save();
    }
}
