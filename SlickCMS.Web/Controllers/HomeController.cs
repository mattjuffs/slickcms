using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SlickCMS.Data;
using SlickCMS.Web.Models;
using System.Diagnostics;
using System.Linq;
using SlickCMS.Data.Interfaces;

namespace SlickCMS.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IConfiguration _config;
        public IHostingEnvironment HostingEnvironment { get; }
        private readonly SlickCMSContext _context;
        private readonly IPostService _postService;

        public HomeController(IConfiguration config, IHostingEnvironment env, SlickCMSContext context, IPostService postService) : base(context)
        {
            this._config = config;
            this.HostingEnvironment = env;
            this._context = context;
            this._postService = postService;
        }

        public IActionResult Index()
        {
            var siteName = _config.GetValue<string>("SlickCMS:SiteName", "Unknown");
            ViewData["Title"] = siteName;

            //ViewData["HostingEnvironment"] = this.HostingEnvironment.ContentRootPath;

            //var connectionString = new SlickCMS.Core.ConnectionString();
            //ViewData["ConnectionString"] = connectionString.Get(this.HostingEnvironment.ContentRootPath);

            var posts = _postService.GetPublished(1, 5);
            ViewData["LatestPosts"] = posts;

            return View();
        }

        [Route("settings")]
        public IActionResult Settings()
        {
            // accessing Configuration from appsettings.json - https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-2.2

            var slickCmsConfigSection = _config.GetSection("SlickCMS");
            ViewData["SiteNameFromConfigSection"] = slickCmsConfigSection.GetValue<string>("SiteName");

            var loggingConfigSection = _config.GetSection("Logging:LogLevel");
            ViewData["LogLevelFromConfigSection"] = loggingConfigSection.GetValue<string>("Default");

            var siteName = _config.GetValue<string>("SlickCMS:SiteName", "Unknown");
            var logLevel = _config.GetValue<string>("Logging:LogLevel:Default", "Unknown");

            var sectionExists = _config.GetSection("Section:That:Does:Not:Exist").Exists();

            var children = loggingConfigSection.GetChildren();// NOTE: returns a collection of child children

            // NOTE: using ViewData over ViewBag https://stackoverflow.com/a/34644441/63100
            ViewData["Title"] = $"{siteName}|{logLevel}";
            ViewData["SiteName"] = siteName;
            ViewData["LogLevel"] = logLevel;
            ViewData["SectionExists"] = sectionExists;
            ViewData["Children"] = children.Count();

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
