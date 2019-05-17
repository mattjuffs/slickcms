using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SlickCMS.Data;
using SlickCMS.Data.Interfaces;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SlickCMS.Core;

namespace SlickCMS.Web.Controllers
{
    public class AdminController : BaseController
    {
        private readonly IConfiguration _config;
        private readonly SlickCMSContext _context;
        private readonly IUserService _userService;
        private readonly IPostService _postService;
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;

        /// <summary>
        /// Key for storing whether the User is logged in
        /// </summary>
        private readonly string _loggedInSessionKey = "AdminLoggedIn";

        /// <summary>
        /// Key for storing whether the User has completed the Login process (successful or not)
        /// </summary>
        private readonly string _loginSessionKey = "AdminLogin";

        public AdminController(IConfiguration config, SlickCMSContext context, IUserService userService, IPostService postService, ICategoryService categoryService, ITagService tagService) : base(context)
        {
            _config = config;
            _context = context;

            _userService = userService;
            _postService = postService;
            _categoryService = categoryService;
            _tagService = tagService;
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
            if (remainder > 0) totalPages++;

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
        public IActionResult Post(int id)
        {
            if (!IsLoggedIn())
                return Redirect("/admin");

            var post = _postService.GetPost(id);
            var categories = _categoryService.GetCategories(post.PostId);
            var tags = _tagService.GetTags(post.PostId);

            var postModel = new Models.PostModel
            {
                Post = post,
                Comments = null,// not loading comments on the Admin page
                Categories = categories,
                Tags = tags,
            };

            return View(postModel);
        }

        [HttpPost]
        [Route("/admin/save-post")]
        public IActionResult SavePost(IFormCollection form)
        {
            if (!IsLoggedIn())
                return Redirect("/admin");

            // retrieve user input from the form
            int? postID = Convert.ToInt32(form["hdnPostId"]);
            int? userID = Convert.ToInt32(form["hdnUserId"]);
            string title = form["txtTitle"];
            string url = form["txtUrl"];
            string summary = form["txtSummary"];
            string content = form["txtContent"];
            string search = form["txtSearch"];
            DateTime dateCreated = String.IsNullOrEmpty(form["txtDateCreated"]) ? DateTime.Now : form["txtDateCreated"].ToDateTime();
            DateTime dateModified = DateTime.Now;
            bool published = (form["chkPublished"] == "1");// TODO: confirm values passed
            bool pageable = (form["chkPageable"] == "1");// TODO: confirm values passed

            // create Post object
            var post = new SlickCMS.Data.Entities.Post
            {
                PostId = postID ?? 0,
                UserId = userID ?? 0,// TODO: get from logged in user
                Title = title,
                // TODO: populate remaining fields
            };

            // save post
            if (postID == null || postID <= 0)
            {
                // TODO: add
            }
            else
            {
                // TODO: update
            }
            // TODO: set postID from add/update DB calls

            // TODO: save Categories
            //    tags will be comma separated list within form

            // TODO: save Tags
            //    replace " " with "-", so "#star wars" becomes "#star-wars"
            //    all tags begin with #, but strip # out when saving to DB

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