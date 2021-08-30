using System;
using System.Collections.Generic;

#nullable disable

namespace multiple_image_upload_AspNetCore.Models.db
{
    public partial class Image
    {
        public int UserId { get; set; }
        public string ImageName { get; set; }

        public virtual User User { get; set; }
    }
}
