using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Core.Data;
using App.Core.Services;
using System.Web.Security;
using WebMatrix.WebData;

namespace App.Web.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IUserService usersService;
        //private readonly IEmailService emailService;

        public RegisterController(IUserService usersService)
        {
            this.usersService = usersService;
            //this.emailService = emailService;
        }

        //
        // GET: /Register/
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Index(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {
                    var token = WebSecurity.CreateUserAndAccount(model.Email, model.Password, null, requireConfirmationToken: true);

                    //this.usersService.SendAccountActivationMail(model.Email);

                    return RedirectToAction("success", "register", new { email = model.Email, area = "account" });
                }
                catch (MembershipCreateUserException e)
                {
                    //ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

    }
}
