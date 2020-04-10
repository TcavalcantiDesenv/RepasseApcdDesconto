using PlatinDashboard.Application.Farmacia.Interfaces;
using PlatinDashboard.Application.Farmacia.ViewModels.Legado;
using PlatinDashboard.Application.Farmacia.ViewModels.Loja;
using PlatinDashboard.Application.Interfaces;
using PlatinDashboard.Domain.Farmacia.Entities;
using PlatinDashboard.Domain.Farmacia.Interfaces.Repositories;
using PlatinDashboard.Domain.Farmacia.Interfaces.StoredProcedures;
using PlatinDashboard.Presentation.MVC.Helpers;
using PlatinDashboard.Presentation.MVC.MvcFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PlatinDashboard.Presentation.MVC.Controllers
{
    public class LojasController : BaseController
    {
        private readonly IClsCabRepository _clsCabRepository;
        private readonly IVenUadRepository _venUadRepository;
        private readonly IVenUadClsDashRepository _venUadClsDashRepository;
        private readonly IVenUadMensalRepository _venUadMensalRepository;
        private readonly IUadCabAppService _uadCabAppService;
        private readonly IClsVenAllGraficoTotalRepository _clsVenAllGraficoTotalRepository;
        private readonly IVendaLojaPorHoraAppService _vendaLojaPorHoraAppService;
        private readonly IVendasLojaPorDiaHoraAppService _vendasLojaPorDiaHoraAppService;
        private readonly IVendasClassificacaoLojaStoredProcedure _vendasClassificacaoLojaStoredProcedure;
        private readonly IVendasClassificacaoLojaTotalStoredProcedure _vendasClassificacaoLojaTotalStoredProcedure;
        private readonly IVendaClassificacaoLojaTotalRepository _vendaClassificacaoLojaTotalRepository;
        private readonly IVendaClassificacaoLojaRepository _vendaClassificacaoLojaRepository;

        public LojasController(IUserAppService userAppService,
                                     ICompanyAppService companyAppService,
                                     IClsCabRepository clsCabRepository,
                                     IVenUadRepository venUadRepository,
                                     IVenUadClsDashRepository venUadClsDashRepository,
                                     IVenUadMensalRepository venUadMensalRepository,
                                     IUadCabAppService uadCabAppService,
                                     IClsVenAllGraficoTotalRepository clsVenAllGraficoTotalRepository,
                                     IVendaLojaPorHoraAppService vendaLojaPorHoraAppService,
                                     IVendasLojaPorDiaHoraAppService vendasLojaPorDiaHoraAppService,
                                     IVendasClassificacaoLojaStoredProcedure vendasClassificacaoLojaStoredProcedure,
                                     IVendasClassificacaoLojaTotalStoredProcedure vendasClassificacaoLojaTotalStoredProcedure,
                                     IVendaClassificacaoLojaTotalRepository vendaClassificacaoLojaTotalRepository,
                                     IVendaClassificacaoLojaRepository vendaClassificacaoLojaRepository)
                                     : base(userAppService, companyAppService)
        {
            _clsCabRepository = clsCabRepository;
            _venUadRepository = venUadRepository;
            _venUadClsDashRepository = venUadClsDashRepository;
            _venUadMensalRepository = venUadMensalRepository;
            _uadCabAppService = uadCabAppService;
            _clsVenAllGraficoTotalRepository = clsVenAllGraficoTotalRepository;
            _vendaLojaPorHoraAppService = vendaLojaPorHoraAppService;
            _vendasLojaPorDiaHoraAppService = vendasLojaPorDiaHoraAppService;
            _vendasClassificacaoLojaStoredProcedure = vendasClassificacaoLojaStoredProcedure;
            _vendasClassificacaoLojaTotalStoredProcedure = vendasClassificacaoLojaTotalStoredProcedure;
            _vendaClassificacaoLojaTotalRepository = vendaClassificacaoLojaTotalRepository;
            _vendaClassificacaoLojaRepository = vendaClassificacaoLojaRepository;
        }

        [ClaimsAuthorize("ChartIndicadorGeralJoja", "Allowed")]
        public ActionResult IndicadorGeral()
        {
            ViewBag.Years = ListGeneratorHelper.GenerateYears();
            ViewBag.Months = ListGeneratorHelper.GenerateMonths();
            var store = _uadCabAppService.GetStores(CompanyUser.GetDbConnection());
            if (LoggedUser.UserStoresIds.Any())
            {
                store = store.Where(s => LoggedUser.UserStoresIds.Any(u => u.Equals(s.Uad)));
            }
            ViewBag.Stores = new SelectList(store, "Uad", "Des");
            ViewBag.Cliente = (LoggedUser != null ? LoggedUser.Name : string.Empty);
            return View();
        }

        [HttpPost]
        [ClaimsAuthorize("ChartIndicadorGeralJoja", "Allowed")]
        public JsonResult RetornarTicketMedioMeses(string ano, int? loja)
        {
            JsonResult result;
            var listRetorno = new List<TicketMedio>();
            try
            {
                var anoAnterior = Convert.ToInt32(ano) - 1;
                var anoAnteriorString = anoAnterior.ToString();
                var venAll = new List<VenUad>();
                if (loja == null)
                {
                    if (LoggedUser.UserStoresIds.Any())
                    {
                        venAll = _venUadRepository.GetByFilter(v => (v.My.StartsWith(ano) || v.My.StartsWith(anoAnteriorString)) &&
                                                               LoggedUser.UserStoresIds.Contains(v.Uad.Value), CompanyUser.GetDbConnection()).ToList();
                    }
                    else
                    {
                        venAll = _venUadRepository.GetByFilter(v => v.My.StartsWith(ano) || v.My.StartsWith(anoAnteriorString), CompanyUser.GetDbConnection()).ToList();
                    }                    
                }
                else
                {
                    if (LoggedUser.UserStoresIds.Any())
                    {
                        venAll = _venUadRepository.GetByFilter(v => v.Uad == loja && 
                                                              (v.My.StartsWith(ano) || v.My.StartsWith(anoAnteriorString))
                                                              && LoggedUser.UserStoresIds.Contains(v.Uad.Value), CompanyUser.GetDbConnection()).ToList();
                    }
                    else
                    {
                        venAll = _venUadRepository.GetByFilter(v => v.Uad == loja && (v.My.StartsWith(ano) || v.My.StartsWith(anoAnteriorString)), CompanyUser.GetDbConnection()).ToList();
                    }                    
                }
                var venAllJaneiro = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("01"));
                var venAllFevereiro = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("02"));
                var venAllMarço = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("03"));
                var venAllAbril = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("04"));
                var venAllMaio = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("05"));
                var venAllJunho = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("06"));
                var venAllJulho = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("07"));
                var venAllAgosto = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("08"));
                var venAllSetembro = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("09"));
                var venAllOutubro = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("10"));
                var venAllNovembro = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("11"));
                var venAllDezembro = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("12"));

                var venAllJaneiroAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("01"));
                var venAllFevereiroAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("02"));
                var venAllMarçoAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("03"));
                var venAllAbrilAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("04"));
                var venAllMaioAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("05"));
                var venAllJunhoAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("06"));
                var venAllJulhoAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("07"));
                var venAllAgostoAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("08"));
                var venAllSetembroAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("09"));
                var venAllOutubroAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("10"));
                var venAllNovembroAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("11"));
                var venAllDezembroAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("12"));


                var janeiroVlb = venAllJaneiro.Count() > 0 ? venAllJaneiro.Sum(v => v.Vlb) : 0;
                var fevereiroVlb = venAllFevereiro.Count() > 0 ? venAllFevereiro.Sum(v => v.Vlb) : 0;
                var marçoVlb = venAllMarço.Count() > 0 ? venAllMarço.Sum(v => v.Vlb) : 0;
                var abrilVlb = venAllAbril.Count() > 0 ? venAllAbril.Sum(v => v.Vlb) : 0;
                var maioVlb = venAllMaio.Count() > 0 ? venAllMaio.Sum(v => v.Vlb) : 0;
                var junhoVlb = venAllJunho.Count() > 0 ? venAllJunho.Sum(v => v.Vlb) : 0;
                var julhoVlb = venAllJulho.Count() > 0 ? venAllJulho.Sum(v => v.Vlb) : 0;
                var agostoVlb = venAllAgosto.Count() > 0 ? venAllAgosto.Sum(v => v.Vlb) : 0;
                var setembroVlb = venAllSetembro.Count() > 0 ? venAllSetembro.Sum(v => v.Vlb) : 0;
                var outubroVlb = venAllOutubro.Count() > 0 ? venAllOutubro.Sum(v => v.Vlb) : 0;
                var novembroVlb = venAllNovembro.Count() > 0 ? venAllNovembro.Sum(v => v.Vlb) : 0;
                var dezembroVlb = venAllDezembro.Count() > 0 ? venAllDezembro.Sum(v => v.Vlb) : 0;



                var janeiroVld = venAllJaneiro.Count() > 0 ? venAllJaneiro.Sum(v => v.Vld) : 0;
                var fevereiroVld = venAllFevereiro.Count() > 0 ? venAllFevereiro.Sum(v => v.Vld) : 0;
                var marçoVld = venAllMarço.Count() > 0 ? venAllMarço.Sum(v => v.Vld) : 0;
                var abrilVld = venAllAbril.Count() > 0 ? venAllAbril.Sum(v => v.Vld) : 0;
                var maioVld = venAllMaio.Count() > 0 ? venAllMaio.Sum(v => v.Vld) : 0;
                var junhoVld = venAllJunho.Count() > 0 ? venAllJunho.Sum(v => v.Vld) : 0;
                var julhoVld = venAllJulho.Count() > 0 ? venAllJulho.Sum(v => v.Vld) : 0;
                var agostoVld = venAllAgosto.Count() > 0 ? venAllAgosto.Sum(v => v.Vld) : 0;
                var setembroVld = venAllSetembro.Count() > 0 ? venAllSetembro.Sum(v => v.Vld) : 0;
                var outubroVld = venAllOutubro.Count() > 0 ? venAllOutubro.Sum(v => v.Vld) : 0;
                var novembroVld = venAllNovembro.Count() > 0 ? venAllNovembro.Sum(v => v.Vld) : 0;
                var dezembroVld = venAllDezembro.Count() > 0 ? venAllDezembro.Sum(v => v.Vld) : 0;

                var janeiroReg = venAllJaneiro.Count() > 0 ? venAllJaneiro.Sum(v => v.Reg) : 0;
                var fevereiroReg = venAllFevereiro.Count() > 0 ? venAllFevereiro.Sum(v => v.Reg) : 0;
                var marçoReg = venAllMarço.Count() > 0 ? venAllMarço.Sum(v => v.Reg) : 0;
                var abrilReg = venAllAbril.Count() > 0 ? venAllAbril.Sum(v => v.Reg) : 0;
                var maioReg = venAllMaio.Count() > 0 ? venAllMaio.Sum(v => v.Reg) : 0;
                var junhoReg = venAllJunho.Count() > 0 ? venAllJunho.Sum(v => v.Reg) : 0;
                var julhoReg = venAllJulho.Count() > 0 ? venAllJulho.Sum(v => v.Reg) : 0;
                var agostoReg = venAllAgosto.Count() > 0 ? venAllAgosto.Sum(v => v.Reg) : 0;
                var setembroReg = venAllSetembro.Count() > 0 ? venAllSetembro.Sum(v => v.Reg) : 0;
                var outubroReg = venAllOutubro.Count() > 0 ? venAllOutubro.Sum(v => v.Reg) : 0;
                var novembroReg = venAllNovembro.Count() > 0 ? venAllNovembro.Sum(v => v.Reg) : 0;
                var dezembroReg = venAllDezembro.Count() > 0 ? venAllDezembro.Sum(v => v.Reg) : 0;


                var janeiroAnteriorVlb = venAllJaneiroAnterior.Count() > 0 ? venAllJaneiroAnterior.Sum(v => v.Vlb) : 0;
                var fevereiroAnteriorVlb = venAllFevereiroAnterior.Count() > 0 ? venAllFevereiroAnterior.Sum(v => v.Vlb) : 0;
                var marçoAnteriorVlb = venAllMarçoAnterior.Count() > 0 ? venAllMarçoAnterior.Sum(v => v.Vlb) : 0;
                var abrilAnteriorVlb = venAllAbrilAnterior.Count() > 0 ? venAllAbrilAnterior.Sum(v => v.Vlb) : 0;
                var maioAnteriorVlb = venAllMaioAnterior.Count() > 0 ? venAllMaioAnterior.Sum(v => v.Vlb) : 0;
                var junhoAnteriorVlb = venAllJunhoAnterior.Count() > 0 ? venAllJunhoAnterior.Sum(v => v.Vlb) : 0;
                var julhoAnteriorVlb = venAllJulhoAnterior.Count() > 0 ? venAllJulhoAnterior.Sum(v => v.Vlb) : 0;
                var agostoAnteriorVlb = venAllAgostoAnterior.Count() > 0 ? venAllAgostoAnterior.Sum(v => v.Vlb) : 0;
                var setembroAnteriorVlb = venAllSetembroAnterior.Count() > 0 ? venAllSetembroAnterior.Sum(v => v.Vlb) : 0;
                var outubroAnteriorVlb = venAllOutubroAnterior.Count() > 0 ? venAllOutubroAnterior.Sum(v => v.Vlb) : 0;
                var novembroAnteriorVlb = venAllNovembroAnterior.Count() > 0 ? venAllNovembroAnterior.Sum(v => v.Vlb) : 0;
                var dezembroAnteriorVlb = venAllDezembroAnterior.Count() > 0 ? venAllDezembroAnterior.Sum(v => v.Vlb) : 0;



                var janeiroAnteriorVld = venAllJaneiroAnterior.Count() > 0 ? venAllJaneiroAnterior.Sum(v => v.Vld) : 0;
                var fevereiroAnteriorVld = venAllFevereiroAnterior.Count() > 0 ? venAllFevereiroAnterior.Sum(v => v.Vld) : 0;
                var marçoAnteriorVld = venAllMarçoAnterior.Count() > 0 ? venAllMarçoAnterior.Sum(v => v.Vld) : 0;
                var abrilAnteriorVld = venAllAbrilAnterior.Count() > 0 ? venAllAbrilAnterior.Sum(v => v.Vld) : 0;
                var maioAnteriorVld = venAllMaioAnterior.Count() > 0 ? venAllMaioAnterior.Sum(v => v.Vld) : 0;
                var junhoAnteriorVld = venAllJunhoAnterior.Count() > 0 ? venAllJunhoAnterior.Sum(v => v.Vld) : 0;
                var julhoAnteriorVld = venAllJulhoAnterior.Count() > 0 ? venAllJulhoAnterior.Sum(v => v.Vld) : 0;
                var agostoAnteriorVld = venAllAgostoAnterior.Count() > 0 ? venAllAgostoAnterior.Sum(v => v.Vld) : 0;
                var setembroAnteriorVld = venAllSetembroAnterior.Count() > 0 ? venAllSetembroAnterior.Sum(v => v.Vld) : 0;
                var outubroAnteriorVld = venAllOutubroAnterior.Count() > 0 ? venAllOutubroAnterior.Sum(v => v.Vld) : 0;
                var novembroAnteriorVld = venAllNovembroAnterior.Count() > 0 ? venAllNovembroAnterior.Sum(v => v.Vld) : 0;
                var dezembroAnteriorVld = venAllDezembroAnterior.Count() > 0 ? venAllDezembroAnterior.Sum(v => v.Vld) : 0;

                var janeiroAnteriorReg = venAllJaneiroAnterior.Count() > 0 ? venAllJaneiroAnterior.Sum(v => v.Reg) : 0;
                var fevereiroAnteriorReg = venAllFevereiroAnterior.Count() > 0 ? venAllFevereiroAnterior.Sum(v => v.Reg) : 0;
                var marçoAnteriorReg = venAllMarçoAnterior.Count() > 0 ? venAllMarçoAnterior.Sum(v => v.Reg) : 0;
                var abrilAnteriorReg = venAllAbrilAnterior.Count() > 0 ? venAllAbrilAnterior.Sum(v => v.Reg) : 0;
                var maioAnteriorReg = venAllMaioAnterior.Count() > 0 ? venAllMaioAnterior.Sum(v => v.Reg) : 0;
                var junhoAnteriorReg = venAllJunhoAnterior.Count() > 0 ? venAllJunhoAnterior.Sum(v => v.Reg) : 0;
                var julhoAnteriorReg = venAllJulhoAnterior.Count() > 0 ? venAllJulhoAnterior.Sum(v => v.Reg) : 0;
                var agostoAnteriorReg = venAllAgostoAnterior.Count() > 0 ? venAllAgostoAnterior.Sum(v => v.Reg) : 0;
                var setembroAnteriorReg = venAllSetembroAnterior.Count() > 0 ? venAllSetembroAnterior.Sum(v => v.Reg) : 0;
                var outubroAnteriorReg = venAllOutubroAnterior.Count() > 0 ? venAllOutubroAnterior.Sum(v => v.Reg) : 0;
                var novembroAnteriorReg = venAllNovembroAnterior.Count() > 0 ? venAllNovembroAnterior.Sum(v => v.Reg) : 0;
                var dezembroAnteriorReg = venAllDezembroAnterior.Count() > 0 ? venAllDezembroAnterior.Sum(v => v.Reg) : 0;



                var objeto = new TicketMedio
                {
                    MediaJaneiro = janeiroReg != 0 ? ((janeiroVlb - janeiroVld) / janeiroReg) : 0,
                    MediaFevereiro = fevereiroReg != 0 ? ((fevereiroVlb - fevereiroVld) / fevereiroReg) : 0,
                    MediaMarço = marçoReg != 0 ? ((marçoVlb - marçoVld) / marçoReg) : 0,
                    MediaAbril = abrilReg != 0 ? ((abrilVlb - abrilVld) / abrilReg) : 0,
                    MediaMaio = maioReg != 0 ? ((maioVlb - maioVld) / maioReg) : 0,
                    MediaJunho = junhoReg != 0 ? ((junhoVlb - junhoVld) / junhoReg) : 0,
                    MediaJulho = julhoReg != 0 ? ((julhoVlb - julhoVld) / julhoReg) : 0,
                    MediaAgosto = agostoReg != 0 ? ((agostoVlb - agostoVld) / agostoReg) : 0,
                    MediaSetembro = setembroReg != 0 ? ((setembroVlb - setembroVld) / setembroReg) : 0,
                    MediaOutubro = outubroReg != 0 ? ((outubroVlb - outubroVld) / outubroReg) : 0,
                    MediaNovembro = novembroReg != 0 ? ((novembroVlb - novembroVld) / novembroReg) : 0,
                    MediaDezembro = dezembroReg != 0 ? ((dezembroVlb - dezembroVld) / dezembroReg) : 0,

                    MediaJaneiroAnterior = janeiroAnteriorReg != 0 ? ((janeiroAnteriorVlb - janeiroAnteriorVld) / janeiroAnteriorReg) : 0,
                    MediaFevereiroAnterior = fevereiroAnteriorReg != 0 ? ((fevereiroAnteriorVlb - fevereiroAnteriorVld) / fevereiroAnteriorReg) : 0,
                    MediaMarçoAnterior = marçoAnteriorReg != 0 ? ((marçoAnteriorVlb - marçoAnteriorVld) / marçoAnteriorReg) : 0,
                    MediaAbrilAnterior = abrilAnteriorReg != 0 ? ((abrilAnteriorVlb - abrilAnteriorVld) / abrilAnteriorReg) : 0,
                    MediaMaioAnterior = maioAnteriorReg != 0 ? ((maioAnteriorVlb - maioAnteriorVld) / maioAnteriorReg) : 0,
                    MediaJunhoAnterior = junhoAnteriorReg != 0 ? ((junhoAnteriorVlb - junhoAnteriorVld) / junhoAnteriorReg) : 0,
                    MediaJulhoAnterior = julhoAnteriorReg != 0 ? ((julhoAnteriorVlb - julhoAnteriorVld) / julhoAnteriorReg) : 0,
                    MediaAgostoAnterior = agostoAnteriorReg != 0 ? ((agostoAnteriorVlb - agostoAnteriorVld) / agostoAnteriorReg) : 0,
                    MediaSetembroAnterior = setembroAnteriorReg != 0 ? ((setembroAnteriorVlb - setembroAnteriorVld) / setembroAnteriorReg) : 0,
                    MediaOutubroAnterior = outubroAnteriorReg != 0 ? ((outubroAnteriorVlb - outubroAnteriorVld) / outubroAnteriorReg) : 0,
                    MediaNovembroAnterior = novembroAnteriorReg != 0 ? ((novembroAnteriorVlb - novembroAnteriorVld) / novembroAnteriorReg) : 0,
                    MediaDezembroAnterior = dezembroAnteriorReg != 0 ? ((dezembroAnteriorVlb - dezembroAnteriorVld) / dezembroAnteriorReg) : 0
                };
                listRetorno.Add(objeto);
                result = Json(listRetorno, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = Json(new HttpStatusCodeResult(400, "Ocorreu uma falha ao carregar o gráfico."), JsonRequestBehavior.AllowGet);
            }
            return result;
        }

        [HttpPost]
        //[ClaimsAuthorize("ChartIndicadorGeralJoja", "Allowed")]
        public JsonResult RetornarTicketMedioClientesMeses(string ano, int? loja)
        {
            JsonResult result;
            var listRetorno = new List<TicketMedio>();
            try
            {               
                var anoAnterior = Convert.ToInt32(ano) - 1;
                var anoAnteriorString = anoAnterior.ToString();
                var venAll = new List<VenUad>();
                if (loja == null)
                {
                    if (LoggedUser.UserStoresIds.Any())
                    {
                        venAll = _venUadRepository.GetByFilter(v => (v.My.StartsWith(ano) || v.My.StartsWith(anoAnteriorString))
                                                               && LoggedUser.UserStoresIds.Contains(v.Uad.Value), CompanyUser.GetDbConnection()).ToList();
                    }
                    else
                    {
                        venAll = _venUadRepository.GetByFilter(v => v.My.StartsWith(ano) || v.My.StartsWith(anoAnteriorString), CompanyUser.GetDbConnection()).ToList();
                    }                    
                }
                else
                {
                    if (LoggedUser.UserStoresIds.Any())
                    {
                        venAll = _venUadRepository.GetByFilter(v => v.Uad == loja && (v.My.StartsWith(ano) || v.My.StartsWith(anoAnteriorString))
                                                               && LoggedUser.UserStoresIds.Contains(v.Uad.Value), CompanyUser.GetDbConnection()).ToList();
                    }
                    else
                    {
                        venAll = _venUadRepository.GetByFilter(v => v.Uad == loja && (v.My.StartsWith(ano) || v.My.StartsWith(anoAnteriorString)), CompanyUser.GetDbConnection()).ToList();
                    }                    
                }
                var venAllJaneiro = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("01"));
                var venAllFevereiro = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("02"));
                var venAllMarço = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("03"));
                var venAllAbril = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("04"));
                var venAllMaio = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("05"));
                var venAllJunho = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("06"));
                var venAllJulho = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("07"));
                var venAllAgosto = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("08"));
                var venAllSetembro = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("09"));
                var venAllOutubro = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("10"));
                var venAllNovembro = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("11"));
                var venAllDezembro = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("12"));

                var venAllJaneiroAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("01"));
                var venAllFevereiroAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("02"));
                var venAllMarçoAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("03"));
                var venAllAbrilAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("04"));
                var venAllMaioAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("05"));
                var venAllJunhoAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("06"));
                var venAllJulhoAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("07"));
                var venAllAgostoAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("08"));
                var venAllSetembroAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("09"));
                var venAllOutubroAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("10"));
                var venAllNovembroAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("11"));
                var venAllDezembroAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("12"));

                var janeiroReg = venAllJaneiro.Count() > 0 ? venAllJaneiro.Sum(v => v.Reg) : 0;
                var fevereiroReg = venAllFevereiro.Count() > 0 ? venAllFevereiro.Sum(v => v.Reg) : 0;
                var marçoReg = venAllMarço.Count() > 0 ? venAllMarço.Sum(v => v.Reg) : 0;
                var abrilReg = venAllAbril.Count() > 0 ? venAllAbril.Sum(v => v.Reg) : 0;
                var maioReg = venAllMaio.Count() > 0 ? venAllMaio.Sum(v => v.Reg) : 0;
                var junhoReg = venAllJunho.Count() > 0 ? venAllJunho.Sum(v => v.Reg) : 0;
                var julhoReg = venAllJulho.Count() > 0 ? venAllJulho.Sum(v => v.Reg) : 0;
                var agostoReg = venAllAgosto.Count() > 0 ? venAllAgosto.Sum(v => v.Reg) : 0;
                var setembroReg = venAllSetembro.Count() > 0 ? venAllSetembro.Sum(v => v.Reg) : 0;
                var outubroReg = venAllOutubro.Count() > 0 ? venAllOutubro.Sum(v => v.Reg) : 0;
                var novembroReg = venAllNovembro.Count() > 0 ? venAllNovembro.Sum(v => v.Reg) : 0;
                var dezembroReg = venAllDezembro.Count() > 0 ? venAllDezembro.Sum(v => v.Reg) : 0;

                var janeiroAnteriorReg = venAllJaneiroAnterior.Count() > 0 ? venAllJaneiroAnterior.Sum(v => v.Reg) : 0;
                var fevereiroAnteriorReg = venAllFevereiroAnterior.Count() > 0 ? venAllFevereiroAnterior.Sum(v => v.Reg) : 0;
                var marçoAnteriorReg = venAllMarçoAnterior.Count() > 0 ? venAllMarçoAnterior.Sum(v => v.Reg) : 0;
                var abrilAnteriorReg = venAllAbrilAnterior.Count() > 0 ? venAllAbrilAnterior.Sum(v => v.Reg) : 0;
                var maioAnteriorReg = venAllMaioAnterior.Count() > 0 ? venAllMaioAnterior.Sum(v => v.Reg) : 0;
                var junhoAnteriorReg = venAllJunhoAnterior.Count() > 0 ? venAllJunhoAnterior.Sum(v => v.Reg) : 0;
                var julhoAnteriorReg = venAllJulhoAnterior.Count() > 0 ? venAllJulhoAnterior.Sum(v => v.Reg) : 0;
                var agostoAnteriorReg = venAllAgostoAnterior.Count() > 0 ? venAllAgostoAnterior.Sum(v => v.Reg) : 0;
                var setembroAnteriorReg = venAllSetembroAnterior.Count() > 0 ? venAllSetembroAnterior.Sum(v => v.Reg) : 0;
                var outubroAnteriorReg = venAllOutubroAnterior.Count() > 0 ? venAllOutubroAnterior.Sum(v => v.Reg) : 0;
                var novembroAnteriorReg = venAllNovembroAnterior.Count() > 0 ? venAllNovembroAnterior.Sum(v => v.Reg) : 0;
                var dezembroAnteriorReg = venAllDezembroAnterior.Count() > 0 ? venAllDezembroAnterior.Sum(v => v.Reg) : 0;

                var objeto = new TicketMedio
                {
                    MediaJaneiro = janeiroReg,
                    MediaFevereiro = fevereiroReg,
                    MediaMarço = marçoReg,
                    MediaAbril = abrilReg,
                    MediaMaio = maioReg,
                    MediaJunho = junhoReg,
                    MediaJulho = julhoReg,
                    MediaAgosto = agostoReg,
                    MediaSetembro = setembroReg,
                    MediaOutubro = outubroReg,
                    MediaNovembro = novembroReg,
                    MediaDezembro = dezembroReg,

                    MediaJaneiroAnterior = janeiroAnteriorReg,
                    MediaFevereiroAnterior = fevereiroAnteriorReg,
                    MediaMarçoAnterior = marçoAnteriorReg,
                    MediaAbrilAnterior = abrilAnteriorReg,
                    MediaMaioAnterior = maioAnteriorReg,
                    MediaJunhoAnterior = junhoAnteriorReg,
                    MediaJulhoAnterior = julhoAnteriorReg,
                    MediaAgostoAnterior = agostoAnteriorReg,
                    MediaSetembroAnterior = setembroAnteriorReg,
                    MediaOutubroAnterior = outubroAnteriorReg,
                    MediaNovembroAnterior = novembroAnteriorReg,
                    MediaDezembroAnterior = dezembroAnteriorReg
                };
                listRetorno.Add(objeto);
                result = Json(listRetorno, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = Json(new HttpStatusCodeResult(400, "Ocorreu uma falha ao carregar o gráfico."), JsonRequestBehavior.AllowGet);
            }
            return result;
        }

        [HttpPost]
        [ClaimsAuthorize("ChartIndicadorGeralJoja", "Allowed")]
        public JsonResult RetornarTicketMedioItensMeses(string ano, int? loja)
        {
            JsonResult result;
            var listRetorno = new List<TicketMedio>();
            try
            {
                var anoAnterior = Convert.ToInt32(ano) - 1;
                var anoAnteriorString = anoAnterior.ToString();
                var venAll = new List<VenUad>();
                if (loja == null)
                {
                    if (LoggedUser.UserStoresIds.Any())
                    {
                        venAll = _venUadRepository.GetByFilter(v => (v.My.StartsWith(ano) || v.My.StartsWith(anoAnteriorString))
                                                               && LoggedUser.UserStoresIds.Contains(v.Uad.Value), CompanyUser.GetDbConnection()).ToList();
                    }
                    else
                    {
                        venAll = _venUadRepository.GetByFilter(v => v.My.StartsWith(ano) || v.My.StartsWith(anoAnteriorString), CompanyUser.GetDbConnection()).ToList();
                    }                    
                }
                else
                {
                    if (LoggedUser.UserStoresIds.Any())
                    {
                        venAll = _venUadRepository.GetByFilter(v => v.Uad == loja && (v.My.StartsWith(ano) || v.My.StartsWith(anoAnteriorString))
                                                               && LoggedUser.UserStoresIds.Contains(v.Uad.Value), CompanyUser.GetDbConnection()).ToList();
                    }
                    else
                    {
                        venAll = _venUadRepository.GetByFilter(v => v.Uad == loja && (v.My.StartsWith(ano) || v.My.StartsWith(anoAnteriorString)), CompanyUser.GetDbConnection()).ToList();
                    }                    
                }
                var venAllJaneiro = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("01"));
                var venAllFevereiro = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("02"));
                var venAllMarço = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("03"));
                var venAllAbril = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("04"));
                var venAllMaio = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("05"));
                var venAllJunho = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("06"));
                var venAllJulho = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("07"));
                var venAllAgosto = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("08"));
                var venAllSetembro = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("09"));
                var venAllOutubro = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("10"));
                var venAllNovembro = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("11"));
                var venAllDezembro = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("12"));

                var venAllJaneiroAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("01"));
                var venAllFevereiroAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("02"));
                var venAllMarçoAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("03"));
                var venAllAbrilAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("04"));
                var venAllMaioAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("05"));
                var venAllJunhoAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("06"));
                var venAllJulhoAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("07"));
                var venAllAgostoAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("08"));
                var venAllSetembroAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("09"));
                var venAllOutubroAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("10"));
                var venAllNovembroAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("11"));
                var venAllDezembroAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("12"));

                var janeiroQtp = venAllJaneiro.Count() > 0 ? venAllJaneiro.Sum(v => v.Qtp) : 0;
                var fevereiroQtp = venAllFevereiro.Count() > 0 ? venAllFevereiro.Sum(v => v.Qtp) : 0;
                var marçoQtp = venAllMarço.Count() > 0 ? venAllMarço.Sum(v => v.Qtp) : 0;
                var abrilQtp = venAllAbril.Count() > 0 ? venAllAbril.Sum(v => v.Qtp) : 0;
                var maioQtp = venAllMaio.Count() > 0 ? venAllMaio.Sum(v => v.Qtp) : 0;
                var junhoQtp = venAllJunho.Count() > 0 ? venAllJunho.Sum(v => v.Qtp) : 0;
                var julhoQtp = venAllJulho.Count() > 0 ? venAllJulho.Sum(v => v.Qtp) : 0;
                var agostoQtp = venAllAgosto.Count() > 0 ? venAllAgosto.Sum(v => v.Qtp) : 0;
                var setembroQtp = venAllSetembro.Count() > 0 ? venAllSetembro.Sum(v => v.Qtp) : 0;
                var outubroQtp = venAllOutubro.Count() > 0 ? venAllOutubro.Sum(v => v.Qtp) : 0;
                var novembroQtp = venAllNovembro.Count() > 0 ? venAllNovembro.Sum(v => v.Qtp) : 0;
                var dezembroQtp = venAllDezembro.Count() > 0 ? venAllDezembro.Sum(v => v.Qtp) : 0;




                var janeiroAnteriorQtp = venAllJaneiroAnterior.Count() > 0 ? venAllJaneiroAnterior.Sum(v => v.Qtp) : 0;
                var fevereiroAnteriorQtp = venAllFevereiroAnterior.Count() > 0 ? venAllFevereiroAnterior.Sum(v => v.Qtp) : 0;
                var marçoAnteriorQtp = venAllMarçoAnterior.Count() > 0 ? venAllMarçoAnterior.Sum(v => v.Qtp) : 0;
                var abrilAnteriorQtp = venAllAbrilAnterior.Count() > 0 ? venAllAbrilAnterior.Sum(v => v.Qtp) : 0;
                var maioAnteriorQtp = venAllMaioAnterior.Count() > 0 ? venAllMaioAnterior.Sum(v => v.Qtp) : 0;
                var junhoAnteriorQtp = venAllJunhoAnterior.Count() > 0 ? venAllJunhoAnterior.Sum(v => v.Qtp) : 0;
                var julhoAnteriorQtp = venAllJulhoAnterior.Count() > 0 ? venAllJulhoAnterior.Sum(v => v.Qtp) : 0;
                var agostoAnteriorQtp = venAllAgostoAnterior.Count() > 0 ? venAllAgostoAnterior.Sum(v => v.Qtp) : 0;
                var setembroAnteriorQtp = venAllSetembroAnterior.Count() > 0 ? venAllSetembroAnterior.Sum(v => v.Qtp) : 0;
                var outubroAnteriorQtp = venAllOutubroAnterior.Count() > 0 ? venAllOutubroAnterior.Sum(v => v.Qtp) : 0;
                var novembroAnteriorQtp = venAllNovembroAnterior.Count() > 0 ? venAllNovembroAnterior.Sum(v => v.Qtp) : 0;
                var dezembroAnteriorQtp = venAllDezembroAnterior.Count() > 0 ? venAllDezembroAnterior.Sum(v => v.Qtp) : 0;



                var objeto = new TicketMedio
                {
                    MediaJaneiro = janeiroQtp,
                    MediaFevereiro = fevereiroQtp,
                    MediaMarço = marçoQtp,
                    MediaAbril = abrilQtp,
                    MediaMaio = maioQtp,
                    MediaJunho = junhoQtp,
                    MediaJulho = julhoQtp,
                    MediaAgosto = agostoQtp,
                    MediaSetembro = setembroQtp,
                    MediaOutubro = outubroQtp,
                    MediaNovembro = novembroQtp,
                    MediaDezembro = dezembroQtp,

                    MediaJaneiroAnterior = janeiroAnteriorQtp,
                    MediaFevereiroAnterior = fevereiroAnteriorQtp,
                    MediaMarçoAnterior = marçoAnteriorQtp,
                    MediaAbrilAnterior = abrilAnteriorQtp,
                    MediaMaioAnterior = maioAnteriorQtp,
                    MediaJunhoAnterior = junhoAnteriorQtp,
                    MediaJulhoAnterior = julhoAnteriorQtp,
                    MediaAgostoAnterior = agostoAnteriorQtp,
                    MediaSetembroAnterior = setembroAnteriorQtp,
                    MediaOutubroAnterior = outubroAnteriorQtp,
                    MediaNovembroAnterior = novembroAnteriorQtp,
                    MediaDezembroAnterior = dezembroAnteriorQtp,


                };
                listRetorno.Add(objeto);


                result = Json(listRetorno, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {


                result = Json(new HttpStatusCodeResult(400, "Ocorreu uma falha ao carregar o gráfico."), JsonRequestBehavior.AllowGet);
            }

            return result;
        }

        [HttpPost]
        [ClaimsAuthorize("ChartIndicadorGeralJoja", "Allowed")]
        public JsonResult RetornarLucroLiquidoMeses(string ano, int? loja)
        {
            JsonResult result;
            var listRetorno = new List<TicketMedio>();
            try
            {
                var anoAnterior = Convert.ToInt32(ano) - 1;
                var anoAnteriorString = anoAnterior.ToString();
                var venAll = new List<VenUad>();
                if (loja == null)
                {
                    if (LoggedUser.UserStoresIds.Any())
                    {
                        venAll = _venUadRepository.GetByFilter(v => (v.My.StartsWith(ano) || 
                                                                     v.My.StartsWith(anoAnteriorString)) 
                                                                    && LoggedUser.UserStoresIds.Contains(v.Uad.Value), CompanyUser.GetDbConnection()).ToList();
                    }
                    else
                    {
                        venAll = _venUadRepository.GetByFilter(v => v.My.StartsWith(ano) || v.My.StartsWith(anoAnteriorString), CompanyUser.GetDbConnection()).ToList();
                    }                    
                }
                else
                {
                    if (LoggedUser.UserStoresIds.Any())
                    {
                        venAll = _venUadRepository.GetByFilter(v => v.Uad == loja &&
                                                              (v.My.StartsWith(ano) || v.My.StartsWith(anoAnteriorString)) && 
                                                              LoggedUser.UserStoresIds.Contains(v.Uad.Value), CompanyUser.GetDbConnection()).ToList();
                    }
                    else
                    {
                        venAll = _venUadRepository.GetByFilter(v => v.Uad == loja && (v.My.StartsWith(ano) || v.My.StartsWith(anoAnteriorString)), CompanyUser.GetDbConnection()).ToList();
                    }                    
                }
                var venAllJaneiro = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("01"));
                var venAllFevereiro = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("02"));
                var venAllMarço = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("03"));
                var venAllAbril = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("04"));
                var venAllMaio = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("05"));
                var venAllJunho = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("06"));
                var venAllJulho = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("07"));
                var venAllAgosto = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("08"));
                var venAllSetembro = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("09"));
                var venAllOutubro = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("10"));
                var venAllNovembro = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("11"));
                var venAllDezembro = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("12"));

                var venAllJaneiroAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("01"));
                var venAllFevereiroAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("02"));
                var venAllMarçoAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("03"));
                var venAllAbrilAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("04"));
                var venAllMaioAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("05"));
                var venAllJunhoAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("06"));
                var venAllJulhoAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("07"));
                var venAllAgostoAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("08"));
                var venAllSetembroAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("09"));
                var venAllOutubroAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("10"));
                var venAllNovembroAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("11"));
                var venAllDezembroAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("12"));



                var janeiroLiquido = venAllJaneiro.Count() > 0 ? venAllJaneiro.Sum(v => v.Vlb) - venAllJaneiro.Sum(v => v.Vld) : 0;
                var fevereiroLiquido = venAllFevereiro.Count() > 0 ? venAllFevereiro.Sum(v => v.Vlb) - venAllFevereiro.Sum(v => v.Vld) : 0;
                var marçoLiquido = venAllMarço.Count() > 0 ? venAllMarço.Sum(v => v.Vlb) - venAllMarço.Sum(v => v.Vld) : 0;
                var abrilLiquido = venAllAbril.Count() > 0 ? venAllAbril.Sum(v => v.Vlb) - venAllAbril.Sum(v => v.Vld) : 0;
                var maioLiquido = venAllMaio.Count() > 0 ? venAllMaio.Sum(v => v.Vlb) - venAllMaio.Sum(v => v.Vld) : 0;
                var junhoLiquido = venAllJunho.Count() > 0 ? venAllJunho.Sum(v => v.Vlb) - venAllJunho.Sum(v => v.Vld) : 0;
                var julhoLiquido = venAllJulho.Count() > 0 ? venAllJulho.Sum(v => v.Vlb) - venAllJulho.Sum(v => v.Vld) : 0;
                var agostoLiquido = venAllAgosto.Count() > 0 ? venAllAgosto.Sum(v => v.Vlb) - venAllAgosto.Sum(v => v.Vld) : 0;
                var setembroLiquido = venAllSetembro.Count() > 0 ? venAllSetembro.Sum(v => v.Vlb) - venAllSetembro.Sum(v => v.Vld) : 0;
                var outubroLiquido = venAllOutubro.Count() > 0 ? venAllOutubro.Sum(v => v.Vlb) - venAllOutubro.Sum(v => v.Vld) : 0;
                var novembroLiquido = venAllNovembro.Count() > 0 ? venAllNovembro.Sum(v => v.Vlb) - venAllNovembro.Sum(v => v.Vld) : 0;
                var dezembroLiquido = venAllDezembro.Count() > 0 ? venAllDezembro.Sum(v => v.Vlb) - venAllDezembro.Sum(v => v.Vld) : 0;

                var janeiroAnteriorLiquido = venAllJaneiroAnterior.Count() > 0 ? venAllJaneiroAnterior.Sum(v => v.Vlb) - venAllJaneiroAnterior.Sum(v => v.Vld) : 0;
                var fevereiroAnteriorLiquido = venAllFevereiroAnterior.Count() > 0 ? venAllFevereiroAnterior.Sum(v => v.Vlb) - venAllFevereiroAnterior.Sum(v => v.Vld) : 0;
                var marçoAnteriorLiquido = venAllMarçoAnterior.Count() > 0 ? venAllMarçoAnterior.Sum(v => v.Vlb) - venAllMarçoAnterior.Sum(v => v.Vld) : 0;
                var abrilAnteriorLiquido = venAllAbrilAnterior.Count() > 0 ? venAllAbrilAnterior.Sum(v => v.Vlb) - venAllAbrilAnterior.Sum(v => v.Vld) : 0;
                var maioAnteriorLiquido = venAllMaioAnterior.Count() > 0 ? venAllMaioAnterior.Sum(v => v.Vlb) - venAllMaioAnterior.Sum(v => v.Vld) : 0;
                var junhoAnteriorLiquido = venAllJunhoAnterior.Count() > 0 ? venAllJunhoAnterior.Sum(v => v.Vlb) - venAllJunhoAnterior.Sum(v => v.Vld) : 0;
                var julhoAnteriorLiquido = venAllJulhoAnterior.Count() > 0 ? venAllJulhoAnterior.Sum(v => v.Vlb) - venAllJulhoAnterior.Sum(v => v.Vld) : 0;
                var agostoAnteriorLiquido = venAllAgostoAnterior.Count() > 0 ? venAllAgostoAnterior.Sum(v => v.Vlb) - venAllAgostoAnterior.Sum(v => v.Vld) : 0;
                var setembroAnteriorLiquido = venAllSetembroAnterior.Count() > 0 ? venAllSetembroAnterior.Sum(v => v.Vlb) - venAllSetembroAnterior.Sum(v => v.Vld) : 0;
                var outubroAnteriorLiquido = venAllOutubroAnterior.Count() > 0 ? venAllOutubroAnterior.Sum(v => v.Vlb) - venAllOutubroAnterior.Sum(v => v.Vld) : 0;
                var novembroAnteriorLiquido = venAllNovembroAnterior.Count() > 0 ? venAllNovembroAnterior.Sum(v => v.Vlb) - venAllNovembroAnterior.Sum(v => v.Vld) : 0;
                var dezembroAnteriorLiquido = venAllDezembroAnterior.Count() > 0 ? venAllDezembroAnterior.Sum(v => v.Vlb) - venAllDezembroAnterior.Sum(v => v.Vld) : 0;



                var objeto = new TicketMedio
                {
                    MediaJaneiro = janeiroLiquido,
                    MediaFevereiro = fevereiroLiquido,
                    MediaMarço = marçoLiquido,
                    MediaAbril = abrilLiquido,
                    MediaMaio = maioLiquido,
                    MediaJunho = junhoLiquido,
                    MediaJulho = julhoLiquido,
                    MediaAgosto = agostoLiquido,
                    MediaSetembro = setembroLiquido,
                    MediaOutubro = outubroLiquido,
                    MediaNovembro = novembroLiquido,
                    MediaDezembro = dezembroLiquido,

                    MediaJaneiroAnterior = janeiroAnteriorLiquido,
                    MediaFevereiroAnterior = fevereiroAnteriorLiquido,
                    MediaMarçoAnterior = marçoAnteriorLiquido,
                    MediaAbrilAnterior = abrilAnteriorLiquido,
                    MediaMaioAnterior = maioAnteriorLiquido,
                    MediaJunhoAnterior = junhoAnteriorLiquido,
                    MediaJulhoAnterior = julhoAnteriorLiquido,
                    MediaAgostoAnterior = agostoAnteriorLiquido,
                    MediaSetembroAnterior = setembroAnteriorLiquido,
                    MediaOutubroAnterior = outubroAnteriorLiquido,
                    MediaNovembroAnterior = novembroAnteriorLiquido,
                    MediaDezembroAnterior = dezembroAnteriorLiquido,


                };
                listRetorno.Add(objeto);


                result = Json(listRetorno, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {


                result = Json(new HttpStatusCodeResult(400, "Ocorreu uma falha ao carregar o gráfico."), JsonRequestBehavior.AllowGet);
            }

            return result;
        }

        [HttpPost]
        [ClaimsAuthorize("ChartIndicadorGeralJoja", "Allowed")]
        public JsonResult RetornarItensPorCliente(string ano, int? loja)
        {
            JsonResult result;
            var listRetorno = new List<TicketMedio>();
            try
            {
                var anoAnterior = Convert.ToInt32(ano) - 1;
                var anoAnteriorString = anoAnterior.ToString();
                var venAll = new List<VenUad>();
                if (loja == null)
                {
                    if (LoggedUser.UserStoresIds.Any())
                    {
                        venAll = _venUadRepository.GetByFilter(v => v.My.StartsWith(ano) || v.My.StartsWith(anoAnteriorString)
                                                               && LoggedUser.UserStoresIds.Contains(v.Uad.Value), CompanyUser.GetDbConnection()).ToList();
                    }
                    else
                    {
                        venAll = _venUadRepository.GetByFilter(v => v.My.StartsWith(ano) || v.My.StartsWith(anoAnteriorString), CompanyUser.GetDbConnection()).ToList();
                    }                    
                }
                else
                {
                    if (LoggedUser.UserStoresIds.Any())
                    {
                        venAll = _venUadRepository.GetByFilter(v => v.Uad == loja && (v.My.StartsWith(ano) || v.My.StartsWith(anoAnteriorString))
                                                               && LoggedUser.UserStoresIds.Contains(v.Uad.Value), CompanyUser.GetDbConnection()).ToList();
                    }
                    else
                    {
                        venAll = _venUadRepository.GetByFilter(v => v.Uad == loja && (v.My.StartsWith(ano) || v.My.StartsWith(anoAnteriorString)), CompanyUser.GetDbConnection()).ToList();
                    }                    
                }
                var venAllJaneiro = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("01"));
                var venAllFevereiro = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("02"));
                var venAllMarço = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("03"));
                var venAllAbril = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("04"));
                var venAllMaio = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("05"));
                var venAllJunho = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("06"));
                var venAllJulho = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("07"));
                var venAllAgosto = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("08"));
                var venAllSetembro = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("09"));
                var venAllOutubro = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("10"));
                var venAllNovembro = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("11"));
                var venAllDezembro = venAll.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("12"));

                var venAllJaneiroAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("01"));
                var venAllFevereiroAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("02"));
                var venAllMarçoAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("03"));
                var venAllAbrilAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("04"));
                var venAllMaioAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("05"));
                var venAllJunhoAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("06"));
                var venAllJulhoAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("07"));
                var venAllAgostoAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("08"));
                var venAllSetembroAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("09"));
                var venAllOutubroAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("10"));
                var venAllNovembroAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("11"));
                var venAllDezembroAnterior = venAll.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("12"));



                var janeiroItensCliente = venAllJaneiro.Count() > 0 ? venAllJaneiro.Sum(v => v.Qtp)  / venAllJaneiro.Sum(v => v.Reg) : 0;
                var fevereiroItensCliente = venAllFevereiro.Count() > 0 ? venAllFevereiro.Sum(v => v.Qtp) / venAllFevereiro.Sum(v => v.Reg) : 0;
                var marçoItensCliente = venAllMarço.Count() > 0 ? venAllMarço.Sum(v => v.Qtp) / venAllMarço.Sum(v => v.Reg) : 0;
                var abrilItensCliente = venAllAbril.Count() > 0 ? venAllAbril.Sum(v => v.Qtp) / venAllAbril.Sum(v => v.Reg) : 0;
                var maioItensCliente = venAllMaio.Count() > 0 ? venAllMaio.Sum(v => v.Qtp) / venAllMaio.Sum(v => v.Reg) : 0;
                var junhoItensCliente = venAllJunho.Count() > 0 ? venAllJunho.Sum(v => v.Qtp) / venAllJunho.Sum(v => v.Reg) : 0;
                var julhoItensCliente = venAllJulho.Count() > 0 ? venAllJulho.Sum(v => v.Qtp) / venAllJulho.Sum(v => v.Reg) : 0;
                var agostoItensCliente = venAllAgosto.Count() > 0 ? venAllAgosto.Sum(v => v.Qtp) / venAllAgosto.Sum(v => v.Reg) : 0;
                var setembroItensCliente = venAllSetembro.Count() > 0 ? venAllSetembro.Sum(v => v.Qtp) / venAllSetembro.Sum(v => v.Reg) : 0;
                var outubroItensCliente = venAllOutubro.Count() > 0 ? venAllOutubro.Sum(v => v.Qtp) / venAllOutubro.Sum(v => v.Reg) : 0;
                var novembroItensCliente = venAllNovembro.Count() > 0 ? venAllNovembro.Sum(v => v.Qtp) / venAllNovembro.Sum(v => v.Reg) : 0;
                var dezembroItensCliente = venAllDezembro.Count() > 0 ? venAllDezembro.Sum(v => v.Qtp) / venAllDezembro.Sum(v => v.Reg) : 0;




                var janeiroAnteriorItensCliente = venAllJaneiroAnterior.Count() > 0 ? venAllJaneiroAnterior.Sum(v => v.Qtp) / venAllJaneiroAnterior.Sum(v => v.Reg) : 0;
                var fevereiroAnteriorItensCliente = venAllFevereiroAnterior.Count() > 0 ? venAllFevereiroAnterior.Sum(v => v.Qtp) / venAllFevereiroAnterior.Sum(v => v.Reg) : 0;
                var marçoAnteriorItensCliente = venAllMarçoAnterior.Count() > 0 ? venAllMarçoAnterior.Sum(v => v.Qtp) / venAllMarçoAnterior.Sum(v => v.Reg) : 0;
                var abrilAnteriorItensCliente = venAllAbrilAnterior.Count() > 0 ? venAllAbrilAnterior.Sum(v => v.Qtp) / venAllAbrilAnterior.Sum(v => v.Reg) : 0;
                var maioAnteriorItensCliente = venAllMaioAnterior.Count() > 0 ? venAllMaioAnterior.Sum(v => v.Qtp) / venAllMaioAnterior.Sum(v => v.Reg) : 0;
                var junhoAnteriorItensCliente = venAllJunhoAnterior.Count() > 0 ? venAllJunhoAnterior.Sum(v => v.Qtp) / venAllJunhoAnterior.Sum(v => v.Reg) : 0;
                var julhoAnteriorItensCliente = venAllJulhoAnterior.Count() > 0 ? venAllJulhoAnterior.Sum(v => v.Qtp) / venAllJulhoAnterior.Sum(v => v.Reg) : 0;
                var agostoAnteriorItensCliente = venAllAgostoAnterior.Count() > 0 ? venAllAgostoAnterior.Sum(v => v.Qtp) / venAllAgostoAnterior.Sum(v => v.Reg) : 0;
                var setembroAnteriorItensCliente = venAllSetembroAnterior.Count() > 0 ? venAllSetembroAnterior.Sum(v => v.Qtp) / venAllSetembroAnterior.Sum(v => v.Reg) : 0;
                var outubroAnteriorItensCliente = venAllOutubroAnterior.Count() > 0 ? venAllOutubroAnterior.Sum(v => v.Qtp) / venAllOutubroAnterior.Sum(v => v.Reg) : 0;
                var novembroAnteriorItensCliente = venAllNovembroAnterior.Count() > 0 ? venAllNovembroAnterior.Sum(v => v.Qtp) / venAllNovembroAnterior.Sum(v => v.Reg) : 0;
                var dezembroAnteriorItensCliente = venAllDezembroAnterior.Count() > 0 ? venAllDezembroAnterior.Sum(v => v.Qtp) / venAllDezembroAnterior.Sum(v => v.Reg) : 0;


                var objeto = new TicketMedio
                {
                    MediaJaneiro = janeiroItensCliente,
                    MediaFevereiro = fevereiroItensCliente,
                    MediaMarço = marçoItensCliente,
                    MediaAbril = abrilItensCliente,
                    MediaMaio = maioItensCliente,
                    MediaJunho = junhoItensCliente,
                    MediaJulho = julhoItensCliente,
                    MediaAgosto = agostoItensCliente,
                    MediaSetembro = setembroItensCliente,
                    MediaOutubro = outubroItensCliente,
                    MediaNovembro = novembroItensCliente,
                    MediaDezembro = dezembroItensCliente,

                    MediaJaneiroAnterior = janeiroAnteriorItensCliente,
                    MediaFevereiroAnterior = fevereiroAnteriorItensCliente,
                    MediaMarçoAnterior = marçoAnteriorItensCliente,
                    MediaAbrilAnterior = abrilAnteriorItensCliente,
                    MediaMaioAnterior = maioAnteriorItensCliente,
                    MediaJunhoAnterior = junhoAnteriorItensCliente,
                    MediaJulhoAnterior = julhoAnteriorItensCliente,
                    MediaAgostoAnterior = agostoAnteriorItensCliente,
                    MediaSetembroAnterior = setembroAnteriorItensCliente,
                    MediaOutubroAnterior = outubroAnteriorItensCliente,
                    MediaNovembroAnterior = novembroAnteriorItensCliente,
                    MediaDezembroAnterior = dezembroAnteriorItensCliente


                };
                listRetorno.Add(objeto);



                result = Json(listRetorno, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                result = Json(new HttpStatusCodeResult(400, "Ocorreu uma falha ao carregar o gráfico."), JsonRequestBehavior.AllowGet);
            }

            return result;
        }

        [HttpPost]
        [ClaimsAuthorize("ChartLojaVendasHora", "Allowed")]
        public JsonResult VendasPorHorarioIndicador(int? lojaId, string ano)
        {
            //Action para buscar as vendas das lojas dividido por hora
            //Buscando todas as vendas por loja e hora
            var vendasLojasPorHora = new List<VendaLojaPorHoraViewModel>();
            if (lojaId != null)
            {
                vendasLojasPorHora = _vendaLojaPorHoraAppService.GetByFilter(v => v.My.StartsWith(ano) && v.Loja == lojaId, CompanyUser.GetDbConnection()).ToList();
                var vendasLojaPorHoraViewModel = new List<VendaLojaPorHoraViewModel>();
                //Interando as vendas dando um distinct por loja
                foreach (var vendasLojaPorHora in vendasLojasPorHora.GroupBy(v => v.Loja).Select(group => group.First()))
                {
                    //Buscando o nome da Loja
                    var loja = _uadCabAppService.GetById(vendasLojaPorHora.Loja, CompanyUser.GetDbConnection());
                    var vendaLojaPorHoraViewModel = new VendaLojaPorHoraViewModel();
                    vendaLojaPorHoraViewModel.Loja = vendasLojaPorHora.Loja;
                    vendaLojaPorHoraViewModel.Nome = loja.Des;
                    for (int i = 0; i <= 23; i++)
                    {
                        //Buscando as vendas totais de cada hora da loja
                        var vendas = vendasLojasPorHora.Where(v => v.Loja == vendasLojaPorHora.Loja && v.Hora == i);
                        //Caso possuir venda nesse horário buscar o valor vendido
                        if (vendas.Any())
                        {
                            vendaLojaPorHoraViewModel.HorasVendaViewModels.Add(new Application.Farmacia.ViewModels.Balconista.HoraVendaViewModel
                            {
                                Hora = i + ":00",
                                Valor = Convert.ToDecimal(vendas.Sum(v => v.Valor)),
                                TicketMedio = Convert.ToDecimal(vendas.Sum(v => v.Valor) / vendas.Sum(v => v.ClientesAtendidos)),
                                ClientesAtendidos = vendas.Sum(v => v.ClientesAtendidos)
                            });
                            vendaLojaPorHoraViewModel.ValorTotal += Convert.ToDecimal(vendas.Sum(v => v.Valor));
                        }
                        else
                        {
                            //Senão deve colocar o valor zerado no horário
                            vendaLojaPorHoraViewModel.HorasVendaViewModels.Add(new Application.Farmacia.ViewModels.Balconista.HoraVendaViewModel
                            {
                                Hora = i + ":00",
                                Valor = 0,
                                TicketMedio = 0,
                                ClientesAtendidos = 0
                            });
                        }
                    }
                    vendasLojaPorHoraViewModel.Add(vendaLojaPorHoraViewModel);
                }
                return Json(vendasLojaPorHoraViewModel, JsonRequestBehavior.AllowGet);
            }
            else
            {
                vendasLojasPorHora = _vendaLojaPorHoraAppService.GetByFilter(v => v.My.StartsWith(ano), CompanyUser.GetDbConnection()).ToList();
                var vendasLojaPorHoraViewModel = new List<VendaLojaPorHoraViewModel>();
                var vendaLojaPorHoraViewModel = new VendaLojaPorHoraViewModel();
                vendaLojaPorHoraViewModel.Nome = "Todas as Lojas";
                for (int i = 8; i <= 22; i++)
                {
                    //Buscando as vendas totais de cada hora da loja
                    var vendas = vendasLojasPorHora.Where(v => v.Hora == i);
                    //Caso possuir venda nesse horário buscar o valor vendido
                    if (vendas.Any())
                    {
                        vendaLojaPorHoraViewModel.HorasVendaViewModels.Add(new Application.Farmacia.ViewModels.Balconista.HoraVendaViewModel
                        {
                            Hora = i + ":00",
                            Valor = Convert.ToDecimal(vendas.Sum(v => v.Valor)),
                            TicketMedio = Convert.ToDecimal(vendas.Sum(v => v.Valor)) / vendas.Sum(v => v.ClientesAtendidos),
                            ClientesAtendidos = vendas.Sum(v => v.ClientesAtendidos)
                        });
                        vendaLojaPorHoraViewModel.ValorTotal += Convert.ToDecimal(vendas.Sum(v => v.Valor));
                    }
                    else
                    {
                        //Senão deve colocar o valor zerado no horário
                        vendaLojaPorHoraViewModel.HorasVendaViewModels.Add(new Application.Farmacia.ViewModels.Balconista.HoraVendaViewModel
                        {
                            Hora = i + ":00",
                            Valor = 0,
                            TicketMedio = 0,
                            ClientesAtendidos = 0
                        });
                    }
                }
                vendasLojaPorHoraViewModel.Add(vendaLojaPorHoraViewModel);                
                return Json(vendasLojaPorHoraViewModel, JsonRequestBehavior.AllowGet);
            }
            
        }

        [HttpGet]
        [ClaimsAuthorize("ChartIndicadorPorClassificacao", "Allowed")]
        public ActionResult IndicadorPorClassificacao()
        {
            ViewBag.Years = ListGeneratorHelper.GenerateYears();
            ViewBag.Months = ListGeneratorHelper.GenerateMonths();
            var store = _uadCabAppService.GetStores(CompanyUser.GetDbConnection());
            if (LoggedUser.UserStoresIds.Any())
            {
                store = store.Where(s => LoggedUser.UserStoresIds.Any(u => u.Equals(s.Uad)));
            }
            ViewBag.Stores = new SelectList(store, "Uad", "Des");
            ViewBag.Cliente = (LoggedUser != null ? LoggedUser.Name : string.Empty);
            return View();
        }

        [HttpPost]
        [ClaimsAuthorize("ChartIndicadorPorClassificacao", "Allowed")]
        public JsonResult RetornarClsVenAllDiasTotal(string mes, string ano, int? loja)
        {
            JsonResult result;
            var listRetorno = new List<SomaClassificacaoMesAno>();
            try
            {
                var mesAno = ano + mes;
                var clsVenAll = new List<ClsVenAllGraficoTotal>();
                if (loja == null)
                {
                    var vendas = new List<VendaClassificacaoLojaTotalSp>();
                    //Verificando de a conexão da empresa é MySql ou Postgre
                    if (CompanyUser.GetDbConnection().ToString() == "MySql.Data.MySqlClient.MySqlConnection")
                    {
                        //Buscando do repositório do MySql
                        if (LoggedUser.UserStoresIds.Any())
                        {
                            vendas = _vendaClassificacaoLojaTotalRepository.GetByFilter(v => v.Mes == mesAno && LoggedUser.UserStoresIds.Any(u => u.Equals(v.Uad)), CompanyUser.GetDbConnection()).ToList();
                        }
                        else
                        {
                            vendas = _vendaClassificacaoLojaTotalRepository.GetByFilter(v => v.Mes == mesAno, CompanyUser.GetDbConnection()).ToList();
                        }                        
                    }
                    else
                    {
                        //Buscando da Stored Procedure do Postgre
                        if (LoggedUser.UserStoresIds.Any())
                        {
                            vendas = _vendasClassificacaoLojaTotalStoredProcedure.Buscar(mesAno, LoggedUser.UserStoresIds, CompanyUser.GetConnectionString()).ToList();
                        }
                        else
                        {
                            vendas = _vendasClassificacaoLojaTotalStoredProcedure.Buscar(mesAno, CompanyUser.GetConnectionString()).ToList();
                        }                        
                    }
                    var disctintList = vendas.GroupBy(c => c.Uad).Select(group => group.First());
                    foreach (var dataFiltrada in vendas.GroupBy(c => c.Dias).Select(group => group.First()))
                    {
                        var objeto = new SomaClassificacaoMesAno
                        {
                            Classificação1 = Convert.ToDouble(vendas.Where(c => c.Dias == dataFiltrada.Dias).Sum(c => c.ValorBruto)),
                            Dia = dataFiltrada.Dias.Day,
                            Meta = Convert.ToDouble(disctintList.Sum(c => c.Meta)),
                            Percentual = Convert.ToDouble(disctintList.Sum(c => c.Percentual))
                        };
                        listRetorno.Add(objeto);
                    }
                }
                else
                {
                    var vendas = new List<VendaClassificacaoLojaTotalSp>();
                    if (CompanyUser.GetDbConnection().ToString() == "MySql.Data.MySqlClient.MySqlConnection")
                    {
                        if (LoggedUser.UserStoresIds.Any())
                        {
                            vendas = _vendaClassificacaoLojaTotalRepository.GetByFilter(v => v.Mes == mesAno && v.Uad == loja.Value && LoggedUser.UserStoresIds.Any(u => u.Equals(v.Uad)), CompanyUser.GetDbConnection()).ToList();
                        }
                        else
                        {
                            vendas = _vendaClassificacaoLojaTotalRepository.GetByFilter(v => v.Mes == mesAno && v.Uad == loja.Value, CompanyUser.GetDbConnection()).ToList();
                        }                        
                    }
                    else
                    {
                        if (LoggedUser.UserStoresIds.Any())
                        {
                            vendas = _vendasClassificacaoLojaTotalStoredProcedure.BuscarPorMyEUad(mesAno, loja.Value, LoggedUser.UserStoresIds, CompanyUser.GetConnectionString()).ToList();
                        }
                        else
                        {
                            vendas = _vendasClassificacaoLojaTotalStoredProcedure.BuscarPorMyEUad(mesAno, loja.Value, CompanyUser.GetConnectionString()).ToList();
                        }                        
                    }
                    var distinctList = vendas.GroupBy(c => c.Dias).Select(group => group.First());
                    foreach (var dataFiltrada in distinctList)
                    {
                        var objeto = new SomaClassificacaoMesAno
                        {
                            Classificação1 = Convert.ToDouble(vendas.Where(c => c.Dias == dataFiltrada.Dias).Sum(c => c.ValorBruto)),
                            Dia = dataFiltrada.Dias.Day,
                            Meta = Convert.ToDouble(vendas.FirstOrDefault(c => c.Dias == dataFiltrada.Dias && c.Uad == loja).Meta),
                            Percentual = Convert.ToDouble(vendas.FirstOrDefault(c => c.Dias == dataFiltrada.Dias && c.Uad == loja).Percentual)

                        };
                        listRetorno.Add(objeto);
                    }
                }                
                result = Json(listRetorno, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = Json(new HttpStatusCodeResult(400, "Ocorreu uma falha ao carregar o gráfico: " + ex), JsonRequestBehavior.AllowGet);
            }
            return result;
        }

        [HttpPost]
        [ClaimsAuthorize("ChartIndicadorPorClassificacao", "Allowed")]
        public JsonResult RetornarClsVenAllMesesTotal(string ano, int? loja)
        {
            JsonResult result;
            var listRetorno = new List<TicketMedio>();
            try
            {                
                var anoAnterior = Convert.ToInt32(ano) - 1;
                var anoAnteriorString = anoAnterior.ToString();
                var vendas = new List<VenUadMensal>();
                if (loja == null)
                {
                    if (LoggedUser.UserStoresIds.Any())
                    {
                        vendas = _venUadMensalRepository.GetByFilter(v => (v.My.StartsWith(ano) || v.My.StartsWith(anoAnteriorString)) && 
                                                                           LoggedUser.UserStoresIds.Contains(v.Uad), CompanyUser.GetDbConnection()).ToList();
                    }
                    else
                    {
                        vendas = _venUadMensalRepository.GetByFilter(v => v.My.StartsWith(ano) || v.My.StartsWith(anoAnteriorString), CompanyUser.GetDbConnection()).ToList();
                    }                    
                }
                else
                {
                    if (LoggedUser.UserStoresIds.Any())
                    {
                        vendas = _venUadMensalRepository.GetByFilter(v => v.Uad == loja && (v.My.StartsWith(ano) || v.My.StartsWith(anoAnteriorString)) 
                                                                     && LoggedUser.UserStoresIds.Contains(v.Uad), CompanyUser.GetDbConnection()).ToList();
                    }
                    else
                    {
                        vendas = _venUadMensalRepository.GetByFilter(v => v.Uad == loja && (v.My.StartsWith(ano) || v.My.StartsWith(anoAnteriorString)), CompanyUser.GetDbConnection()).ToList();
                    }                    
                }
                var clsVenAllJaneiro = vendas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("01"));
                var clsVenAllFevereiro = vendas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("02"));
                var clsVenAllMarço = vendas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("03"));
                var clsVenAllAbril = vendas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("04"));
                var clsVenAllMaio = vendas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("05"));
                var clsVenAllJunho = vendas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("06"));
                var clsVenAllJulho = vendas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("07"));
                var clsVenAllAgosto = vendas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("08"));
                var clsVenAllSetembro = vendas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("09"));
                var clsVenAllOutubro = vendas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("10"));
                var clsVenAllNovembro = vendas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("11"));
                var clsVenAllDezembro = vendas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("12"));

                var clsVenAllJaneiroAnterior = vendas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("01"));
                var clsVenAllFevereiroAnterior = vendas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("02"));
                var clsVenAllMarçoAnterior = vendas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("03"));
                var clsVenAllAbrilAnterior = vendas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("04"));
                var clsVenAllMaioAnterior = vendas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("05"));
                var clsVenAllJunhoAnterior = vendas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("06"));
                var clsVenAllJulhoAnterior = vendas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("07"));
                var clsVenAllAgostoAnterior = vendas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("08"));
                var clsVenAllSetembroAnterior = vendas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("09"));
                var clsVenAllOutubroAnterior = vendas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("10"));
                var clsVenAllNovembroAnterior = vendas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("11"));
                var clsVenAllDezembroAnterior = vendas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("12"));

                var janeiroCls = clsVenAllJaneiro.Count() > 0 ? clsVenAllJaneiro.Sum(v => v.ValorBruto) : 0;
                var fevereiroCls = clsVenAllFevereiro.Count() > 0 ? clsVenAllFevereiro.Sum(v => v.ValorBruto) : 0;
                var marçoCls = clsVenAllMarço.Count() > 0 ? clsVenAllMarço.Sum(v => v.ValorBruto) : 0;
                var abrilCls = clsVenAllAbril.Count() > 0 ? clsVenAllAbril.Sum(v => v.ValorBruto) : 0;
                var maioCls = clsVenAllMaio.Count() > 0 ? clsVenAllMaio.Sum(v => v.ValorBruto) : 0;
                var junhoCls = clsVenAllJunho.Count() > 0 ? clsVenAllJunho.Sum(v => v.ValorBruto) : 0;
                var julhoCls = clsVenAllJulho.Count() > 0 ? clsVenAllJulho.Sum(v => v.ValorBruto) : 0;
                var agostoCls = clsVenAllAgosto.Count() > 0 ? clsVenAllAgosto.Sum(v => v.ValorBruto) : 0;
                var setembroCls = clsVenAllSetembro.Count() > 0 ? clsVenAllSetembro.Sum(v => v.ValorBruto) : 0;
                var outubroCls = clsVenAllOutubro.Count() > 0 ? clsVenAllOutubro.Sum(v => v.ValorBruto) : 0;
                var novembroCls = clsVenAllNovembro.Count() > 0 ? clsVenAllNovembro.Sum(v => v.ValorBruto) : 0;
                var dezembroCls = clsVenAllDezembro.Count() > 0 ? clsVenAllDezembro.Sum(v => v.ValorBruto) : 0;




                var janeiroAnteriorCls = clsVenAllJaneiroAnterior.Count() > 0 ? clsVenAllJaneiroAnterior.Sum(v => v.ValorBruto) : 0;
                var fevereiroAnteriorCls = clsVenAllFevereiroAnterior.Count() > 0 ? clsVenAllFevereiroAnterior.Sum(v => v.ValorBruto) : 0;
                var marçoAnteriorCls = clsVenAllMarçoAnterior.Count() > 0 ? clsVenAllMarçoAnterior.Sum(v => v.ValorBruto) : 0;
                var abrilAnteriorCls = clsVenAllAbrilAnterior.Count() > 0 ? clsVenAllAbrilAnterior.Sum(v => v.ValorBruto) : 0;
                var maioAnteriorCls = clsVenAllMaioAnterior.Count() > 0 ? clsVenAllMaioAnterior.Sum(v => v.ValorBruto) : 0;
                var junhoAnteriorCls = clsVenAllJunhoAnterior.Count() > 0 ? clsVenAllJunhoAnterior.Sum(v => v.ValorBruto) : 0;
                var julhoAnteriorCls = clsVenAllJulhoAnterior.Count() > 0 ? clsVenAllJulhoAnterior.Sum(v => v.ValorBruto) : 0;
                var agostoAnteriorCls = clsVenAllAgostoAnterior.Count() > 0 ? clsVenAllAgostoAnterior.Sum(v => v.ValorBruto) : 0;
                var setembroAnteriorCls = clsVenAllSetembroAnterior.Count() > 0 ? clsVenAllSetembroAnterior.Sum(v => v.ValorBruto) : 0;
                var outubroAnteriorCls = clsVenAllOutubroAnterior.Count() > 0 ? clsVenAllOutubroAnterior.Sum(v => v.ValorBruto) : 0;
                var novembroAnteriorCls = clsVenAllNovembroAnterior.Count() > 0 ? clsVenAllNovembroAnterior.Sum(v => v.ValorBruto) : 0;
                var dezembroAnteriorCls = clsVenAllDezembroAnterior.Count() > 0 ? clsVenAllDezembroAnterior.Sum(v => v.ValorBruto) : 0;



                var objeto = new TicketMedio
                {
                    MediaJaneiro = janeiroCls,
                    MediaFevereiro = fevereiroCls,
                    MediaMarço = marçoCls,
                    MediaAbril = abrilCls,
                    MediaMaio = maioCls,
                    MediaJunho = junhoCls,
                    MediaJulho = julhoCls,
                    MediaAgosto = agostoCls,
                    MediaSetembro = setembroCls,
                    MediaOutubro = outubroCls,
                    MediaNovembro = novembroCls,
                    MediaDezembro = dezembroCls,

                    MediaJaneiroAnterior = janeiroAnteriorCls,
                    MediaFevereiroAnterior = fevereiroAnteriorCls,
                    MediaMarçoAnterior = marçoAnteriorCls,
                    MediaAbrilAnterior = abrilAnteriorCls,
                    MediaMaioAnterior = maioAnteriorCls,
                    MediaJunhoAnterior = junhoAnteriorCls,
                    MediaJulhoAnterior = julhoAnteriorCls,
                    MediaAgostoAnterior = agostoAnteriorCls,
                    MediaSetembroAnterior = setembroAnteriorCls,
                    MediaOutubroAnterior = outubroAnteriorCls,
                    MediaNovembroAnterior = novembroAnteriorCls,
                    MediaDezembroAnterior = dezembroAnteriorCls,
                };
                listRetorno.Add(objeto);


                result = Json(listRetorno, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {


                result = Json(new HttpStatusCodeResult(400, "Ocorreu uma falha ao carregar o gráfico."), JsonRequestBehavior.AllowGet);
            }

            return result;
        }

        [HttpPost]
        [ClaimsAuthorize("ChartIndicadorPorClassificacao", "Allowed")]
        public JsonResult RetornarClsVenAllMeses(string ano, int cls, int? loja)
        {
            JsonResult result;
            var listRetorno = new List<TicketMedio>();
            try
            {
                var anoAnterior = Convert.ToInt32(ano) - 1;
                var anoAnteriorString = anoAnterior.ToString();
                var vendas = new List<VenUadClsDash>();
                if (loja == null)
                {
                    if (LoggedUser.UserStoresIds.Any())
                    {
                        vendas = _venUadClsDashRepository.GetByFilter(v => v.Cls == cls && (v.My.StartsWith(ano) || 
                                                                      v.My.StartsWith(anoAnteriorString)) && 
                                                                      LoggedUser.UserStoresIds.Any(u => u.Equals(v.Uad)), CompanyUser.GetDbConnection()).ToList();
                    }
                    else
                    {
                        vendas = _venUadClsDashRepository.GetByFilter(v => v.Cls == cls && (v.My.StartsWith(ano) || v.My.StartsWith(anoAnteriorString)), CompanyUser.GetDbConnection()).ToList();
                    }
                }
                else
                {
                    if (LoggedUser.UserStoresIds.Any())
                    {
                        vendas = _venUadClsDashRepository.GetByFilter(v => v.Uad == loja && v.Cls == cls && (v.My.StartsWith(ano) || 
                                                                      v.My.StartsWith(anoAnteriorString)) && 
                                                                      LoggedUser.UserStoresIds.Any(u => u.Equals(v.Uad)), CompanyUser.GetDbConnection()).ToList();
                    }
                    else
                    {
                        vendas = _venUadClsDashRepository.GetByFilter(v => v.Uad == loja && v.Cls == cls && (v.My.StartsWith(ano) || v.My.StartsWith(anoAnteriorString)), CompanyUser.GetDbConnection()).ToList();
                    }                    
                }
                var nomeClassificacao1 = _clsCabRepository.GetByFilter(c => c.Cod == "1", CompanyUser.GetDbConnection()).FirstOrDefault().Des;
                var nomeClassificacao2 = _clsCabRepository.GetByFilter(c => c.Cod == "2", CompanyUser.GetDbConnection()).FirstOrDefault().Des;
                var nomeClassificacao3 = _clsCabRepository.GetByFilter(c => c.Cod == "3", CompanyUser.GetDbConnection()).FirstOrDefault().Des;
                var nomeClassificacao4 = _clsCabRepository.GetByFilter(c => c.Cod == "4", CompanyUser.GetDbConnection()).FirstOrDefault().Des;
                var nomeClassificacao5 = _clsCabRepository.GetByFilter(c => c.Cod == "5", CompanyUser.GetDbConnection()).FirstOrDefault().Des;
                var nomeClassificacao6 = _clsCabRepository.GetByFilter(c => c.Cod == "6", CompanyUser.GetDbConnection()).FirstOrDefault().Des;
                var nomeClassificacao7 = _clsCabRepository.GetByFilter(c => c.Cod == "7", CompanyUser.GetDbConnection()).FirstOrDefault().Des;
                var nomeClassificacao8 = _clsCabRepository.GetByFilter(c => c.Cod == "8", CompanyUser.GetDbConnection()).FirstOrDefault().Des;


                var clsVenAllJaneiro = vendas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("01"));
                var clsVenAllFevereiro = vendas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("02"));
                var clsVenAllMarço = vendas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("03"));
                var clsVenAllAbril = vendas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("04"));
                var clsVenAllMaio = vendas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("05"));
                var clsVenAllJunho = vendas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("06"));
                var clsVenAllJulho = vendas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("07"));
                var clsVenAllAgosto = vendas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("08"));
                var clsVenAllSetembro = vendas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("09"));
                var clsVenAllOutubro = vendas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("10"));
                var clsVenAllNovembro = vendas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("11"));
                var clsVenAllDezembro = vendas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("12"));

                var clsVenAllJaneiroAnterior = vendas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("01"));
                var clsVenAllFevereiroAnterior = vendas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("02"));
                var clsVenAllMarçoAnterior = vendas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("03"));
                var clsVenAllAbrilAnterior = vendas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("04"));
                var clsVenAllMaioAnterior = vendas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("05"));
                var clsVenAllJunhoAnterior = vendas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("06"));
                var clsVenAllJulhoAnterior = vendas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("07"));
                var clsVenAllAgostoAnterior = vendas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("08"));
                var clsVenAllSetembroAnterior = vendas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("09"));
                var clsVenAllOutubroAnterior = vendas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("10"));
                var clsVenAllNovembroAnterior = vendas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("11"));
                var clsVenAllDezembroAnterior = vendas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("12"));



                var janeiroCls = clsVenAllJaneiro.Count() > 0 ? clsVenAllJaneiro.Sum(v => v.ValorBruto) : 0;
                var fevereiroCls = clsVenAllFevereiro.Count() > 0 ? clsVenAllFevereiro.Sum(v => v.ValorBruto) : 0;
                var marçoCls = clsVenAllMarço.Count() > 0 ? clsVenAllMarço.Sum(v => v.ValorBruto) : 0;
                var abrilCls = clsVenAllAbril.Count() > 0 ? clsVenAllAbril.Sum(v => v.ValorBruto) : 0;
                var maioCls = clsVenAllMaio.Count() > 0 ? clsVenAllMaio.Sum(v => v.ValorBruto) : 0;
                var junhoCls = clsVenAllJunho.Count() > 0 ? clsVenAllJunho.Sum(v => v.ValorBruto) : 0;
                var julhoCls = clsVenAllJulho.Count() > 0 ? clsVenAllJulho.Sum(v => v.ValorBruto) : 0;
                var agostoCls = clsVenAllAgosto.Count() > 0 ? clsVenAllAgosto.Sum(v => v.ValorBruto) : 0;
                var setembroCls = clsVenAllSetembro.Count() > 0 ? clsVenAllSetembro.Sum(v => v.ValorBruto) : 0;
                var outubroCls = clsVenAllOutubro.Count() > 0 ? clsVenAllOutubro.Sum(v => v.ValorBruto) : 0;
                var novembroCls = clsVenAllNovembro.Count() > 0 ? clsVenAllNovembro.Sum(v => v.ValorBruto) : 0;
                var dezembroCls = clsVenAllDezembro.Count() > 0 ? clsVenAllDezembro.Sum(v => v.ValorBruto) : 0;




                var janeiroAnteriorCls = clsVenAllJaneiroAnterior.Count() > 0 ? clsVenAllJaneiroAnterior.Sum(v => v.ValorBruto) : 0;
                var fevereiroAnteriorCls = clsVenAllFevereiroAnterior.Count() > 0 ? clsVenAllFevereiroAnterior.Sum(v => v.ValorBruto) : 0;
                var marçoAnteriorCls = clsVenAllMarçoAnterior.Count() > 0 ? clsVenAllMarçoAnterior.Sum(v => v.ValorBruto) : 0;
                var abrilAnteriorCls = clsVenAllAbrilAnterior.Count() > 0 ? clsVenAllAbrilAnterior.Sum(v => v.ValorBruto) : 0;
                var maioAnteriorCls = clsVenAllMaioAnterior.Count() > 0 ? clsVenAllMaioAnterior.Sum(v => v.ValorBruto) : 0;
                var junhoAnteriorCls = clsVenAllJunhoAnterior.Count() > 0 ? clsVenAllJunhoAnterior.Sum(v => v.ValorBruto) : 0;
                var julhoAnteriorCls = clsVenAllJulhoAnterior.Count() > 0 ? clsVenAllJulhoAnterior.Sum(v => v.ValorBruto) : 0;
                var agostoAnteriorCls = clsVenAllAgostoAnterior.Count() > 0 ? clsVenAllAgostoAnterior.Sum(v => v.ValorBruto) : 0;
                var setembroAnteriorCls = clsVenAllSetembroAnterior.Count() > 0 ? clsVenAllSetembroAnterior.Sum(v => v.ValorBruto) : 0;
                var outubroAnteriorCls = clsVenAllOutubroAnterior.Count() > 0 ? clsVenAllOutubroAnterior.Sum(v => v.ValorBruto) : 0;
                var novembroAnteriorCls = clsVenAllNovembroAnterior.Count() > 0 ? clsVenAllNovembroAnterior.Sum(v => v.ValorBruto) : 0;
                var dezembroAnteriorCls = clsVenAllDezembroAnterior.Count() > 0 ? clsVenAllDezembroAnterior.Sum(v => v.ValorBruto) : 0;



                var objeto = new TicketMedio
                {
                    MediaJaneiro = janeiroCls,
                    MediaFevereiro = fevereiroCls,
                    MediaMarço = marçoCls,
                    MediaAbril = abrilCls,
                    MediaMaio = maioCls,
                    MediaJunho = junhoCls,
                    MediaJulho = julhoCls,
                    MediaAgosto = agostoCls,
                    MediaSetembro = setembroCls,
                    MediaOutubro = outubroCls,
                    MediaNovembro = novembroCls,
                    MediaDezembro = dezembroCls,

                    MediaJaneiroAnterior = janeiroAnteriorCls,
                    MediaFevereiroAnterior = fevereiroAnteriorCls,
                    MediaMarçoAnterior = marçoAnteriorCls,
                    MediaAbrilAnterior = abrilAnteriorCls,
                    MediaMaioAnterior = maioAnteriorCls,
                    MediaJunhoAnterior = junhoAnteriorCls,
                    MediaJulhoAnterior = julhoAnteriorCls,
                    MediaAgostoAnterior = agostoAnteriorCls,
                    MediaSetembroAnterior = setembroAnteriorCls,
                    MediaOutubroAnterior = outubroAnteriorCls,
                    MediaNovembroAnterior = novembroAnteriorCls,
                    MediaDezembroAnterior = dezembroAnteriorCls,


                    NomeClassificação1 = nomeClassificacao1,
                    NomeClassificação2 = nomeClassificacao2,
                    NomeClassificação3 = nomeClassificacao3,
                    NomeClassificação4 = nomeClassificacao4,
                    NomeClassificação5 = nomeClassificacao5,
                    NomeClassificação6 = nomeClassificacao6,
                    NomeClassificação7 = nomeClassificacao7,
                    NomeClassificação8 = nomeClassificacao8


                };
                listRetorno.Add(objeto);


                result = Json(listRetorno, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {


                result = Json(new HttpStatusCodeResult(400, "Ocorreu uma falha ao carregar o gráfico."), JsonRequestBehavior.AllowGet);
            }

            return result;
        }

        [HttpPost]
        [ClaimsAuthorize("ChartIndicadorPorClassificacao", "Allowed")]
        public JsonResult RetornarClsVenAllDias(string mes, string ano, int cls, int? loja)
        {
            JsonResult result;
            var listRetorno = new List<SomaClassificacaoMesAno>();
            try
            {
                var mesAno = ano + mes;
                var vendas = new List<VendaClassificacaoLojaSp>();
                //var clsVenAll = new List<ClsVenAllGraficoPorCls>();
                if (loja == null)
                {
                    //Verificando de a conexão da empresa é MySql ou Postgre
                    if (CompanyUser.GetDbConnection().ToString() == "MySql.Data.MySqlClient.MySqlConnection")
                    {
                        //Buscando do repositório do MySql
                        if (LoggedUser.UserStoresIds.Any())
                        {
                            vendas = _vendaClassificacaoLojaRepository.GetByFilter(v => v.Mes == mesAno && 
                                                                                   v.Cls == cls && 
                                                                                   LoggedUser.UserStoresIds.Contains(v.Uad), CompanyUser.GetDbConnection()).ToList();
                        }
                        else
                        {
                            vendas = _vendaClassificacaoLojaRepository.GetByFilter(v => v.Mes == mesAno && v.Cls == cls, CompanyUser.GetDbConnection()).ToList();
                        }                        
                    }
                    else
                    {
                        //Buscando da Stored Procedure do Postgre
                        if (LoggedUser.UserStoresIds.Any())
                        {
                            vendas = _vendasClassificacaoLojaStoredProcedure.BuscarPorMyECls(mesAno, cls, LoggedUser.UserStoresIds, CompanyUser.GetConnectionString()).ToList();
                        }
                        else
                        {
                            vendas = _vendasClassificacaoLojaStoredProcedure.BuscarPorMyECls(mesAno, cls, CompanyUser.GetConnectionString()).ToList();
                        }                        
                    }
                    //vendas = _vendasClassificacaoLojaStoredProcedure.BuscarPorMyECls(mesAno, cls, CompanyUser.GetConnectionString()).ToList();
                    var distinct = vendas.GroupBy(c => c.Uad).Select(group => group.First());
                    var meta = distinct.Any() ? Convert.ToDouble(distinct.Sum(c => c.Meta)) : 0;
                    var percentual = distinct.Any() ? Convert.ToDouble(distinct.Sum(c => c.Percentual)) : 0;
                    foreach (var dataFiltrada in vendas.GroupBy(c => c.Dias).Select(group => group.First()))
                    {
                        var objeto = new SomaClassificacaoMesAno
                        {
                            Classificação1 = Convert.ToDouble(vendas.Where(c => c.Dias == dataFiltrada.Dias).Sum(c => c.ValorBruto)),
                            Dia = dataFiltrada.Dias.Day,
                            Meta = meta,
                            Percentual = percentual
                        };
                        listRetorno.Add(objeto);
                    }
                }
                else
                {
                    if (CompanyUser.GetDbConnection().ToString() == "MySql.Data.MySqlClient.MySqlConnection")
                    {
                        if (LoggedUser.UserStoresIds.Any())
                        {
                            vendas = _vendaClassificacaoLojaRepository.GetByFilter(v => v.Mes == mesAno && 
                                                                                   v.Cls == cls && 
                                                                                   v.Uad == loja.Value && 
                                                                                   LoggedUser.UserStoresIds.Contains(v.Uad), CompanyUser.GetDbConnection()).ToList();
                        }
                        else
                        {
                            vendas = _vendaClassificacaoLojaRepository.GetByFilter(v => v.Mes == mesAno && v.Cls == cls && v.Uad == loja.Value, CompanyUser.GetDbConnection()).ToList();
                        }                        
                    }
                    else
                    {
                        if (LoggedUser.UserStoresIds.Any())
                        {
                            vendas = _vendasClassificacaoLojaStoredProcedure.Buscar(mesAno, loja.Value, cls, LoggedUser.UserStoresIds, CompanyUser.GetConnectionString()).ToList();
                        }
                        else
                        {
                            vendas = _vendasClassificacaoLojaStoredProcedure.Buscar(mesAno, loja.Value, cls, CompanyUser.GetConnectionString()).ToList();
                        }                        
                    }                    
                    var distinct = vendas.GroupBy(c => c.Uad).Select(group => group.First());
                    var meta = distinct.Any() ? Convert.ToDouble(distinct.Sum(c => c.Meta)) : 0;
                    var percentual = distinct.Any() ? Convert.ToDouble(distinct.Sum(c => c.Percentual)) : 0;
                    foreach (var dataFiltrada in vendas.GroupBy(c => c.Dias).Select(group => group.First()))
                    {
                        var objeto = new SomaClassificacaoMesAno
                        {
                            Classificação1 = Convert.ToDouble(vendas.Where(c => c.Dias == dataFiltrada.Dias).Sum(c => c.ValorBruto)),
                            Dia = dataFiltrada.Dias.Day,
                            Meta = meta,
                            Percentual = percentual
                        };
                        listRetorno.Add(objeto);
                    }                    
                }               
                result = Json(listRetorno, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = Json(new HttpStatusCodeResult(400, "Ocorreu uma falha ao carregar o gráfico: " + ex), JsonRequestBehavior.AllowGet);
            }
            return result;
        }

        [HttpGet]
        [ClaimsAuthorize("ChartLojaVendasHora", "Allowed")]
        public ActionResult VendasPorHorario()
        {
            ViewBag.Months = ListGeneratorHelper.GenerateMonths();
            ViewBag.Years = ListGeneratorHelper.GenerateYears();
            ViewBag.Cliente = (LoggedUser != null ? LoggedUser.Name : string.Empty);
            return View();
        }

        [HttpPost]
        [ClaimsAuthorize("ChartLojaVendasHora", "Allowed")]
        public JsonResult VendasPorHorario(string mes, string ano, int top = 10, bool onlyToday = false)
        {
            //Action para buscar as vendas das lojas dividido por hora
            var anoMes = $"{ano}{mes}";
            //Buscando todas as vendas por loja e hora
            var vendasLojasPorHora = new List<VendaLojaPorHoraViewModel>();
            if (onlyToday)
            {
                var today = DateTime.Now.ToString("yyyy-MM-dd");
                if (LoggedUser.UserStoresIds.Any())
                {
                    vendasLojasPorHora = _vendasLojaPorDiaHoraAppService.GetByFilter(v => v.My == anoMes && 
                                                                                     v.Dat.ToString() == today && 
                                                                                     LoggedUser.UserStoresIds.Any(u => u.Equals(v.Loja)), CompanyUser.GetDbConnection()).Cast<VendaLojaPorHoraViewModel>().ToList();
                }
                else
                {
                    vendasLojasPorHora = _vendasLojaPorDiaHoraAppService.GetByFilter(v => v.My == anoMes && 
                                                                                     v.Dat.ToString() == today, CompanyUser.GetDbConnection()).Cast<VendaLojaPorHoraViewModel>().ToList();
                }                
            }
            else
            {
                if (LoggedUser.UserStoresIds.Any())
                {                    
                    vendasLojasPorHora = _vendaLojaPorHoraAppService.GetByFilter(v => v.My == anoMes &&
                                                                                      LoggedUser.UserStoresIds.Any(u => u.Equals(v.Loja)), CompanyUser.GetDbConnection()).ToList();
                }
                else
                {
                    vendasLojasPorHora = _vendaLojaPorHoraAppService.GetByFilter(v => v.My == anoMes, CompanyUser.GetDbConnection()).ToList();
                }                                
            }
            var vendasLojaPorHoraViewModel = new List<VendaLojaPorHoraViewModel>();
            //Interando as vendas dando um distinct por loja
            foreach (var vendasLojaPorHora in vendasLojasPorHora.GroupBy(v => v.Loja).Select(group => group.First()))
            {
                //Buscando o nome da Loja
                var loja = _uadCabAppService.GetById(vendasLojaPorHora.Loja, CompanyUser.GetDbConnection());
                var vendaLojaPorHoraViewModel = new VendaLojaPorHoraViewModel();
                vendaLojaPorHoraViewModel.Loja = vendasLojaPorHora.Loja;
                vendaLojaPorHoraViewModel.Nome = loja.Des;
                for (int i = 0; i <= 23; i++)
                {
                    //Buscando as vendas totais de cada hora da loja
                    var venda = vendasLojasPorHora.FirstOrDefault(v => v.Loja == vendasLojaPorHora.Loja && v.Hora == i);
                    //Caso possuir venda nesse horário buscar o valor vendido
                    if (venda != null)
                    {
                        vendaLojaPorHoraViewModel.HorasVendaViewModels.Add(new Application.Farmacia.ViewModels.Balconista.HoraVendaViewModel
                        {
                            Hora = i + ":00",
                            Valor = venda.Valor.Value - venda.Desconto.Value,
                            TicketMedio = (venda.Valor.Value - venda.Desconto.Value) / venda.ClientesAtendidos,
                            ClientesAtendidos = venda.ClientesAtendidos
                        });
                        vendaLojaPorHoraViewModel.ValorTotal += venda.Valor.Value;
                    }
                    else
                    {
                        //Senão deve colocar o valor zerado no horário
                        vendaLojaPorHoraViewModel.HorasVendaViewModels.Add(new Application.Farmacia.ViewModels.Balconista.HoraVendaViewModel
                        {
                            Hora = i + ":00",
                            Valor = 0,
                            TicketMedio = 0,
                            ClientesAtendidos = 0
                        });
                    }
                }
                vendasLojaPorHoraViewModel.Add(vendaLojaPorHoraViewModel);
            }
            for (int i = 0; i < 24; i++)
            {
                var hora = $"{i}:00";
                //Verificando se há pelo menos uma loja com venda nesse horário
                var hasValue = vendasLojaPorHoraViewModel.Any(v => v.HorasVendaViewModels.Any(h => h.Hora == hora && h.Valor != 0));
                if (!hasValue)
                {
                    //Se nenhuma loja possui venda nesse horário, deve-se remover esse horário da timeline de venda
                    foreach (var vendas in vendasLojaPorHoraViewModel)
                    {
                        vendas.HorasVendaViewModels.Remove(vendas.HorasVendaViewModels.FirstOrDefault(h => h.Hora == hora));
                    }
                }
            }
            vendasLojaPorHoraViewModel = vendasLojaPorHoraViewModel.OrderByDescending(v => v.ValorTotal).Take(top).ToList();
            return Json(vendasLojaPorHoraViewModel, JsonRequestBehavior.AllowGet);
        }

    }
}