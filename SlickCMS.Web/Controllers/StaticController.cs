using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SlickCMS.Data;
using System.Web;

namespace SlickCMS.Web.Controllers
{
    public class StaticController : BaseController
    {
        private readonly SlickCMSContext _context;

        public StaticController(SlickCMSContext context) : base(context)
        {
            this._context = context;

            base.LoadCategories();
        }

        public IActionResult About()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Contact()
        {
            // exposing Session to the view using ViewData
            ViewData["ContactFormResult"] = (HttpContext.Session.GetString("ContactFormResult") + "");
            HttpContext.Session.Remove("ContactFormResult");

            return View();
        }

        [HttpPost]
        public IActionResult SaveContact(FormCollection form)
        {
            var contactModel = new Models.ContactModel
            {
                // FormCollection not available in MVC 6: https://stackoverflow.com/questions/35800809/asp-net-mvc-6-formcollection-not-being-populated-in-ajax-post
                Name = HttpUtility.HtmlEncode(form["contact-name"]),
                Email = form["contact-email"],// accepting raw email address
                Message = HttpUtility.HtmlEncode(form["contact-message"]),
            };

            // TODO: spam protection (recaptcha?)

            var successful = SlickCMS.Web.Global.SendEmail(contactModel);

            // working with Session in .NET Core https://benjii.me/2016/07/using-sessions-and-httpcontext-in-aspnetcore-and-mvc-core/
            HttpContext.Session.SetString("ContactFormResult", (successful ? "success" : "error"));

            return Redirect("/contact");
        }

        public IActionResult License()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Cookies()
        {
            return View();
        }

        public IActionResult Projects()
        {
            return View("Projects/Index");
        }

        [Route("framework")]
        public IActionResult Framework()
        {
            ViewData["Title"] = "Framework";
            return View("Projects/Framework");
        }
    }
}