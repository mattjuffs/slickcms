using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SlickCMS.Data;
using SlickCMS.Data.Interfaces;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace SlickCMS.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Key for storing whether the User is logged in
        /// </summary>
        private readonly string _loggedInSessionKey = "AdminLoggedIn";

        /// <summary>
        /// Key for storing whether the User has completed the Login process (successful or not)
        /// </summary>
        private readonly string _loginSessionKey = "AdminLogin";

        public AdminController(IUserService userService)
        {
            _userService = userService;
        }

        private bool IsLoggedIn()
        {
            return _userService.IsAuthenticated(HttpContext.Session.GetString(_loggedInSessionKey));
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewData[_loginSessionKey] = (HttpContext.Session.GetString(_loginSessionKey) + "");
            HttpContext.Session.Remove(_loginSessionKey);

            return View();
        }

        [HttpPost]
        public IActionResult Login(IFormCollection form)
        {
            string email = form["email"];
            string password = form["password"];

            if (_userService.Login(email, password))
            {
                HttpContext.Session.SetString(_loginSessionKey, "success");// TODO: use this to display a successful login message on /admin/posts
                HttpContext.Session.SetString(_loggedInSessionKey, System.Guid.NewGuid().ToString());
                return Redirect("/admin/posts");
            }
            else
            {
                HttpContext.Session.SetString(_loginSessionKey, "error");
                return Redirect("/admin");
            }
        }

        [HttpGet]
        public IActionResult Posts()
        {
            if (!IsLoggedIn())
                return Redirect("/admin");

            // TODO: list posts

            return View();
        }

        [HttpGet]
        public IActionResult Post()
        {
            if (!IsLoggedIn())
                return Redirect("/admin");

            // TODO: show Post, editable (or add new post)

            return View();
        }

        [HttpPost]
        public IActionResult SavePost()
        {
            if (!IsLoggedIn())
                return Redirect("/admin");

            // TODO: save post data (add/update)

            return Redirect("/admin/posts");
        }

        [HttpGet]
        public IActionResult Categories()
        {
            if (!IsLoggedIn())
                return Redirect("/admin");

            // TODO: list categories

            return View();
        }

        [HttpGet]
        public IActionResult Tags()
        {
            if (!IsLoggedIn())
                return Redirect("/admin");

            // TODO: list tags

            return View();
        }
    }
}