using System.Web;
using System.Web.Mvc;
using App.Web.Code.Attribute;

namespace App.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new MembershipAuthorize());
            //filters.Add(DependencyResolver.Current.GetService<App.Web.Code.Attribute.MembershipAuthorize>());
            filters.Add(new HandleErrorAttribute());
        }
    }
}