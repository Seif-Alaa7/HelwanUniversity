using HelwanUniversity.Vaildations;
using Microsoft.AspNetCore.Http;
using Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.UniFileVMs
{
    public class UniFileVM2
    {
        //Edit
        public int Id { get; set; }
        public string File { get; set; } = null!;
        [NotMapped]
        public IFormFile? ImgPath { get; set; } = null!;
        [NotMapped]
        public Filetype ContentType { get; set; }
    }
}
