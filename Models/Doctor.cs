using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public Gender Gender { get; set; }
        public Religion Religion { get; set; }
        public string? Picture { get; set; }
        public string? Address { get; set; }
        public JobTitle JobTitle { get; set; }
        public List<Subject> Subjects { get; set; } = new List<Subject>();

    }
}
