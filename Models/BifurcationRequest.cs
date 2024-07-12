using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class BifurcationRequest
    {
        public int Id { get; set; }
        public int StudentId {  get; set; }
        public int DepartmentId {  get; set; }
        public int Rank { get; set; }
        public Department Department { get; set; } = null!;
        public Student Student { get; set; } = null!;
    }
}
