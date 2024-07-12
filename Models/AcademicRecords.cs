using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class AcademicRecords
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public decimal GPASemester { get; private set; }
        public decimal GPATotal { get; private set; }
        public int CreditHours { get; private set; }
        public int RecordedHours { get; private set; }
        public int TotalHours { get; private set; }
        public decimal SemesterPoints { get; private set; }
        public decimal TotalPoints { get; private set; }
        public Level Level { get; private set; }
        public Semester Semester { get; private set; }
        public Student Student { get; set; } = null!;
    }
}
