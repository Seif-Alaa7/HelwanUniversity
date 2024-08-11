using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class Picture
    {
        public int Id { get; set; } 
        public string MainPicture { get; set; } = null!;
        [NotMapped]
        [Display(Name = "If you want Change The Image, Enter New Image here: ")]
        public IFormFile? MainPictureFile { get; set; }
    }
}
