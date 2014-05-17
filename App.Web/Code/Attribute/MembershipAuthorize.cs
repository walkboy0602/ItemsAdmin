using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Core.Services;
using Microsoft.Practices.Unity;
using WebMatrix.WebData;
using System.Web.Routing;
using App.Web.Code.Membership;

namespace App.Web.Code.Attribute
{
    public class MembershipAuthorize : AuthorizeAttribute
    {
        [Dependency]
        public IUserService userService { get; set; }

        public override void OnAuthorization(AuthorizationContext context)
        {
            base.OnAuthorization(context);

            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                //var membership = userService.GetMembership(context.HttpContext.User.Identity.Name);

                var identity = context.HttpContext.User.ToCustomPrincipal().CustomIdentity;

                if (!identity.isEmailConfirmed)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        action = "Index",
                        controller = "Verification",
                        area = "Account"
                    }));
                }
            }
        }

    }
}