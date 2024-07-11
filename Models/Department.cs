using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Department
    {
        public int Id { get; set; }
        public int HeadId { get; set; }
        public string Name { get; set; }
        public int FacultyId { get; set; }

        public Faculty Faculty { get; set; } = null!;
        public HighBoard HighBoard { get; set; } = null!;
        public List<Student> Students { get; set; } = new List<Student>();
        public List<DepartmentSubjects> DepartmentSubjects { get; set; } = new List<DepartmentSubjects>();

    }
}
