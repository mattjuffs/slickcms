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
        public IActionResult SaveContact(IFormCollection form)
        {
            var contactModel = new Models.ContactModel
            {
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
            return View("Projects/Framework");
        }
    }
}