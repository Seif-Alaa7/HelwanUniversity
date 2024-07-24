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
        void Update(University university);
        University? Get();
        void Save();
    }
}
