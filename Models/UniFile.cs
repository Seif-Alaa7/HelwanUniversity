using Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class UniFile
    {
        public int Id { get; set; }
        public string File { get; set; } = null!;
        public type ContentType  { get; set; }
    }
}
