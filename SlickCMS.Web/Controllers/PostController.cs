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

namespace SlickCMS.Web.Controllers
{
    public class PostController : Controller
    {
        private readonly SlickCMSContext _context;
        private readonly IPostService _postService;

        public PostController(SlickCMSContext context, IPostService postService)
        {
            _context = context;
            _postService = postService;
        }

        public IActionResult Index(string url)
        {
            // TODO: setup route for urls, e.g. /post/url-goes-here or /yyyy-mm-dd/url-goes-here or /url-goes-here
            if (url == string.Empty)
                url = HttpContext.Request.Path.ToString();

            // https://stackoverflow.com/questions/41577376/how-to-read-values-from-the-querystring-with-asp-net-core
            /*if (HttpContext.Request.QueryString("url") != "")
                url = Request.QueryString("url");*/

            var post = _postService.GetPost(url);

            LoadComments(post.PostId);

            return View(post);
        }

        private void LoadComments(int postID)
        {
            // TODO
        }

        [HttpPost]
        public IActionResult SaveComment(FormCollection form)
        {
            return Redirect("/");// TODO: redirect back to post page with #comments or #comment-form
        }
    }
}