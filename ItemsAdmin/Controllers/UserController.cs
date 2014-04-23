using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using MonsterAdmin.Models;
using MonsterAdmin.ViewModel;
using MonsterAdmin.Filters;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;


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

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ValidateEmail(RegisterUser user)
        {
            user.Email = "test123@test.com";
            user.Password = "test123";
         
            WebSecurity.CreateUserAndAccount(user.Email, user.Password, propertyValues: new { 
                            Password = user.Password,
                            UserID = user.UserId });
            return Json(true);

        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
