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
using App.Core.Models;

namespace App.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService usersService;
        //private readonly IEmailService emailService;

        public AccountController(IUserService usersService)
        {
            this.usersService = usersService;
            //this.emailService = emailService;
        }

        //
        // GET: /Account/Register/
        public ActionResult Index()
        {
            return View();
        }
    }
}