using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SlickCMS.Web.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            // TODO: login form

            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // TODO: check login is successful

            return Redirect("/admin/posts");
        }

        [HttpGet]
        public IActionResult Posts()
        {
            // TODO: list posts

            return View();
        }

        [HttpGet]
        public IActionResult Post()
        {
            // TODO: show Post, editable (or add new post)

            return View();
        }

        [HttpPost]
        public IActionResult SavePost()
        {
            // TODO: save post data (add/update)

            return Redirect("/admin/posts");
        }

        [HttpGet]
        public IActionResult Categories()
        {
            // TODO: list categories

            return View();
        }

        [HttpGet]
        public IActionResult Tags()
        {
            // TODO: list tags

            return View();
        }
    }
}