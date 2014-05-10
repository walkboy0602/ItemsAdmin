using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Core.Services;
using Microsoft.Practices.Unity;
using WebMatrix.WebData;
using System.Web.Routing;

namespace App.Web.Code.Attribute
{
    public class MembershipAuthorize: AuthorizeAttribute
    {
        [Dependency]
        public IUserService userService { get; set; }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            int d = filterContext.HttpContext.User.Identity.GetHashCode();
            int userId = WebSecurity.GetUserId(filterContext.HttpContext.User.Identity.Name);

            var membership = userService.GetMembership(userId);

            if (!membership.IsEmailConfirmed)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    action = "Index",
                    controller = "Verification",
                    area = "Account"
                }));
            }
        }

    }
}