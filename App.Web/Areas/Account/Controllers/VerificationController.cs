using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Core.Data;
using App.Core.Services;
using App.Core.ViewModel;
using WebMatrix.WebData;

namespace App.Web.Areas.Account.Controllers
{
    [Authorize]
    public class VerificationController : Controller
    {
        private readonly IUserService userService;

        public VerificationController(IUserService userService)
        {
            this.userService = userService;
        }

        //
        // GET: /Account/Verify/
        public ActionResult Index()
        {
            Membership membership = userService.GetMembership(User.Identity.Name);
            return View(membership);
        }

        [HttpPost, ActionName("SendEmail")]
        public JsonResult SendVerificationEmail()
        {
            try
            {
                userService.SendAccountActivationMail(User.Identity.Name);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            return Json(true);
        }

        [AllowAnonymous]
        public ActionResult Success(string type = null)
        {
            ViewBag.Message = "Your email is successfully verified.";
            return View();
        }


        [AllowAnonymous]
        public ActionResult Confirmation(Guid? guid)
        {
            if (!guid.HasValue)
            {
                ViewBag.Message = "Activation code is invalid.";
                return RedirectToAction("Index", "Error", new { area = "" });
            }
            try
            {
                WebSecurity.ConfirmAccount(guid.Value.ToString());
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return RedirectToAction("Index", "Error", new { area = "" });
            }

            return RedirectToAction("Success", "Verification", new { area = "Account" });

            //var membership = this.userService.GetMembershipByConfirmToken(guid.Value.ToString(), withUserProfile: true);
            //WebSecurity.Login(membership.UserProfile.UserName, membership.EmailConfirmationToken);

            //return RedirectToAction("Index", "Verification", new { area = "Account" });
        }
    }
}