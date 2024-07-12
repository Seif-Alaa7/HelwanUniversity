﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Faculty
    {
        public int Id { get; set; }
        public int DeanId {  get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public string Picture { get; set; }
        public string Description { get; set; }
        public int ViewCount { get; set; }

        public List<Department> Departments { get; set; } = new List<Department>();
        public HighBoard HighBoard { get; set; } = null!;
    }
}
