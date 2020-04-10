using Model.Neg;
using PlatinDashboard.Application.Administrativo.Interfaces;
using PlatinDashboard.Application.Farmacia.Interfaces;
using PlatinDashboard.Application.Interfaces;
using PlatinDashboard.Application.ViewModels.CompanyViewModels;
using PlatinDashboard.Application.ViewModels.StoreViewModels;
using PlatinDashboard.Application.ViewModels.UserViewModels;
using PlatinDashboard.Infra.CrossCutting.Identity.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PlatinDashboard.Presentation.MVC.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        LojasNeg objLojasNeg;
        MaconNeg objMaconNeg;
        GraduacaoNeg objGraduacaoNeg;

        public HomeController(IUserAppService userAppService,
                                  ICompanyAppService companyAppService)
                                  : base(userAppService, companyAppService)
        {
            objMaconNeg = new MaconNeg();
            objLojasNeg = new LojasNeg();
            objGraduacaoNeg = new GraduacaoNeg();
        }

        public ActionResult Index()
        {
            var ObjAcessosNeg = new AcessosNeg();
            ViewBag.Acessos = ObjAcessosNeg.findAll();

            if (CompanyUser.CompanyType == "Cliente")
            {
                Session.Remove("userId");
                Session.Remove("changeCompanyId");
                ViewBag.Cliente = (LoggedUser != null ? LoggedUser.Name : string.Empty);
                return RedirectToAction("Index", "Dashboard");
            }

            var aprendiz = objMaconNeg.findAll().Where(n => n.Id_Graduacao == 1).Count();
            var companheiro = objMaconNeg.findAll().Where(n => n.Id_Graduacao == 2).Count();
            var mestre = objMaconNeg.findAll().Where(n => n.Id_Graduacao == 3).Count();

            ViewBag.Aprendiz = aprendiz;
            ViewBag.Companheiro = companheiro;
            ViewBag.Mestre = mestre;
            ViewBag.Total = aprendiz + companheiro + mestre;

            return View();
        }

    }

}