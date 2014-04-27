using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MonsterAdmin
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Merchant",
            //    url: "Merchant/{controller}/{action}/{id}",
            //    defaults: new { controller = "Listing", action = "Index", id = UrlParameter.Optional }
            //);

            //-- signup Route
            routes.MapRoute(
                name: "SignUp",
                url: "SignUp",
                defaults: new { controller = "Home", action = "SignUp" }
            );

            routes.MapRoute(
                name: "routeWithController",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );




        }
    }
}