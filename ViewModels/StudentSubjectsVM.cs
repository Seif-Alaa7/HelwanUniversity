using Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ViewModels
{
    internal class StudentSubjectsVM
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
    }
}
