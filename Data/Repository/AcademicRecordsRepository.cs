using Data.Repository.IRepository;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class AcademicRecordsRepository : IAcademicRecordsRepository
    {
        private readonly ApplicationDbContext context;

        public AcademicRecordsRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void Add(AcademicRecords academicRecords)
        {
            context.academicRecords.Add(academicRecords);
        }

        public void Update(AcademicRecords academicRecords)
        {
            context.academicRecords.Update(academicRecords);
        }

        public void Delete(AcademicRecords academicRecords)
        {
            context.academicRecords.Remove(academicRecords);
        }

        public AcademicRecords GetOne(int Id)
        {
            var AcademicRecords = context.academicRecords
                .Find(Id);
            return AcademicRecords;
        }

        public List<AcademicRecords> GetAll()
        {
            var AcademicRecords = context.academicRecords
                .ToList();
            return AcademicRecords;
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
