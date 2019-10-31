using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GineCore.Web.Areas.Api
{
    [Area("Api")]
    public class HomeController: Controller
    {
        public IActionResult Start()
        {
            return Json(new { message = "api开启"});
        }
    }
}
