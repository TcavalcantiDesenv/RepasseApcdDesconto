using Microsoft.AspNet.Identity;
using PlatinDashboard.Application.Interfaces;
using PlatinDashboard.Application.ViewModels.CompanyViewModels;
using PlatinDashboard.Application.ViewModels.UserViewModels;
using System.IO;
using System.Web.Mvc;

namespace PlatinDashboard.Presentation.MVC.Controllers
{

    public class BaseController : Controller
    {
        protected readonly IUserAppService _userAppService;
        protected readonly ICompanyAppService _companyAppService;
        protected UserViewModel _loggedUser;
        protected CompanyViewModel _companyUser;

        public UserViewModel LoggedUser
        {
            get
            {
                if (Session["userId"] == null)
                {
                    return _userAppService.GetById(User.Identity.GetUserId());
                }
                else
                {
                    return _userAppService.GetById(Session["userId"].ToString());
                }
            }
            set
            {
                _loggedUser = value;
            }
        }

        public CompanyViewModel CompanyUser
        {
            get
            {
                if (Session["changeCompanyId"] == null)
                {
                    var loggedUser = _userAppService.GetById(User.Identity.GetUserId());
                    return _companyAppService.GetById(loggedUser.CompanyId);
                }
                else
                {
                    var loggedUser = _userAppService.GetById(Session["userId"].ToString());
                    return _companyAppService.GetById(loggedUser.CompanyId);
                }
            }
            set
            {
                _companyUser = value;
            }
        }

        public BaseController(IUserAppService userAppService, ICompanyAppService companyAppService)
        {
            _userAppService = userAppService;
            _companyAppService = companyAppService;
        }

        protected string RenderPartialView(string viewName, object model)
        {
            ViewData.Model = model;
            using (var writer = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, writer);
                viewResult.View.Render(viewContext, writer);
                return writer.GetStringBuilder().ToString();
            }
        }

        protected ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        public bool VerifyUserCompanyAccess(string userId)
        {
            //método para verificar se o usuário logado tem acesso pra editar outro usuário
            //Usuário Platin pode editar qualquer outro usuário
            if (CompanyUser.CompanyType == "Master")
            {
                return true;
            }
            //Os demais usuários só podem editar usuários da própia empresa
            var user = _userAppService.GetById(userId);
            if (LoggedUser.CompanyId != user.CompanyId || LoggedUser.UserType == "Comum")
            {
                return false;
            }
            if (LoggedUser.UserType == "Subadmin" && (user.UserType == "Subadmin" || user.UserType == "Manager"))
            {
                return false;
            }
            return true;
        }
    }
}