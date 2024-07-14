using Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class StudentSubjects
    {
        private decimal _DegreePoints;

        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public int? Degree { get; set; }
        public Grade? Grade { get; private set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal DegreePoints
        {
            get => _DegreePoints;
            set => _DegreePoints = Math.Round(value, 3);
        }
        public Student Student { get; set; } = null!;
        public Subject Subject { get; set; } = null!;

    }
}
