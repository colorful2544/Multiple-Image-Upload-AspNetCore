using System;
using System.Collections.Generic;

#nullable disable

namespace multiple_image_upload_AspNetCore.Models.db
{
    public partial class User
    {
        public User()
        {
            Images = new HashSet<Image>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Image> Images { get; set; }
    }
}
