using Model.Entity;
using Model.Neg;
using PlatinDashboard.Application.Interfaces;
using PlatinDashboard.Application.ViewModels.UserViewModels;
using PlatinDashboard.Infra.CrossCutting.Identity.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PlatinDashboard.Presentation.MVC.Controllers
{
    [Authorize]
    public class PerfilController : BaseController
    {
        private readonly ApplicationUserManager _userManager;
        LojasNeg objLojasNeg;
        MaconNeg objMaconNeg;
        GraduacaoNeg objGraduacaoNeg;

        public PerfilController(IUserAppService userAppService,
                                  ICompanyAppService companyAppService,
                                  ApplicationUserManager userManager)
                                  : base(userAppService, companyAppService)
        {
            _userManager = userManager;
            objMaconNeg = new MaconNeg();
            objLojasNeg = new LojasNeg();
            objGraduacaoNeg = new GraduacaoNeg();
        }

        [HttpGet]
        public ActionResult Index()
        {
  //          ViewBag.Situacao = new SelectList
  //(
  //    new Situacao().ListaSituacao(),
  //    "Id",
  //    "Nome"
  //);

            List<Lojas> data = objLojasNeg.findAll();
            SelectList lista = new SelectList(data, "Id_Loja", "Nome");
            ViewBag.ListaLojas = lista;

            List<Graduacao> graduacao = objGraduacaoNeg.findAll();
            SelectList listagrad = new SelectList(graduacao, "Id_Graduacao", "Descricao");
            ViewBag.ListaGraduacao = listagrad;

            Macon objMacon = new Macon();
            objMacon.Guide = LoggedUser.UserId;
            List<Macon> lstMacon = objMaconNeg.FindPorGuide(objMacon);
            ViewBag.Macon = lstMacon;
            return View(LoggedUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(ProfileUserViewModel profileUserViewModel)
        {
            //ViewBag.Situacao = new SelectList
            //  (
            //      new Situacao().ListaSituacao(),
            //      "Id",
            //      "Nome"
            //  );

            List<Lojas> data = objLojasNeg.findAll();
            SelectList lista = new SelectList(data, "Id_Loja", "Nome");
            ViewBag.ListaLojas = lista;

            List<Graduacao> graduacao = objGraduacaoNeg.findAll();
            SelectList listagrad = new SelectList(graduacao, "Id_Graduacao", "Descricao");
            ViewBag.ListaGraduacao = listagrad;

            if (ModelState.IsValid)
            {
                profileUserViewModel = _userAppService.Update(profileUserViewModel);
                return Json(new { updated = true, view = RenderPartialView("_Editar", profileUserViewModel) });
            }
            return Json(new { updated = false, view = RenderPartialView("_Editar", profileUserViewModel) });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> TrocarSenha(ChangePasswordViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                string token = await _userManager.GeneratePasswordResetTokenAsync(userViewModel.UserId);
                var result = await _userManager.ResetPasswordAsync(userViewModel.UserId, token, userViewModel.Password);
                return Json(new { updated = result.Succeeded, view = RenderPartialView("_TrocarSenha", userViewModel) });
            }
            return Json(new { updated = false, view = RenderPartialView("_TrocarSenha", userViewModel) });
        }
    }
}