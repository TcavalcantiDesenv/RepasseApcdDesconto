using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PlatinDashboard.Application.Administrativo.Interfaces;
using PlatinDashboard.Application.Interfaces;
using PlatinDashboard.Application.ViewModels.CompanyViewModels;
using PlatinDashboard.Application.ViewModels.UserViewModels;
using PlatinDashboard.Infra.CrossCutting.Identity.Configuration;
using System.Web;
using System.Web.Mvc;

namespace PlatinDashboard.Presentation.MVC.Controllers
{
    [AllowAnonymous]
    public class EntrarController : BaseController
    {
        private ApplicationSignInManager _signInManager;
        private readonly ApplicationUserManager _userManager;
        private readonly IClienteAppService _clienteAppService;

        public EntrarController(IUserAppService userAppService,
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
        // GET: Entrar
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
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginViewModel loginViewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var applicationUser = _userManager.FindByName(loginViewModel.UserName);
                //Caso o usuário não exista ele deve verificar se o usuario existe na base administrativa
                if (applicationUser == null)
                {
                    var usuario = _clienteAppService.BuscarUsuarioPorCredencial(loginViewModel.UserName, loginViewModel.Password);
                    if (usuario != null)
                    {
                        //Verificando se o usuário é da matriz
                        if (usuario.Tipo == "matriz")
                        {
                            var rede = _clienteAppService.BuscarRedePorCodigoCliente(usuario.CodigoCliente);
                            var company = _companyAppService.GetByCustomerCode(usuario.CodigoCliente);
                            //Caso o usuário exista e a empresa não, deve sincronizar os dados da empresa
                            if (usuario != null && company == null)
                            {
                                var newCompany = new CreateCompanyViewModel();
                                newCompany.GenerateDefaultConnetionValues();
                                newCompany.GetRedeInformation(rede);
                                newCompany.GetUsuarioInformation(usuario);
                                newCompany.CompanyType = "Cliente";
                                _companyAppService.Add(newCompany);
                                var passwordHash = _userManager.PasswordHasher.HashPassword(newCompany.Password);
                                _userAppService.Add(newCompany, passwordHash);
                            }
                            ModelState.AddModelError("", "O seu acesso ao Portal ainda não está habilitado, por favor entre em contato com o HelpDesk.");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Seu usuário administrativo não pertence a Matriz.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Conta bloqueada ou inexistente.");
                    }
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
                    var result = SignInManager.PasswordSignIn(loginViewModel.UserName, loginViewModel.Password, true, true);
                    switch (result)
                    {
                        case SignInStatus.Success:
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
                _signInManager.AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            }
            return RedirectToAction("Index", "Acesso");
        }

        public ActionResult Voltar()
        {
            return View();
        }

        public ActionResult Baixa()
        {
            return View();
        }
    }
}