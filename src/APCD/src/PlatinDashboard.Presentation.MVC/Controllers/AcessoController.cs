using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Model.Entity;
using Model.Neg;
using PlatinDashboard.Application.Administrativo.Interfaces;
using PlatinDashboard.Application.Interfaces;
using PlatinDashboard.Application.ViewModels.CompanyViewModels;
using PlatinDashboard.Application.ViewModels.UserViewModels;
using PlatinDashboard.Infra.CrossCutting.Identity.Configuration;
using System;
using System.Web;
using System.Web.Mvc;

namespace PlatinDashboard.Presentation.MVC.Controllers
{

    [AllowAnonymous]
    public class AcessoController : BaseController
    {
        private ApplicationSignInManager _signInManager;
        private readonly ApplicationUserManager _userManager;
        private readonly IClienteAppService _clienteAppService;

        public AcessoController(IUserAppService userAppService,
                                ICompanyAppService companyAppService,
                                IClienteAppService clienteAppService,
                                ApplicationSignInManager signInManager,
                                ApplicationUserManager userManager)
                                : base(userAppService, companyAppService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _clienteAppService = clienteAppService;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                if (_signInManager != null && HttpContext.GetOwinContext().Get<ApplicationSignInManager>() == null)
                {
                    HttpContext.GetOwinContext().Set<ApplicationSignInManager>(_signInManager);
                }

                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        [HttpGet]
        public ActionResult Index(string returnUrl)
        {


            if (!string.IsNullOrEmpty(User.Identity.GetUserId()))
            {
                return RedirectToLocal(returnUrl);
           //     return RedirectToAction("Index", "Remessa");
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginViewModel loginViewModel, string returnUrl)
        {
            Session["IDAcesso"] = "";
            Session["Senha"] = "";
            var ID = 0;
            var ObjAcessosNeg = new AcessosNeg();
            var acessos = new Acessos();


            // Conexão utilizando proxy 
            System.Net.ServicePointManager.Expect100Continue = false;
                string ipUser = string.Empty;
                try
                {
                    if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] == null)
                    {
                        ipUser = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                    }
                    else
                    {
                        ipUser = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    }
                }
                catch
                {
                    ipUser = string.Empty;
                }

            if (ModelState.IsValid)
            {
                var applicationUser = _userManager.FindByName(loginViewModel.UserName);
                Session["Senha"] = loginViewModel.Password;
                //Caso o usuário não exista ele deve verificar se o usuario existe na base administrativa
                if (applicationUser == null)
                {
                    ModelState.AddModelError("", "Usuário ou senha não conferem !");
                }
                //Verificando se conexão da empresa está configurada
                else if (!_companyAppService.GetById(applicationUser.CompanyId).CheckConnectionConfiguration())
                {
                    ModelState.AddModelError("", "O seu acesso ao Portal ainda não está habilitado, por favor entre em contato com o HelpDesk.");
                }
                //Verificando se a conta esta bloqueada
                else if (!applicationUser.Active)
                {
                    ModelState.AddModelError("", "Conta bloqueada ou inexistente.");
                }
                else
                {
                    bool ativo = applicationUser.PhoneNumberConfirmed; 
                    acessos.Nome = applicationUser.Name;
                    acessos.DataEntrada = DateTime.Now;
                    acessos.IP = ipUser;
                    acessos.Usuario = applicationUser.UserName;
                    acessos.Empresa = applicationUser.Name + " " + applicationUser.LastName;
                    ObjAcessosNeg.create(acessos);
                    ID = ObjAcessosNeg.buscarUltimoID();

                    Session["IP"] = ipUser;
                    if(ativo)
                    {
                        Session["DataInicial"] = applicationUser.DataInicial;
                        Session["DataFinal"] = applicationUser.DataFinal;
                        Session["NovoValor"] = applicationUser.NovoValor;
                        Session["Descricao"] = applicationUser.Descricao;
                        Session["Ativo"] = applicationUser.PhoneNumberConfirmed;

                    }
                    else
                    {
                        Session["DataInicial"] = "01/01/200";
                        Session["DataFinal"] = "01/01/2000";
                        Session["NovoValor"] = "0,00";
                        Session["Descricao"] = "Nenhum valor definido para esta regional";
                        Session["Ativo"] = false;

                    }


                    Session["Username"] = applicationUser.UserName;
                    Session["IDAcesso"] = ID.ToString();
                    Session["GUIDE"] = applicationUser.Id;
                    Session["Nome"] = applicationUser.Name;
                    Session["Sobrenome"] = applicationUser.LastName;
                    Session["CODREG"] = applicationUser.LastName;
                    Session["NOMREG"] = applicationUser.Name;
                    Session["CompanyId"] = applicationUser.CompanyId;
                    Session["usuarioId"] = applicationUser.Id;
                    var company = _companyAppService.GetById(applicationUser.CompanyId);
                    Session["TipoUser"] = applicationUser.UserType;
                    Session["Loja"] = company.Name;

                    var result = SignInManager.PasswordSignIn(loginViewModel.UserName, loginViewModel.Password, true, true);
                    string senha = Session["Senha"].ToString();
                    switch (result)
                    {

                        case SignInStatus.Success:
                           if(senha == "123mudar") { RedirectToAction("Index", "Macon"); }
                            return RedirectToLocal(returnUrl);
                        case SignInStatus.LockedOut:
                            ModelState.AddModelError("", "Conta bloqueada por várias tentativas de acesso.");
                            break;
                        case SignInStatus.Failure:
                            ModelState.AddModelError("", "Login ou senha Inválido.");
                            break;
                        default:
                            ModelState.AddModelError("", "Tentativa inválida de login.");
                            break;
                    }
                }             
            }
            ViewBag.ReturnUrl = returnUrl;
            return View(loginViewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Sair()
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    var saiID = Session["IDAcesso"].ToString();

                }
                catch(Exception ex)
                {

                }
                _signInManager.AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            }
            return RedirectToAction("Index", "Acesso");
        }

    }
}