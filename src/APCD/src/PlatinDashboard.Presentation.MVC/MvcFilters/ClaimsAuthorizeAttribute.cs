using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PlatinDashboard.Presentation.MVC.MvcFilters
{
    public class ClaimsAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly string _claimName;
        private readonly string _claimValue;
        private readonly string _companyAccess;
        //private string _actionName = "Forbidden";
        //private string _controllerName = "Errors";

        public ClaimsAuthorizeAttribute(string claimName, string claimValue)
        {
            _claimName = claimName;
            _claimValue = claimValue;
        }

        public ClaimsAuthorizeAttribute(string claimName, string claimValue, string companyAccess)
        {
            _claimName = claimName;
            _claimValue = claimValue;
            _companyAccess = companyAccess;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var identity = (ClaimsIdentity)httpContext.User.Identity;
            var claim = identity.Claims.Where(c=> _claimName.Split(",".ToCharArray()).Contains(c.Type)).FirstOrDefault();
            var claimCompany = identity.Claims.FirstOrDefault(c => c.Type == "CompanyType");

            //Verifica o acesso de Companias 
            if (claimCompany != null)
                return true;

            if (_companyAccess != null)
            {
                //Verifica o acesso do usuário e da empresa do usuário
                if (claim != null)
                {
                    var claimValues = _claimValue.Split(',');
                    //return claim.Value.Contains(_claimValue) && claimCompany.Value.Contains(_companyAccess);
                    return claimValues.Contains(claim.Value) && claimCompany.Value.Contains(_companyAccess);
                }
            }
            else
            {
                //Se caso não for passado o parametro de acesso da empresa ele só irá verificar o acesso do usuário
                if (claim != null)
                {
                    var claimValues = _claimValue.Split(',');
                    return claimValues.Contains(claim.Value);
                }
            }
            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //Caso o usuário não esteja logado ele será redirecionado para a página de login
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    {"action", "Index"},
                    {"controller", "Acesso"}
                });
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    {"action", "Forbidden"},
                    {"controller", "Errors"}
                });
            }
        }
    }
}