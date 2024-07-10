using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class HighBoard
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public JobTitle JobTitle { get; set; }
        public string Picture { get; set; }

    }
}
