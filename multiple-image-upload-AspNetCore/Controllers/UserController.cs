using Microsoft.AspNetCore.Mvc;
using multiple_image_upload_AspNetCore.Models.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace multiple_image_upload_AspNetCore.Controllers
{
    public class UserController : Controller
    {
        private readonly MultipleImageUploadContext _db;

        public UserController(MultipleImageUploadContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var user = _db.Users.ToList();
            return View(user);
        }
    }
}
