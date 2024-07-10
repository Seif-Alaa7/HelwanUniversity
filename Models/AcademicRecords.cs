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
        public decimal GPASemester { get; set; }
        public decimal GPATotal { get; set; }
        public int CreditHours { get; set; }
        public int RecordedHours { get; set; }
        public int TotalHours { get; set; }
        public decimal SemesterPoints { get; set; }
        public decimal TotalPoints { get; set; }
        public Level Level { get; set; }
        public Semester Semester { get; set; }

        public Student Student { get; set; } = null!;

    }
}
