using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Web.Areas.Account.Models;
using WebMatrix.WebData;

namespace App.Web.Areas.Account.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Account/Login/
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public ActionResult Index(LoginModel model, string returnUrl)
        {

            if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
            {
                return Url.IsLocalUrl(returnUrl) ? (ActionResult)Redirect(returnUrl) : RedirectToAction("Index", "Category", new { area = "" });
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }
	}
}