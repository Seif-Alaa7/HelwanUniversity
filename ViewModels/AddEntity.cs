using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class AddEntity
    {
        public string EntityType { get; set; } = null!;

        // Common properties
        public DepartmentVM Department { get; set; }
        public FacultyVM Faculty { get; set; }
        public SubjectVM Subject { get; set; }
        public string Name { get; set; } = null!;

        // Department-specific properties
        public int? HeadId { get; set; }
        public int? FacultyId { get; set; }
        public int? Allowed { get; set; }

        // Faculty-specific properties
        public int? DeanId { get; set; }
        public string? Logo { get; set; }
        public string? Picture { get; set; }
        public string? Description { get; set; }
        public int? ViewCount { get; set; }

        // Subject-specific properties
        public int? DepartmentId { get; set; }
        public int? DoctorId { get; set; }
        public int? SubjectHours { get; set; }
        public int? StudentsAllowed { get; set; }
        public Models.Enums.Level? Level { get; set; }
        public Models.Enums.Semester? Semester { get; set; }
        public Models.Enums.SummerStatus? SummerStatus { get; set; }
        public Models.Enums.SubjectType? SubjectType { get; set; }
        public int? Salary { get; set; }
    }
}
