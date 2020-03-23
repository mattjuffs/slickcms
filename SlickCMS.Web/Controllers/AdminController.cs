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

        private readonly string _loggedInUserId = "AdminUserID";

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
            var user = _userService.Login(email, password);

            if (user != null)
            {
                HttpContext.Session.SetString(_loginSessionKey, "success");// TODO: use this to display a successful login message on /admin/posts
                HttpContext.Session.SetString(_loggedInSessionKey, System.Guid.NewGuid().ToString());
                HttpContext.Session.SetInt32(_loggedInUserId, user.UserId);
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
            var categories = _categoryService.GetCategories(id);
            var tags = _tagService.GetTags(id);

            var allCategories = SlickCMS.Core.Caching.MemoryCache.Get<List<SlickCMS.Data.Entities.CategorySummary>>("Categories|Posts");
            var allTags = SlickCMS.Core.Caching.MemoryCache.Get<List<SlickCMS.Data.Entities.TagSummary>>("Tags");

            var postModel = new Models.PostModel
            {
                Post = post,
                Comments = null,// not loading comments on the Admin page
                Categories = categories,
                Tags = tags,
                AllCategories = allCategories ?? new List<Data.Entities.CategorySummary>(),
                AllTags = allTags ?? new List<Data.Entities.TagSummary>(),
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
            int userID = Convert.ToInt32(form["hdnUserId"]);
            string title = form["txtTitle"];
            string url = form["txtUrl"];
            string summary = form["txtSummary"];
            string content = form["txtContent"];
            string search = form["txtSearch"];
            DateTime dateCreated = form["hdnDateCreated"].ToString().ToDateTime();
            DateTime dateModified = DateTime.Now;
            bool published = (form["chkPublished"] == "on");
            bool pageable = (form["chkPageable"] == "on");

            int loggedInUserID = HttpContext.Session.GetInt32(_loggedInUserId) ?? 0;

            // create Post object
            var post = new SlickCMS.Data.Entities.Post
            {
                PostId = postID ?? 0,
                UserId = userID != 0 ? userID : loggedInUserID,
                Title = title,
                Content = content,
                DateCreated = dateCreated,
                DateModified = dateModified,
                Pageable = pageable ? 1 : 0,
                Published = published ? 1 : 0,
                Search = search,
                Summary = summary,
                Url = url,
            };

            // save post
            if (postID == null || postID == 0)
            {
                //_context.Post.Add(post);
                _context.Set<Data.Entities.Post>().Add(post);

                postID = post.PostId;
            }
            else
            {
                //_context.Post.Update(post);
                _context.Set<Data.Entities.Post>().Update(post);
            }
            _context.SaveChanges();

            SaveCategories(postID, form);
            SaveTags(postID, form);

            return Redirect("/admin/posts");
        }

        private void SaveCategories(int? postID, IFormCollection form)
        {
            var currentCategories = _categoryService.GetCategories(postID ?? 0);// Categories currently assigned to the Post

            if (form["selCategories"] != "")
            {
                string[] newCategories = form["selCategories"].ToString().Split(',');// new associations

                // add new associations
                foreach (var newCategory in newCategories)
                {
                    // check if we already have this association
                    var currentCategory = currentCategories.Where(p => p.CategoryId == newCategory.ToInt()).FirstOrDefault();
                    if (currentCategory != null)
                    {
                        currentCategories.Remove(currentCategory);
                        continue;
                    }

                    var relationship = new SlickCMS.Data.Entities.Relationship
                    {
                        PostId = postID ?? 0,
                        CategoryId = newCategory.ToInt(),
                    };
                    _context.Set<Data.Entities.Relationship>().Add(relationship);
                }

                // remove old associations
                foreach (var currentCategory in currentCategories)
                {
                    var currentRelationship = _context.Set<Data.Entities.Relationship>().Where(r => r.CategoryId == currentCategory.CategoryId && r.PostId == postID);
                    _context.Set<Data.Entities.Relationship>().RemoveRange(currentRelationship);
                }
            }
            else
            {
                foreach (var currentCategory in currentCategories)
                {
                    var currentRelationship = _context.Set<Data.Entities.Relationship>().Where(r => r.CategoryId == currentCategory.CategoryId && r.PostId == postID);
                    _context.Set<Data.Entities.Relationship>().RemoveRange(currentRelationship);
                }
            }

            _context.SaveChanges();
        }

        private void SaveTags(int? postID, IFormCollection form)
        {
            //    replace " " with "-", so "#star wars" becomes "#star-wars"
            //    all tags begin with #, but strip # out when saving to DB

            var currentTags = _tagService.GetTags(postID ?? 0);// Tags currently assigned to the post
            if (form["txtTags"] != "")
            {
                string[] newTags = form["txtTags"].ToString().Split('#');

                foreach (var newTag in newTags)
                {
                    if (newTag.Length == 0)
                        continue;

                    string formattedTag = newTag.Trim().ToLower().Replace(" ", "-");

                    // check if we already have this association
                    var currentTag = currentTags.Where(p => p.Name == formattedTag).FirstOrDefault();
                    if (currentTag != null)
                    {
                        currentTags.Remove(currentTag);
                        continue;
                    }

                    // check if the tag already exists
                    var tag = _tagService.Get(p => p.Name == formattedTag);
                    if (tag == null)
                    {
                        // add a new tag
                        tag = new Data.Entities.Tag
                        {
                            Name = formattedTag,
                        };
                        _context.Set<Data.Entities.Tag>().Add(tag);
                        _context.SaveChanges();
                    }

                    // save the association
                    var relationship = new SlickCMS.Data.Entities.Relationship
                    {
                        PostId = postID ?? 0,
                        TagId = tag.TagId,
                    };
                    _context.Set<Data.Entities.Relationship>().Add(relationship);
                }

                // remove old associations
                foreach (var currentTag in currentTags)
                {
                    var currentRelationship = _context.Set<Data.Entities.Relationship>().Where(r => r.CategoryId == currentTag.TagId && r.PostId == postID);
                    _context.Set<Data.Entities.Relationship>().RemoveRange(currentRelationship);
                }
            }
            else
            {
                foreach (var currentTag in currentTags)
                {
                    var currentRelationship = _context.Set<Data.Entities.Relationship>().Where(r => r.TagId == currentTag.TagId && r.PostId == postID);
                    _context.Set<Data.Entities.Relationship>().RemoveRange(currentRelationship);
                }
            }

            _context.SaveChanges();
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