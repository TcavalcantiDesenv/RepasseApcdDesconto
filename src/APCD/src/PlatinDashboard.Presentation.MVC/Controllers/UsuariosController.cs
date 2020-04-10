using PlatinDashboard.Application.Farmacia.Interfaces;
using PlatinDashboard.Application.Interfaces;
using PlatinDashboard.Application.ViewModels.StoreViewModels;
using PlatinDashboard.Application.ViewModels.UserViewModels;
using PlatinDashboard.Infra.CrossCutting.Identity.Configuration;
using PlatinDashboard.Presentation.MVC.MvcFilters;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PlatinDashboard.Presentation.MVC.Controllers
{
    [Authorize]
    public class UsuariosController : BaseController
    {
        private readonly ApplicationUserManager _userManager;
        private readonly IStoreAppService _storeAppService;
        private readonly IUadCabAppService _uadCabAppService;

        public UsuariosController(IUserAppService userAppService,
                                  ICompanyAppService companyAppService,
                                  IStoreAppService storeAppService,
                                  IUadCabAppService uadCabAppService,
                                  ApplicationUserManager userManager)
                                  : base(userAppService, companyAppService)
        {
            _userManager = userManager;
            _storeAppService = storeAppService;
            _uadCabAppService = uadCabAppService;
        }

        [HttpGet]
        [ClaimsAuthorize("UserType", "Admin,Subadmin,Manager")]
        public ActionResult Index()
        {            
            var usuarioViewModels = _userAppService.GetByCompany(LoggedUser.CompanyId);
            if (LoggedUser.UserType == "Subadmin")
            {
                usuarioViewModels = usuarioViewModels.Where(u => u.UserType == "Comum");
            }
            return View(usuarioViewModels);
        }

        [HttpGet]
        [ClaimsAuthorize("UserType", "Admin,Subadmin,Manager")]
        public ActionResult Novo()
        {
            if (CompanyUser.CompanyType != "Master")
            {
                //Buscando as lojas cadastradas na base da empresa
                var lojas = _uadCabAppService.GetStores(CompanyUser.GetDbConnection());
                ViewBag.Lojas = new SelectList(lojas, "Uad", "Des");
            }            
            return View(new CreateUserViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ClaimsAuthorize("UserType", "Admin,Subadmin,Manager")]
        public ActionResult Novo(CreateUserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                var passwordHash = _userManager.PasswordHasher.HashPassword(userViewModel.Password);
                userViewModel.CompanyId = LoggedUser.CompanyId;
                //Se empresa for a Platin o tipo de usuário deve ser Admin
                if (CompanyUser.CompanyType == "Master") userViewModel.UserType = "Admin";
                var companyViewModel = _companyAppService.GetById(userViewModel.CompanyId);
                var userTuple = _userAppService.Add(userViewModel, passwordHash, CompanyUser.CompanyType);
                //Verificando se foi criado com sucesso
                if (userTuple.Item2)
                {
                    //Caso o usuario não for manager e estiver com acesso limitado de lojas
                    if (userViewModel.UserType != "Manager" && userViewModel.UserStoresIds.Any())
                    {
                        //Cadastrando as lojas da empresa na base do Portal
                        foreach (var storeId in userViewModel.UserStoresIds)
                        {
                            //Verificando se essa loja já está cadastrada na base do Portal
                            if (_storeAppService.GetByCompanyAndExternalId(userViewModel.CompanyId, storeId) == null)
                            {
                                var uadCabViewModel = _uadCabAppService.GetById(storeId, companyViewModel.GetDbConnection());
                                var storeViewModel = new StoreViewModel(userViewModel, uadCabViewModel);
                                _storeAppService.Add(storeViewModel);
                            }
                        }
                        //Relacionando as lojas com o novo usuário
                        _storeAppService.SetUserStores(userTuple.Item1.UserId, userViewModel.UserStoresIds);
                    }
                    return RedirectToAction("Index", "Usuarios");
                }
                else
                {
                    //Adicionando mensagem de erro
                    ModelState.AddModelError(userTuple.Item3, userTuple.Item4);
                }
            }
            if (CompanyUser.CompanyType != "Master")
            {
                //Buscando as lojas cadastradas na base da empresa
                var lojas = _uadCabAppService.GetStores(CompanyUser.GetDbConnection());
                ViewBag.Lojas = new SelectList(lojas, "Uad", "Des");
            }
            return View(userViewModel);
        }

        [HttpGet]
        [ClaimsAuthorize("UserType", "Admin,Subadmin,Manager")]
        public ActionResult Detalhes(string id)
        {             
            if (string.IsNullOrEmpty(id)) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (!VerifyUserCompanyAccess(id)) return RedirectToAction("Forbidden", "Errors");
            var userViewModel = _userAppService.GetById(id);
            if (userViewModel == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var companyViewModel = _companyAppService.GetById(userViewModel.CompanyId);
            if (companyViewModel.CompanyType == "Cliente")
            {
                ViewBag.CompanyCharts = _companyAppService.GetCompanyCharts(userViewModel.CompanyId);
                ViewBag.UserCharts = _userAppService.GetUserCharts(id);
                ViewBag.UserCharts.FillOutChartsInformation(ViewBag.CompanyCharts);
                //Buscando as lojas cadastradas na base da empresa
                var lojas = _uadCabAppService.GetStores(companyViewModel.GetDbConnection());
                ViewBag.Lojas = new SelectList(lojas, "Uad", "Des");
            }
            ViewBag.LoggedUser = LoggedUser;
            return View(userViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ClaimsAuthorize("UserType", "Admin,Subadmin,Manager")]
        public ActionResult Editar(EditUserViewModel userViewModel)
        {
            if (!VerifyUserCompanyAccess(userViewModel.UserId)) return RedirectToAction("Forbidden", "Errors");
            //Buscando as lojas cadastradas na base da empresa
            var user = _userAppService.GetById(userViewModel.UserId);
            var companyViewModel = _companyAppService.GetById(user.CompanyId);
            if (CompanyUser.CompanyType != "Master")
            {
                var lojas = _uadCabAppService.GetStores(companyViewModel.GetDbConnection());
                ViewBag.Lojas = new SelectList(lojas, "Uad", "Des");
            }            
            if (ModelState.IsValid)
            {
                if (CompanyUser.CompanyType == "Master") userViewModel.UserType = "Admin";
                //Caso o usuario não for Manager e estiver com acesso limitado de lojas
                if (userViewModel.UserType != "Manager" && userViewModel.UserType != "Admin")
                {
                    //Cadastrando as lojas da empresa na base do Portal
                    foreach (var storeId in userViewModel.UserStoresIds)
                    {
                        //Verificando se essa loja já está cadastrada na base do Portal
                        if (_storeAppService.GetByCompanyAndExternalId(user.CompanyId, storeId) == null)
                        {
                            var uadCabViewModel = _uadCabAppService.GetById(storeId, companyViewModel.GetDbConnection());
                            var storeViewModel = new StoreViewModel(userViewModel, companyViewModel, uadCabViewModel);
                            _storeAppService.Add(storeViewModel);
                        }
                    }
                    //Relacionando as lojas com o novo usuário
                    _storeAppService.SetUserStores(user.UserId, userViewModel.UserStoresIds, LoggedUser.UserId);
                }
                else
                {
                    //Caso for alterado para Manager deve remover todas as limitações de acesso
                    _storeAppService.RemoveAllUserStores(user.UserId);
                }
                userViewModel = _userAppService.Update(userViewModel);
                return Json(new { updated = true, view = RenderPartialView("_Editar", userViewModel) });
            }
            return Json(new { updated = false, view = RenderPartialView("_Editar", userViewModel) });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ClaimsAuthorize("UserType", "Admin,Subadmin,Manager")]
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

        [HttpGet]
        [ClaimsAuthorize("UserType", "Admin,Subadmin,Manager")]
        public ActionResult ModalRemover(string id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var company = _userAppService.GetById(id);
            if (company == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            return PartialView("_ModalRemoverUsuario", company);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ClaimsAuthorize("UserType", "Admin,Subadmin,Manager")]
        public ActionResult Remover(string userId)
        {
            if (userId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var user = _userAppService.GetById(userId);
            if (user == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            _userAppService.Remove(user.UserId);
            return Json(new { deleted = true, userId = user.UserId });
        }

        [HttpPost]
        [ClaimsAuthorize("CompanyType", "Cliente")]
        [ValidateAntiForgeryToken]
        public ActionResult EditarAcessoGraficos(UserChartViewModel userChartViewModel)
        {
            if (ModelState.IsValid)
            {
                //Buscando Usuário
                var userViewModel = _userAppService.GetById(userChartViewModel.UserId);
                //Verificando existencia do usuário
                if (userViewModel == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                //Atualizando acessos do usuário
                _userAppService.UpdateUserCharts(userChartViewModel);
                ViewBag.CompanyCharts = _companyAppService.GetCompanyCharts(userViewModel.CompanyId);
                var userCharts = _userAppService.GetUserCharts(userChartViewModel.UserId);
                userCharts.FillOutChartsInformation(ViewBag.CompanyCharts);
                return Json(new { updated = true, view = RenderPartialView("_EditarAcessoGraficos", userCharts) });
            }
            return Json(new { updated = false, view = RenderPartialView("_EditarAcessoGraficos", userChartViewModel) });
        }        
    }
}