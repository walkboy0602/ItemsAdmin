using System.Security.Principal;

namespace App.Web.Code.Membership
{
    public static class MembershipExtentions
    {
        public static CustomPrincipal ToCustomPrincipal(this IPrincipal principal)
        {
            return (CustomPrincipal)principal;
        }
    }
}