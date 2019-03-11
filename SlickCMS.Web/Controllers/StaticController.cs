﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Web;

namespace SlickCMS.Web.Controllers
{
    public class StaticController : Controller
    {
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
        public IActionResult ProcessContact(FormCollection form)
        {
            var contactModel = new Models.ContactModel
            {
                // FormCollection not available in MVC 6: https://stackoverflow.com/questions/35800809/asp-net-mvc-6-formcollection-not-being-populated-in-ajax-post
                Name = HttpUtility.HtmlEncode(form["Name"]),
                Email = form["Email"],// accepting raw email address
                Message = HttpUtility.HtmlEncode(form["Message"]),
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
            ViewData["MetaTitle"] = "Framework";
            return View("Projects/Framework");
        }
    }
}