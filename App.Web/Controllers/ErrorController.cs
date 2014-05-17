using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/
        public ActionResult Index(string errorType = null)
        {
            ViewBag.Message = "Your email confirmation link does not appear to be correct.";
            return View();
        }
    }
}