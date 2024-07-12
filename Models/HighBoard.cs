using Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class HighBoard
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public JobTitle JobTitle { get; set; }
        public string? Picture { get; set; }
        public Faculty Faculty { get; set; } = null!;
        public Department Department { get; set; } = null!;
    }
}
