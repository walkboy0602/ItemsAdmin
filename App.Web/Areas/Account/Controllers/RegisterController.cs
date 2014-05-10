using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using App.Core.Data;
using App.Core.Services;
using System.Web.Security;
using WebMatrix.WebData;
using System.Data.Entity.Validation;
using App.Web.Areas.Account.Models;

namespace App.Web.Areas.Account.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IUserService userService;
        //private readonly IEmailService emailService;

        public RegisterController(IUserService userService)
        {
            this.userService = userService;
            //this.emailService = emailService;
        }

        //
        // GET: /Account/Register/
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Index(RegisterModel model)
        {
            //TODO: Override OnActionExecuting
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {

                    var token = WebSecurity.CreateUserAndAccount(model.Email, model.Password, 
                        propertyValues: new 
                        {  
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            Mobile = model.Mobile
                        }, requireConfirmationToken: true);

                    //this.userService.SendAccountActivationMail(model.Email);

                    return RedirectToAction("success", "register", new { email = model.Email, area = "account" });
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        //
        // GET: /Account/Register/Success

        public ActionResult Success(string email)
        {
            ViewData["email"] = email;
            return View();
        }


        #region Helpers

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }

        #endregion
	}
}