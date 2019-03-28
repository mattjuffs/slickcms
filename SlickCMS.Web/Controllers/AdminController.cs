﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SlickCMS.Data;
using SlickCMS.Data.Interfaces;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace SlickCMS.Web.Controllers
{
    public class AdminController : BaseController
    {
        private readonly IConfiguration _config;
        private readonly SlickCMSContext _context;
        private readonly IUserService _userService;
        private readonly IPostService _postService;

        /// <summary>
        /// Key for storing whether the User is logged in
        /// </summary>
        private readonly string _loggedInSessionKey = "AdminLoggedIn";

        /// <summary>
        /// Key for storing whether the User has completed the Login process (successful or not)
        /// </summary>
        private readonly string _loginSessionKey = "AdminLogin";

        public AdminController(IConfiguration config, SlickCMSContext context, IUserService userService, IPostService postService) : base(context)
        {
            _config = config;
            _context = context;

            _userService = userService;
            _postService = postService;
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
        public IActionResult Posts(int page = 1, bool viewall = false)
        {
            if (!IsLoggedIn())
                return Redirect("/admin");

            int take = _config.GetValue<int>("SlickCMS:AdminPostsPerPage", 20);
            if (viewall) { take = _config.GetValue<int>("SlickCMS:ViewAllCount", 1000); }
            int totalPosts = _postService.TotalPostsForAdmin();
            int remainder = (totalPosts % take);
            int totalPages = (totalPosts - remainder) / take;

            var posts = _postService.GetForAdmin(page, take);

            var postsModel = new Models.PostsModel
            {
                Name = "Posts",
                Posts = posts,
                Pagination = new Models.PaginationModel
                {
                    TotalPages = totalPages,
                    CurrentPage = page,
                    TotalPosts = totalPosts,
                    Query = ""
                },
                Type = Models.PostsModel.PageType.Post
            };

            return View(postsModel);
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