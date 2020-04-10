using Model.Neg;
using PlatinDashboard.Application.Farmacia.Interfaces;
using PlatinDashboard.Application.Farmacia.ViewModels.Classificacao;
using PlatinDashboard.Application.Farmacia.ViewModels.Loja;
using PlatinDashboard.Application.Interfaces;
using PlatinDashboard.Domain.Farmacia.Interfaces.Repositories;
using PlatinDashboard.Domain.Farmacia.Interfaces.StoredProcedures;
using PlatinDashboard.Presentation.MVC.Helpers;
using PlatinDashboard.Presentation.MVC.MvcFilters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace PlatinDashboard.Presentation.MVC.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly IClsCabAppService _clsCabAppService;
        private readonly IClsVenAllAppService _clsVenAllAppService;
        private readonly IGraficoWebAppService _graficoWebAppService;
        private readonly IVendaLojaPorMesAppService _vendaLojaPorMesAppService;
        private readonly IVenUadAppService _venUadAppService;
        private readonly IUadCabAppService _uadCabAppService;
        private readonly IVendaBalconistaPorClassificacaoRepository _vendaBalconistaPorClassificacaoRepository;
        private readonly IVenUadClsRepository _venUadClsRepository;
        private readonly IVenUadClsDashRepository _venUadClsDashRepository;
        private readonly IGraficoWebLojaStoredProcedure _graficoWebLojaStoredProcedure;

        public DashboardController(IUserAppService userAppService,
                                     ICompanyAppService companyAppService,
                                     IClsCabAppService clsCabAppService,
                                     IClsVenAllAppService clsVenAllAppService,
                                     IGraficoWebAppService graficoWebAppService,
                                     IVendaLojaPorMesAppService vendaLojaPorMesAppService,
                                     IVenUadAppService venUadAppService,
                                     IUadCabAppService uadCabAppService,
                                     IVendaBalconistaPorClassificacaoRepository vendaBalconistaPorClassificacaoRepository,
                                     IVenUadClsRepository venUadClsRepository,
                                     IVenUadClsDashRepository venUadClsDashRepository,
                                     IGraficoWebLojaStoredProcedure graficoWebLojaStoredProcedure)
                                     : base(userAppService, companyAppService)
        {
            _clsCabAppService = clsCabAppService;
            _clsVenAllAppService = clsVenAllAppService;
            _graficoWebAppService = graficoWebAppService;
            _vendaLojaPorMesAppService = vendaLojaPorMesAppService;
            _venUadAppService = venUadAppService;
            _uadCabAppService = uadCabAppService;
            _vendaBalconistaPorClassificacaoRepository = vendaBalconistaPorClassificacaoRepository;
            _venUadClsRepository = venUadClsRepository;
            _venUadClsDashRepository = venUadClsDashRepository;
            _graficoWebLojaStoredProcedure = graficoWebLojaStoredProcedure;
        }

        [HttpGet]
        [ClaimsAuthorize("ChartDashboard", "Allowed")]
        public ActionResult Index()
        {


            ViewBag.Months = ListGeneratorHelper.GenerateMonths();
            ViewBag.Years = ListGeneratorHelper.GenerateYears();
            var store = "";// _uadCabAppService.GetStores(CompanyUser.GetDbConnection());
            if (LoggedUser.UserStoresIds.Any())
            {
                //store = store.Where(s => LoggedUser.UserStoresIds.Any(u => u.Equals(s.Uad)));
            }
            ViewBag.Stores = "";// new SelectList(store, "Uad", "Des");
            ViewBag.Cliente = "Hilton";// (LoggedUser != null ? LoggedUser.Name : string.Empty);
            ViewBag.LastName = "Lima";// (LoggedUser != null ? LoggedUser.LastName : string.Empty);
            Session["GUIDE"] = LoggedUser.UserId;
            Session["NIVEL"] = LoggedUser.UserType;
            Session["CODREG"] = LoggedUser.LastName;
            Session["NOMREG"] = LoggedUser.Name;
            //  return RedirectToAction("Index", "Remessa");
            return RedirectToAction("Index", "Regional");
            //        return View();
        }

        [HttpPost]
        [ClaimsAuthorize("ChartDashboard", "Allowed")]
        public ActionResult GraficoFaturamentoClassificacao(string mes, string ano, int? loja)
        {
            var vendas =  new List<ClassificacaoVendaViewModel>();
            var vendasMesAnterior =  new List<ClassificacaoVendaViewModel>();
            var periodo = $"{ano}{mes}";
            var anoMesAnterior = Convert.ToInt32(mes) == 1 ? (Convert.ToInt32(ano) - 1).ToString() : ano;
            var mesAnterior = Convert.ToInt32(mes) == 1 ? 12.ToString() : (Convert.ToInt32(mes) - 1).ToString();
            var segundoPeriodo = $"{anoMesAnterior}{(mesAnterior.Length < 2 ? "0" + mesAnterior : mesAnterior)}";
            //for (int i = 1; i < 9; i++)
            //{
            //    //Buscando as vendas por classificação e período informado
            //    var venda = new ClassificacaoVendaViewModel
            //    {
            //        ClassificacaoId = i,
            //        Nome = _clsCabAppService.GetByFilter(c => c.Cod == i.ToString(), CompanyUser.GetDbConnection()).FirstOrDefault()?.Des ?? "Sem Identificação"
            //    };
            //    var vendaMesAnterior = new ClassificacaoVendaViewModel
            //    {
            //        ClassificacaoId = i,
            //        Nome = _clsCabAppService.GetByFilter(c => c.Cod == i.ToString(), CompanyUser.GetDbConnection()).FirstOrDefault()?.Des ?? "Sem Identificação"
            //    };
            //    if (LoggedUser.UserStoresIds.Any())
            //    {
            //        venda.Valor = loja == null ?
            //        _venUadClsDashRepository.GetByFilter(c => c.Cls == i && c.My == periodo && LoggedUser.UserStoresIds.Contains(c.Uad), CompanyUser.GetDbConnection()).ToList().Sum(c => c.ValorBruto) :
            //        _venUadClsDashRepository.GetByFilter(c => c.Cls == i && c.My == periodo && c.Uad == loja && LoggedUser.UserStoresIds.Contains(c.Uad), CompanyUser.GetDbConnection()).ToList().Sum(c => c.ValorBruto);
            //        vendaMesAnterior.Valor = loja == null ?
            //        _venUadClsDashRepository.GetByFilter(c => c.Cls == i && c.My == segundoPeriodo && LoggedUser.UserStoresIds.Contains(c.Uad), CompanyUser.GetDbConnection()).Sum(c => c.ValorBruto) :
            //        _venUadClsDashRepository.GetByFilter(c => c.Cls == i && c.My == segundoPeriodo && c.Uad == loja && LoggedUser.UserStoresIds.Contains(c.Uad), CompanyUser.GetDbConnection()).Sum(c => c.ValorBruto);
            //    }
            //    else
            //    {
            //        venda.Valor = loja == null ?
            //        _venUadClsDashRepository.GetByFilter(c => c.Cls == i && c.My == periodo, CompanyUser.GetDbConnection()).ToList().Sum(c => c.ValorBruto) :
            //        _venUadClsDashRepository.GetByFilter(c => c.Cls == i && c.My == periodo && c.Uad == loja, CompanyUser.GetDbConnection()).ToList().Sum(c => c.ValorBruto);
            //        vendaMesAnterior.Valor = loja == null ?
            //        _venUadClsDashRepository.GetByFilter(c => c.Cls == i && c.My == segundoPeriodo, CompanyUser.GetDbConnection()).Sum(c => c.ValorBruto) :
            //        _venUadClsDashRepository.GetByFilter(c => c.Cls == i && c.My == segundoPeriodo && c.Uad == loja, CompanyUser.GetDbConnection()).Sum(c => c.ValorBruto);
            //    }
            //    vendas.Add(venda);   
            //    vendasMesAnterior.Add(vendaMesAnterior);
            //}
            var venda = new ClassificacaoVendaViewModel();
            var culture = new CultureInfo("pt-BR");
            var month = new DateTime(Convert.ToInt32("2000"), Convert.ToInt32("07"), 1).ToString("MMMM", culture);
            return Json(new { vendas, vendasMesAnterior, month, year = ano }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ClaimsAuthorize("ChartDashboard", "Allowed")]
        public JsonResult GridTotalDeVendas(string mes, string ano)
        {
            var periodo = ano + mes;            
            var vendasLoja = new List<VendaLojaViewModel>();
            //if (LoggedUser.UserStoresIds.Any())
            //{
            //    var graficosWeb = _graficoWebLojaStoredProcedure.Buscar(periodo, LoggedUser.UserStoresIds, CompanyUser.GetConnectionString(), CompanyUser.DatabaseProvider);
            //    foreach (var graficoWeb in graficosWeb)
            //    {
            //        vendasLoja.Add(new VendaLojaViewModel(graficoWeb));
            //    }
            //}
            //else
            //{
            //    var graficosWeb = _graficoWebAppService.GetByFilter(c => c.My == periodo, CompanyUser.GetDbConnection()).OrderBy(v => v.Dat);
            //    foreach (var graficoWeb in graficosWeb)
            //    {
            //        vendasLoja.Add(new VendaLojaViewModel(graficoWeb));
            //    }
            //}            
            return Json(vendasLoja, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ClaimsAuthorize("ChartDashboard", "Allowed")]
        public JsonResult GridVendaPorLojas(string mes, string ano)
        {
            var periodo = ano + mes;
            var vendasLoja = new List<VenUadViewModel>();
            //if (LoggedUser.UserStoresIds.Any())
            //{
            //    vendasLoja = _venUadAppService.GetByFilter(c => c.My == periodo && LoggedUser.UserStoresIds.Contains(c.Uad.Value), CompanyUser.GetDbConnection()).OrderBy(v => v.Dat).ToList();
            //}
            //else
            //{
            //    vendasLoja = _venUadAppService.GetByFilter(c => c.My == periodo, CompanyUser.GetDbConnection()).OrderBy(v => v.Dat).ToList();
            //}
            //var lojas = _uadCabAppService.GetAll(CompanyUser.GetDbConnection());
            var vendaLojaViewModels = new List<VendaLojaViewModel>();
            //foreach (var vendaLoja in vendasLoja)
            //{
            //    vendaLojaViewModels.Add(new VendaLojaViewModel(vendaLoja, lojas));
            //}
          //  vendaLojaViewModels.Add(new VendaLojaViewModel(vendaLoja, lojas));
            return Json(vendaLojaViewModels, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ClaimsAuthorize("ChartDashboard", "Allowed")]
        public JsonResult GridVendaMensalPorLojas(string mes, string ano)
        {
            //var periodo = ano + mes;
            //var vendasLoja = new List<VendaLojaPorMesViewModel>();
            //if (LoggedUser.UserStoresIds.Any())
            //{
            //    vendasLoja = _vendaLojaPorMesAppService.GetByFilter(c => c.AnoMes == periodo && LoggedUser.UserStoresIds.Contains(c.Uad.Value), CompanyUser.GetDbConnection()).OrderBy(v => v.Uad).ToList();
            //}
            //else
            //{
            //    vendasLoja = _vendaLojaPorMesAppService.GetByFilter(c => c.AnoMes == periodo, CompanyUser.GetDbConnection()).OrderBy(v => v.Uad).ToList();
            //}
            //var lojas = _uadCabAppService.GetAll(CompanyUser.GetDbConnection());
            var vendaLojaViewModels = new List<VendaLojaViewModel>();
            //foreach (var vendaLoja in vendasLoja)
            //{
            //    vendaLojaViewModels.Add(new VendaLojaViewModel(vendaLoja, lojas));
            //}
            return Json(vendaLojaViewModels, JsonRequestBehavior.AllowGet);
        }
    }
}