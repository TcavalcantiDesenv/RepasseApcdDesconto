using System.Security.Claims;
using System.Security.Principal;

namespace PlatinDashboard.Presentation.MVC.Helpers
{
    public static class IdentityHelper
    {
        public static string GetFisrtName(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("FirstName");
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string GetLastName(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("LastName");
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string GetFullName(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("FullName");
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string GetUserType(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("UserType");
            return (claim != null) ? claim.Value : string.Empty;
        }
    }
}