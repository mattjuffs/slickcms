using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SlickCMS.Data;
using SlickCMS.Data.Entities;
using SlickCMS.Data.Services;
using SlickCMS.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Web;
using Microsoft.Extensions.Configuration;

namespace SlickCMS.Web.Controllers
{
    public class PostController : Controller
    {
        private readonly IConfiguration _config;
        private readonly SlickCMSContext _context;
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;

        public PostController(IConfiguration config, SlickCMSContext context, IPostService postService, ICommentService commentService)
        {
            _config = config;
            _context = context;
            _postService = postService;
            _commentService = commentService;
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

            var postModel = new Models.PostModel
            {
                Post = post,
                Comments = comments,
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