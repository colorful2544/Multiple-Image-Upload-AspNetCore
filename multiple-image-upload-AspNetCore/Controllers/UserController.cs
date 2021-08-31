using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using multiple_image_upload_AspNetCore.Models.db;
using multiple_image_upload_AspNetCore.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
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
        public IActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(UploadViewModel data)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(data.Name) || data.Image == null) return View();

                var user = _db.Users.FirstOrDefault(u => u.Name == data.Name);

                if (user != null)
                {
                    List<string> imagesName = new List<string>();

                    foreach(var item in data.Image)
                    {
                        var fileName = Path.GetFileName(item.FileName);
                        var fileExt = Path.GetExtension(fileName);
                        var tmpName = Guid.NewGuid().ToString();
                        var newFileName = string.Concat(tmpName, fileExt);
                        var filePath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","images")).Root + $@"\{newFileName}";

                        using (FileStream fs = System.IO.File.Create(filePath))
                        {
                            item.CopyTo(fs);
                            fs.Flush();
                        }
                        imagesName.Add(newFileName);
                    }

                    foreach(var item in imagesName)
                    {
                        Image image = new Image
                        {
                            UserId = user.Id,
                            ImageName = item
                        };
                        await _db.Images.AddAsync(image);
                    }
                    await _db.SaveChangesAsync();
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    return NotFound();
                }
            }
            return View();
        }
        public IActionResult SeeImagesOfUser(string id)
        {
            var image = _db.Images.Where(i => i.UserId.ToString() == id).Include("User").ToList();
            return View(image);
        }
    }
}
