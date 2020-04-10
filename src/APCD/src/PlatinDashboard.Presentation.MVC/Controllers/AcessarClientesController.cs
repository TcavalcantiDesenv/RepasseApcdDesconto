using PlatinDashboard.Application.Administrativo.Interfaces;
using PlatinDashboard.Application.Farmacia.Interfaces;
using PlatinDashboard.Application.Interfaces;
using PlatinDashboard.Application.ViewModels.CompanyViewModels;
using PlatinDashboard.Application.ViewModels.StoreViewModels;
using PlatinDashboard.Application.ViewModels.UserViewModels;
using PlatinDashboard.Infra.CrossCutting.Identity.Configuration;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PlatinDashboard.Presentation.MVC.Controllers
{
    public class AcessarClientesController : BaseController
    {

        public AcessarClientesController(IUserAppService userAppService,
                                  ICompanyAppService companyAppService)
                                  : base(userAppService, companyAppService)
        {    }

        public JsonResult ListarCompanias()
        {
            var data = _companyAppService.GetAll();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AlterarCompania(int CompanyId, string CompanyName)
        {
            var AlterarUsuarioCompania = AlterarVinculoUsuarioCompania(CompanyId, CompanyName);
            return Json(AlterarUsuarioCompania, JsonRequestBehavior.AllowGet);
        }

        private bool AlterarVinculoUsuarioCompania(int CompanyId, string CompanyName)
        {
            var changeConnectionConfiguration = false;
            var usuarioViewModel = ((List<UserViewModel>)_userAppService.GetByCompany(CompanyId)).Find(p => CompanyName.Contains(p.Name));

            if (usuarioViewModel != null)
            {
                Session["userId"] = usuarioViewModel. UserId;
                Session["changeCompanyId"] = usuarioViewModel.CompanyId;
                Session["Nome"] = usuarioViewModel.Name;
                Session["Sobrenome"] = usuarioViewModel.LastName;
                Session["Loja"] = CompanyName;

                LoggedUser = usuarioViewModel;
                CompanyUser = _companyAppService.GetById(CompanyId);
                changeConnectionConfiguration = (_companyAppService.Update(_companyAppService.GetById(CompanyId)) != null);
            }

            return changeConnectionConfiguration;
        }

        public JsonResult logoutClient()
        {
            Session.Remove("userId");
            Session.Remove("changeCompanyId");

            return Json(true, JsonRequestBehavior.AllowGet);
        }

    }
}