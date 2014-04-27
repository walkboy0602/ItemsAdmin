using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using MonsterAdmin.Filters;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using App.Core.Data;

namespace MonsterAdmin.Controllers
{
    [InitializeSimpleMembership]
    public class UserController : Controller
    {
        private ShopDBEntities db = new ShopDBEntities();

        //
        // GET: /User/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ValidateEmail()
        {

            //WebSecurity.CreateUserAndAccount(user.Email, user.Password, propertyValues: new { 
            //                Password = user.Password,
            //                UserID = user.UserId });
            return Json(true);

        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
