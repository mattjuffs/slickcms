using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SlickCMS.Web.Controllers
{
    public class StaticController : Controller
    {
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult License()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Cookies()
        {
            return View();
        }

        public IActionResult Projects()
        {
            return View("Projects/Index");
        }

        [Route("framework")]
        public IActionResult Framework()
        {
            ViewData["MetaTitle"] = "Framework";
            return View("Projects/Framework");
        }
    }
}