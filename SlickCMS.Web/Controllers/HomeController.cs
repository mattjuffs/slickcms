using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SlickCMS.Web.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SlickCMS.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _config;
        public IHostingEnvironment HostingEnvironment { get; }

        public HomeController(IConfiguration config, IHostingEnvironment env)
        {
            this._config = config;
            this.HostingEnvironment = env;
        }

        public IActionResult Index()
        {
            var siteName = _config.GetValue<string>("SlickCMS:SiteName", "Unknown");
            ViewData["MetaTitle"] = siteName;

            ViewData["HostingEnvironment"] = this.HostingEnvironment.ContentRootPath;

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
            ViewData["MetaTitle"] = $"{siteName}|{logLevel}";
            ViewData["SiteName"] = siteName;
            ViewData["LogLevel"] = logLevel;
            ViewData["SectionExists"] = sectionExists;
            ViewData["Children"] = children.Count();

            return View();
        }

        [Route("framework")]
        public IActionResult Framework()
        {
            ViewData["MetaTitle"] = "Framework";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
