using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Enums
{

    public enum Grade
    {
        [Display(Name = "A+")]
        APlus,
        A,
        [Display(Name = "B+")]
        BPlus,
        B,
        [Display(Name = "C+")]
        CPlus,
        C,
        D,
        F
    }
}
