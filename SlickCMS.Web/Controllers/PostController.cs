using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SlickCMS.Data;
using SlickCMS.Data.Interfaces;
using System;
using System.Web;

namespace SlickCMS.Web.Controllers
{
    public class PostController : BaseController
    {
        private readonly IConfiguration _config;
        private readonly SlickCMSContext _context;
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;
        private readonly ICategoryService _categoryService;

        public PostController(IConfiguration config, SlickCMSContext context, IPostService postService, ICommentService commentService, ICategoryService categoryService) : base(context)
        {
            _config = config;
            _context = context;
            _postService = postService;
            _commentService = commentService;
            _categoryService = categoryService;

            base.LoadCategories();
        }

        public IActionResult Index(string url)
        {
            if (url == string.Empty)
                url = HttpContext.Request.Path.ToString();

            // https://stackoverflow.com/questions/41577376/how-to-read-values-from-the-querystring-with-asp-net-core
            /*if (HttpContext.Request.QueryString("url") != "")
                url = Request.QueryString("url");*/

            var post = _postService.GetPost(url);
            var comments = _commentService.GetPublished(post.PostId);
            var categories = _categoryService.GetCategories(post.PostId);

            var postModel = new Models.PostModel
            {
                Post = post,
                Comments = comments,
                Categories = categories,
            };

            return View(postModel);
        }

        [HttpPost]
        public IActionResult SaveComment(IFormCollection form)
        {
            bool autoPublish = _config.GetValue<bool>("SlickCMS:AutoPublishComments", false);

            var referer = (HttpContext.Request.Headers[Microsoft.Net.Http.Headers.HeaderNames.Referer] + "");

            var comment = new SlickCMS.Data.Entities.Comment
            {
                //CommentId = 0,
                PostId = Convert.ToInt32(form["comment-postid"].ToString()),
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                Content = HttpUtility.HtmlEncode(form["comment-message"]),
                Email = form["comment-email"],
                Name = HttpUtility.HtmlEncode(form["comment-name"]),
                Published = (autoPublish ? 1 : 0),
                HttpUserAgent = (Request.Headers[Microsoft.Net.Http.Headers.HeaderNames.UserAgent] + ""),
                Ip = HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString(),
                Url = "",// TODO: add to Comments Form
                UserId = 0,// TODO: add User Login
            };

            _commentService.Add(comment);

            // TODO: show success/fail message on redirected page

            // TODO: if there's no referer, use the PostID to determine the URL for the post

            return Redirect((referer != "") ? (referer + "#comment-form") : "/");
        }
    }
}