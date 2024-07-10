using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.IRepository
{
    public interface IAcademicRecordsRepository
    {
        List<AcademicRecords> GetAll();
        AcademicRecords GetOne(int Id);
        void Delete(AcademicRecords academicRecords);
        void Update(AcademicRecords academicRecords);
        void Add(AcademicRecords academicRecords);
        void Save();
    }
}
