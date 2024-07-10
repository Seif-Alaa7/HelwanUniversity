using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Nationality { get; set; }
        public Gender Gender { get; set; }
        public Religion Religion { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int DepartmentId { get; set; }
        public string Picture { get; set; }
        public DateTime AdmissionDate { get; set; } = DateTime.Now;
        public bool PaymentFees { get; set; }

        public List<StudentSubjects> StudentSubjects { get; set; } = new List<StudentSubjects>();
        public Department Department { get; set; } = null!;
        public AcademicRecords AcademicRecords { get; set; } =null!;

    }
}
