using GineCore.Web.Areas.Base.Filter;
using GineCore.Web.Controllers.Base;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GineCore.Web.Areas.Api
{
    [EnableCors("any")]
    [Area("Api")]
    public class FileController : Controller
    {
        public IActionResult GetPic(string path)
        {
            var bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes, "image/jpeg");
        }

        [NoLogin]
        public async Task<IActionResult> SaveFile(IFormFile file)
        {
            string fileName = "Upload/" + Guid.NewGuid().ToString("n") + file.FileName;
            string path = Path.Combine(Environment.CurrentDirectory, fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return Content(fileName);
        }
    }
}
