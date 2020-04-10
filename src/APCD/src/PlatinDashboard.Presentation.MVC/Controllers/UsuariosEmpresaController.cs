using Model.Entity;
using Model.Neg;
using PlatinDashboard.Application.Administrativo.Interfaces;
using PlatinDashboard.Application.Farmacia.Interfaces;
using PlatinDashboard.Application.Interfaces;
using PlatinDashboard.Application.ViewModels.StoreViewModels;
using PlatinDashboard.Application.ViewModels.UserViewModels;
using PlatinDashboard.Infra.CrossCutting.Identity.Configuration;
using PlatinDashboard.Presentation.MVC.MvcFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PlatinDashboard.Presentation.MVC.Controllers
{
    [Authorize]
    public class UsuariosEmpresaController : BaseController
    {
        private readonly ApplicationUserManager _userManager;
        private readonly IClienteAppService _clienteAppService;
        private readonly IUadCabAppService _uadCabAppService;
        private readonly IStoreAppService _storeAppService;
        LojasNeg objLojasNeg;
        MaconNeg objMaconNeg;
        GraduacaoNeg objGraduacaoNeg;

        RemessaNeg objRemessaNeg;
        RegionaisNeg objRegionaisNeg;
        EnderecosNeg objEnderecosNeg;
        ValorRegionaisNeg objValorRegionaisNeg;
        Enderecos enderecos = new Enderecos();
        //  RemessaModel remessa = new RemessaModel();
        RegionaisModel regionais = new RegionaisModel();

        public UsuariosEmpresaController(IUserAppService userAppService,
                                         ICompanyAppService companyAppService,
                                         IClienteAppService clienteAppService,
                                         IUadCabAppService uadCabAppService,
                                         IStoreAppService storeAppService,
                                         ApplicationUserManager userManager)
                                         : base(userAppService, companyAppService)
        {
            objMaconNeg = new MaconNeg();
            objLojasNeg = new LojasNeg();
            objGraduacaoNeg = new GraduacaoNeg();
            objRemessaNeg = new RemessaNeg();
            objRegionaisNeg = new RegionaisNeg();
            objValorRegionaisNeg = new ValorRegionaisNeg();
            objEnderecosNeg = new EnderecosNeg();

            _userManager = userManager;
            _clienteAppService = clienteAppService;
            _uadCabAppService = uadCabAppService;
            _storeAppService = storeAppService;
        }

        [HttpGet]
        [ClaimsAuthorize("CompanyType", "Master")]
        public ActionResult Novo(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var companyViewModel = _companyAppService.GetById(id.Value);
            ViewBag.CompanyViewModel = companyViewModel;
            if (ViewBag.CompanyViewModel == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //Buscando as lojas cadastradas na base da empresa
            //var lojas = _uadCabAppService.GetStores(companyViewModel.GetDbConnection());
            //ViewBag.Lojas = new SelectList(lojas, "Uad", "Des");
            return View(new CreateUserViewModel { CompanyId = id.Value });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ClaimsAuthorize("CompanyType", "Master")]
        public ActionResult Novo(CreateUserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                var passwordHash = _userManager.PasswordHasher.HashPassword(userViewModel.Password);
                var companyViewModel = _companyAppService.GetById(userViewModel.CompanyId);
                var userTuple = _userAppService.Add(userViewModel, passwordHash, companyViewModel.CompanyType);

                // Pesquisar Guide Cadastrado
                string guide = userViewModel.UserId;
                int loja = userViewModel.CompanyId;
                var CadMacon = new Macon();
                CadMacon.Guide = guide;
                CadMacon.Nome = userViewModel.Name;
                CadMacon.Email = userViewModel.Email;
                CadMacon.Nome_Tratamento = userViewModel.LastName;
                CadMacon.Situacao = "Em Loja";
              //  CadMacon.Cim = 0;
                CadMacon.Id_Graduacao = 1;
                CadMacon.Id_Condecoracao = 1;
                CadMacon.Id_Cargo = 1;
                CadMacon.Id_Loja = loja;

                objMaconNeg.create(CadMacon);

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
                    return RedirectToAction("Detalhes", "Empresas", new { id = userViewModel.CompanyId });
                }
                else
                {
                    //Adicionando mensagem de erro
                    ModelState.AddModelError(userTuple.Item3, userTuple.Item4);
                }
            }
            ViewBag.CompanyViewModel = _companyAppService.GetById(userViewModel.CompanyId);
            //Buscando as lojas cadastradas na base da empresa
            //var lojas = _uadCabAppService.GetStores(ViewBag.CompanyViewModel.GetDbConnection());
            //ViewBag.Lojas = new SelectList(lojas, "Uad", "Des");        
            return View(userViewModel);
        }



        ///** /////////////////////////////////////////////////////////////////////////////////
        [HttpGet]
        public ActionResult NovoValor(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var companyViewModel = _companyAppService.GetById(id.Value);
            ViewBag.CompanyViewModel = companyViewModel;
            if (ViewBag.CompanyViewModel == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //Buscando as lojas cadastradas na base da empresa
            //var lojas = _uadCabAppService.GetStores(companyViewModel.GetDbConnection());
            //ViewBag.Lojas = new SelectList(lojas, "Uad", "Des");
            return View(new CreateUserViewModel { CompanyId = id.Value });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NovoValor(CreateUserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                var passwordHash = _userManager.PasswordHasher.HashPassword(userViewModel.Password);
                var companyViewModel = _companyAppService.GetById(userViewModel.CompanyId);
                var userTuple = _userAppService.Add(userViewModel, passwordHash, companyViewModel.CompanyType);

                // Pesquisar Guide Cadastrado
                string guide = userViewModel.UserId;
                int loja = userViewModel.CompanyId;
                var CadMacon = new Macon();
                CadMacon.Guide = guide;
                CadMacon.Nome = userViewModel.Name;
                CadMacon.Email = userViewModel.Email;
                CadMacon.Nome_Tratamento = userViewModel.LastName;
                CadMacon.Situacao = "Em Loja";
                //  CadMacon.Cim = 0;
                CadMacon.Id_Graduacao = 1;
                CadMacon.Id_Condecoracao = 1;
                CadMacon.Id_Cargo = 1;
                CadMacon.Id_Loja = loja;

                objMaconNeg.create(CadMacon);

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
                    return RedirectToAction("Detalhes", "Empresas", new { id = userViewModel.CompanyId });
                }
                else
                {
                    //Adicionando mensagem de erro
                    ModelState.AddModelError(userTuple.Item3, userTuple.Item4);
                }
            }
            ViewBag.CompanyViewModel = _companyAppService.GetById(userViewModel.CompanyId);
            //Buscando as lojas cadastradas na base da empresa
            //var lojas = _uadCabAppService.GetStores(ViewBag.CompanyViewModel.GetDbConnection());
            //ViewBag.Lojas = new SelectList(lojas, "Uad", "Des");        
            return View(userViewModel);
        }

        /// *****************************************************************************





        [HttpGet]
        [ClaimsAuthorize("CompanyType", "Master")]
        public ActionResult Detalhes(string id)
        {
            ViewBag.UserID = id;
            if (string.IsNullOrEmpty(id)) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var userViewModel = _userAppService.GetById(id);
            if (userViewModel == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ViewBag.CompanyViewModel = _companyAppService.GetById(userViewModel.CompanyId);
            ViewBag.CompanyCharts = _companyAppService.GetCompanyCharts(userViewModel.CompanyId);
            ViewBag.UserCharts = _userAppService.GetUserCharts(id);
            ViewBag.UserCharts.FillOutChartsInformation(ViewBag.CompanyCharts);
            //Buscando as lojas cadastradas na base da empresa
            //var lojas = _uadCabAppService.GetStores(ViewBag.CompanyViewModel.GetDbConnection());
            //ViewBag.Lojas = new SelectList(lojas, "Uad", "Des");
            return View(userViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ClaimsAuthorize("CompanyType", "Master")]
        public ActionResult Editar(EditUserViewModel userViewModel)
        {
            //Buscando as lojas cadastradas na base da empresa
            var user = _userAppService.GetById(userViewModel.UserId);
            var companyViewModel = _companyAppService.GetById(user.CompanyId);
            //var lojas = _uadCabAppService.GetStores(companyViewModel.GetDbConnection());
            //ViewBag.Lojas = new SelectList(lojas, "Uad", "Des");
            if (ModelState.IsValid)
            {                                
                //Caso o usuario não for Manager e estiver com acesso limitado de lojas
                if (userViewModel.UserType != "Manager")
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
                    _storeAppService.SetUserStores(user.UserId, userViewModel.UserStoresIds);
                }
                else
                {
                    //Caso for alterado para Manager deve remover todas as limitações de acesso
                    _storeAppService.RemoveAllUserStores(user.UserId);
                }
                ValorRegionais valorRegional = new ValorRegionais();
                valorRegional.CodRegional = userViewModel.LastName;
                valorRegional.Regional = userViewModel.Name;
                valorRegional.Valor = userViewModel.NovoValor;
                valorRegional.DataInicial = userViewModel.DataInicial;
                valorRegional.DataFinal = userViewModel.DataFinal;
                valorRegional.Descricao = userViewModel.Descricao;
                objValorRegionaisNeg.create(valorRegional);

                userViewModel = _userAppService.Update(userViewModel);


                return Json(new { updated = true, view = RenderPartialView("_Editar", userViewModel) });
            }
            return Json(new { updated = false, view = RenderPartialView("_Editar", userViewModel) });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ClaimsAuthorize("CompanyType", "Master")]
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
        [ClaimsAuthorize("CompanyType", "Master")]
        public ActionResult ModalRemover(string id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var company = _userAppService.GetById(id);
            if (company == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            return PartialView("_ModalRemoverUsuario", company);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ClaimsAuthorize("CompanyType", "Master")]
        public ActionResult Remover(string userId)
        {
            if (userId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var user = _userAppService.GetById(userId);
            if (user == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            _userAppService.Remove(user.UserId);
            return RedirectToAction("Index", "Empresas");
         //   return Json(new { deleted = true, userId = user.UserId });
        }

        [HttpPost]
        [ClaimsAuthorize("CompanyType", "Master")]
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