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

        [HttpPost, AllowAnonymous]
        public JsonResult Index(RegisterModel model)
        {
            //TODO: Override OnActionExecuting
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {
                    var token = WebSecurity.CreateUserAndAccount(model.Email, model.Password, null, requireConfirmationToken: true);

                    //this.usersService.SendAccountActivationMail(model.Email);

                    //return RedirectToAction("success", "register", new { email = model.Email, area = "account" });

                    return Json(true);
                }
                catch (MembershipCreateUserException e)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new { Message = ErrorCodeToString(e.StatusCode) });
                }
                //catch (DbEntityValidationException ex)
                //{
                //    // Retrieve the error messages as a list of strings.
                //    var errorMessages = ex.EntityValidationErrors
                //            .SelectMany(x => x.ValidationErrors)
                //            .Select(x => x.ErrorMessage);

                //    // Join the list to a single string.
                //    var fullErrorMessage = string.Join("; ", errorMessages);

                //    // Combine the original exception message with the new one.
                //    var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                //    // Throw a new DbEntityValidationException with the improved exception message.
                //    throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
                //}
            }

            // If we got this far, something failed, redisplay form
            return Json(model);
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
