using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace PlatinDashboard.Presentation.MVC.Helpers
{
    public static class PermissionHelper
    {
        public static MvcHtmlString IfClaimHelper(this MvcHtmlString value, string claimName, string claimValue)
        {
            //Esconde o elemento da página verificando os acessos do usuário
            return ValidatePermission(claimName, claimValue) ? value : MvcHtmlString.Empty;
        }

        public static MvcHtmlString IfClaimHelper(this MvcHtmlString value, string claimName, string claimValue, string companyAccess)
        {
            //Esconde o elemento da página verificando os acessos do usuário e a permissão da empresa no contexto
            return ValidatePermission(claimName, claimValue, companyAccess) ? value : MvcHtmlString.Empty;
        }

        public static bool IfClaim(this WebViewPage page, string claimName, string claimValue)
        {
            //Esconde o elemento da página verificando os acessos do usuário
            return ValidatePermission(claimName, claimValue);
        }

        public static bool IfClaim(this WebViewPage page, string claimName, string claimValue, string companyAccess)
        {
            //Esconde o elemento da página verificando os acessos do usuário e a permissão da empresa no contexto
            return ValidatePermission(claimName, claimValue, companyAccess);
        }

        private static bool ValidatePermission(string claimName, string claimValue)
        {
            //Esconde o elemento da página verificando os acessos do usuário
            var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
            var claim = identity.Claims.FirstOrDefault(c => c.Type == claimName);
            return claim != null && claim.Value.Contains(claimValue);
        }

        private static bool ValidatePermission(string claimName, string claimValue, string companyAccess)
        {
            //Esconde o elemento da página verificando os acessos do usuário e a permissão da empresa no contexto
            var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
            var claim = identity.Claims.FirstOrDefault(c => c.Type == claimName);
            var claimCompany = identity.Claims.FirstOrDefault(c => c.Type == "CompanyAccess");
            return claim != null && claim.Value.Contains(claimValue) && claimCompany != null && claimCompany.Value.Contains(companyAccess);
        }
    }
}