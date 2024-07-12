using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class StudentSubjects
    {
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public int? Degree { get; set; }
        public Grade? Grade { get; private set; }
        public decimal DegreePoints { get; private set; }
        public Student Student { get; set; } = null!;
        public Subject Subject { get; set; } = null!;

    }
}
