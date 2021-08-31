using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace multiple_image_upload_AspNetCore.ViewModels
{
    public class UploadViewModel
    {
        [Required(ErrorMessage = "Please enter your name.")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please select your image.")]
        [Display(Name = "Upload image")]
        public List<IFormFile> Image { get; set; }
    }
}
