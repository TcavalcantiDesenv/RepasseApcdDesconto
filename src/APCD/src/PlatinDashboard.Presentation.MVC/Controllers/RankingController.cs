using PlatinDashboard.Application.Farmacia.Interfaces;
using PlatinDashboard.Application.Farmacia.ViewModels.Balconista;
using PlatinDashboard.Application.Farmacia.ViewModels.Loja;
using PlatinDashboard.Application.Interfaces;
using PlatinDashboard.Domain.Farmacia.Entities;
using PlatinDashboard.Domain.Farmacia.Interfaces.Repositories;
using PlatinDashboard.Presentation.MVC.Helpers;
using PlatinDashboard.Presentation.MVC.MvcFilters;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PlatinDashboard.Presentation.MVC.Controllers
{
    public class RankingController : BaseController
    {
        private readonly IUadCabAppService _uadCabAppService;
        private readonly IFunCabAppService _funCabAppService;
        private readonly IVendaBalconistaPorClassificacaoRepository _vendaBalconistaPorClassificacaoRepository;
        private readonly IVenUadClsRepository _venUadClsRepository;

        public RankingController(IUserAppService userAppService,
                                     ICompanyAppService companyAppService,
                                     IUadCabAppService uadCabAppService,
                                     IFunCabAppService funCabAppService,
                                     IVendaBalconistaPorClassificacaoRepository vendaBalconistaPorClassificacaoRepository,
                                     IVenUadClsRepository venUadClsRepository)
                                     : base(userAppService, companyAppService)
        {
            _funCabAppService = funCabAppService;
            _uadCabAppService = uadCabAppService;
            _vendaBalconistaPorClassificacaoRepository = vendaBalconistaPorClassificacaoRepository;
            _venUadClsRepository = venUadClsRepository;
        }
        [HttpGet]
        [ClaimsAuthorize("ChartRankingLojas", "Allowed")]
        public ActionResult Lojas()
        {
            ViewBag.Years = ListGeneratorHelper.GenerateYears();
            ViewBag.Months = ListGeneratorHelper.GenerateMonths();
            ViewBag.Cliente = (LoggedUser != null ? LoggedUser.Name : string.Empty);
            return View();
        }

        [HttpPost]
        [ClaimsAuthorize("ChartRankingLojas", "Allowed")]
        public JsonResult Lojas(string mes, string ano)
        {
            var mesAno = ano + mes;
            var vendas = new List<VenUadCls>();
            if (LoggedUser.UserStoresIds.Any())
            {
                vendas = _venUadClsRepository.GetByFilter(c => c.My == mesAno &&
                                                               LoggedUser.UserStoresIds.Contains(c.Uad), CompanyUser.GetDbConnection()).ToList();
            }
            else
            {
                vendas = _venUadClsRepository.GetByFilter(c => c.My == mesAno, CompanyUser.GetDbConnection()).ToList();
            }            
            var rankingList = new List<LojaRankingViewModel>();
            foreach (var venda in vendas.GroupBy(c => c.Uad).Select(group => group.First()))
            {
                var valorBruto = vendas.Where(c => c.Uad == venda.Uad).Sum(c => c.Vlb);
                var valorDesconto = vendas.Where(c => c.Uad == venda.Uad).Sum(c => c.Vld);
                var rankingViewModel = new LojaRankingViewModel
                {
                    Uad = venda.Uad,
                    ValorLiquido = valorBruto - valorDesconto,
                    Nome = _uadCabAppService.GetById(venda.Uad, CompanyUser.GetDbConnection()).Des
                };
                rankingList.Add(rankingViewModel);
            }
            rankingList = rankingList.OrderByDescending(r => r.ValorLiquido).Take(15).ToList();
            return Json(rankingList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [ClaimsAuthorize("ChartRankingBalconistas", "Allowed")]
        public ActionResult Balconistas()
        {
            ViewBag.Years = ListGeneratorHelper.GenerateYears();
            ViewBag.Months = ListGeneratorHelper.GenerateMonths();
            ViewBag.Cliente = (LoggedUser != null ? LoggedUser.Name : string.Empty);
            ViewBag.Stores = new SelectList(_uadCabAppService.GetStores(CompanyUser.GetDbConnection()), "Uad", "Des");
            return View();
        }

        [HttpPost]
        [ClaimsAuthorize("ChartRankingBalconistas", "Allowed")]
        public JsonResult Balconistas(string mes, string ano, int loja)
        {
            var mesAno = ano + mes;
            var vendas = new List<VendaBalconistaPorClassificacao>();
            vendas = _vendaBalconistaPorClassificacaoRepository.GetByFilter(c => c.My == mesAno && c.Uad == loja, CompanyUser.GetDbConnection()).ToList();
            var rankingList = new List<BalconistaRankingViewModel>();
            foreach (var venda in vendas.GroupBy(c => c.Bal).Select(group => group.First()))
            {
                var valorBruto = vendas.Where(c => c.Bal == venda.Bal).Sum(c => c.Vlb);
                var valorDesconto = vendas.Where(c => c.Bal == venda.Bal).Sum(c => c.Vld);
                var balconista = _funCabAppService.GetByFilter(f => f.Cod == venda.Bal.ToString(), CompanyUser.GetDbConnection()).FirstOrDefault();
                var rankingViewModel = new BalconistaRankingViewModel
                {
                    BalconistaId = venda.Bal,
                    ValorLiquido = valorBruto - valorDesconto,
                    Nome = balconista != null ? balconista.Nom : "Sem Identificação"
                };
                rankingList.Add(rankingViewModel);
            }
            rankingList = rankingList.OrderByDescending(r => r.ValorLiquido).Take(10).ToList();
            return Json(rankingList, JsonRequestBehavior.AllowGet);
        }
    }
}