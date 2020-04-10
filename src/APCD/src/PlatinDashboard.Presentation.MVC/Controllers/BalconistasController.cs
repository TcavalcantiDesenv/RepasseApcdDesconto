using PlatinDashboard.Application.Farmacia.Interfaces;
using PlatinDashboard.Application.Farmacia.ViewModels.Balconista;
using PlatinDashboard.Application.Farmacia.ViewModels.Legado;
using PlatinDashboard.Application.Interfaces;
using PlatinDashboard.Domain.Farmacia.Interfaces.Repositories;
using PlatinDashboard.Presentation.MVC.Helpers;
using PlatinDashboard.Presentation.MVC.MvcFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PlatinDashboard.Presentation.MVC.Controllers
{
    public class BalconistasController : BaseController
    {
        private readonly IClsCabAppService _clsCabAppService;
        private readonly IUadCabAppService _uadCabAppService;
        private readonly IFunCabAppService _funCabAppService;
        private readonly IViewBalconistaAppService _viewBalconistaAppService;
        private readonly IVendaBalconistaPorHoraAppService _vendaBalconistaPorHoraAppService;
        private readonly IVendaLojaPorHoraAppService _vendaLojaPorHoraAppService;
        private readonly IVendaBalconistaPorClassificacaoRepository _vendaBalconistaPorClassificacaoRepository;
        private readonly IBalVenAllGraficoTotalRepository _balVenAllGraficoTotalRepository;
        private readonly IBalVenAllGraficoPorClsRepository _balVenAllGraficoPorClsRepository;

        public BalconistasController(IUserAppService userAppService,
                                ICompanyAppService companyAppService,
                                IClsCabAppService clsCabAppService,
                                IUadCabAppService uadCabAppService,
                                IFunCabAppService funCabAppService,
                                IViewBalconistaAppService viewBalconistaAppService,
                                IVendaBalconistaPorHoraAppService vendaBalconistaPorHoraAppService,
                                IVendaLojaPorHoraAppService vendaLojaPorHoraAppService,
                                IVendaBalconistaPorClassificacaoRepository vendaBalconistaPorClassificacaoRepository,
                                IBalVenAllGraficoTotalRepository balVenAllGraficoTotalRepository,
                                IBalVenAllGraficoPorClsRepository balVenAllGraficoPorClsRepository)
                                : base(userAppService, companyAppService)
        {
            _clsCabAppService = clsCabAppService;
            _uadCabAppService = uadCabAppService;
            _funCabAppService = funCabAppService;
            _viewBalconistaAppService = viewBalconistaAppService;
            _vendaBalconistaPorHoraAppService = vendaBalconistaPorHoraAppService;
            _vendaLojaPorHoraAppService = vendaLojaPorHoraAppService;
            _vendaBalconistaPorClassificacaoRepository = vendaBalconistaPorClassificacaoRepository;
            _balVenAllGraficoTotalRepository = balVenAllGraficoTotalRepository;
            _balVenAllGraficoPorClsRepository = balVenAllGraficoPorClsRepository;
        }

        [HttpGet]
        [ClaimsAuthorize("ChartBalconistaVendasHora", "Allowed")]
        public ActionResult VendasPorHorario()
        {
            ViewBag.Months = ListGeneratorHelper.GenerateMonths();
            ViewBag.Years = ListGeneratorHelper.GenerateYears();
            ViewBag.Stores = new SelectList(_uadCabAppService.GetStores(CompanyUser.GetDbConnection()), "Uad", "Des");
            ViewBag.Cliente = (Session["userId"] == null ? "" : LoggedUser.Name);
            return View();
        }

        [HttpPost]
        [ClaimsAuthorize("ChartBalconistaVendasHora", "Allowed")]
        public ActionResult VendasPorHorario(int lojaId, string mes, string ano, int top = 10)
        {
            //Action para buscar as vendas dos balconistas dividido por hora
            var anoMes = $"{ano}{mes}";
            //Buscando todas as vendas da loja por balconistas e hora
            var vendasBalconistasPorHora = _vendaBalconistaPorHoraAppService.GetByFilter(v => v.Loja == lojaId && v.My == anoMes, CompanyUser.GetDbConnection());
            var vendasBalconistaPorHoraViewModel = new List<VendaBalconistaPorHoraViewModel>();
            //Interando as vendas dando um distinct por balconista
            foreach (var vendasBalconistaPorHora in vendasBalconistasPorHora.GroupBy(v => v.Balconista).Select(group => group.First()).ToList())
            {
                //Buscando o nome do balconista
                var balconista = _funCabAppService.GetByFilter(v => v.Cod == vendasBalconistaPorHora.Balconista.ToString(), CompanyUser.GetDbConnection()).FirstOrDefault();
                var vendaBalconistaPorHoraViewModel = new VendaBalconistaPorHoraViewModel();
                vendaBalconistaPorHoraViewModel.Loja = vendasBalconistaPorHora.Loja;
                vendaBalconistaPorHoraViewModel.Balconista = vendasBalconistaPorHora.Balconista;
                vendaBalconistaPorHoraViewModel.Nome = balconista?.Nom != null ? balconista.Nom : "Sem Identificação";
                for (int i = 8; i <= 22; i++)
                {
                    //Buscando as vendas totais de cada hora da balconista
                    var venda = vendasBalconistasPorHora.FirstOrDefault(v => v.Balconista == vendasBalconistaPorHora.Balconista && v.Hora == i);
                    //Caso possuir venda nesse horário buscar o valor vendido
                    if (venda != null)
                    {
                        vendaBalconistaPorHoraViewModel.HorasVendaViewModels.Add(new HoraVendaViewModel
                        {
                            Hora = i + ":00",
                            Valor = venda.Valor.Value,
                            TicketMedio = (venda.Valor.Value - venda.Desconto.Value) / venda.ClientesAtendidos,
                            ClientesAtendidos = venda.ClientesAtendidos
                        });
                        vendaBalconistaPorHoraViewModel.ValorTotal += venda.Valor.Value;
                    }
                    else
                    {
                        //Senão deve colocar o valor zerado no horário
                        vendaBalconistaPorHoraViewModel.HorasVendaViewModels.Add(new HoraVendaViewModel
                        {
                            Hora = i + ":00",
                            Valor = 0,
                            TicketMedio = 0,
                            ClientesAtendidos = 0
                        });
                    }
                }
                vendasBalconistaPorHoraViewModel.Add(vendaBalconistaPorHoraViewModel);
            }
            vendasBalconistaPorHoraViewModel = vendasBalconistaPorHoraViewModel.OrderByDescending(v => v.ValorTotal).Take(top).ToList();
            return Json(vendasBalconistaPorHoraViewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [ClaimsAuthorize("ChartBalconistaVendasAnual", "Allowed")]
        public ActionResult VendasAnual()
        {
            ViewBag.Years = ListGeneratorHelper.GenerateYears();
            ViewBag.Stores = new SelectList(_uadCabAppService.GetStores(CompanyUser.GetDbConnection()), "Uad", "Des");
            return View();
        }

        [HttpPost]
        [ClaimsAuthorize("ChartBalconistaVendasAnual", "Allowed")]
        public ActionResult VendasAnual(string ano, int balconistaId)
        {
            var balconistaViewModel = new BalconistaViewModel();
            //Buscando os nomes das categorias
            var categorias = new List<string>();
            for (var i = 1; i < 9; i++)
            {
                var categoria = _clsCabAppService.GetByFilter(c => c.Cod == i.ToString(), CompanyUser.GetDbConnection()).FirstOrDefault();
                if (categoria != null) categorias.Add(categoria.Des);
            }
            //Buscando todas as vendas do balconista no ano
            var viewBalconistas = _viewBalconistaAppService.GetByFilter(v => v.MesAno.Remove(4, 2) == ano && v.IdBalconista == balconistaId, CompanyUser.GetDbConnection());
            for (int i = 1; i <= 12; i++)
            {
                for (int j = 1; j <= 8; j++)
                {
                    //Segmentando as vendas por mês e classificação
                    var month = i.ToString().Length < 2 ? $"0{i}" : i.ToString();
                    var viewBalconista = viewBalconistas.Where(v => v.MesAno.Remove(0, 4) == month && v.Classificacao == j)?.FirstOrDefault();
                    balconistaViewModel.BalconistaVendaViewModels.Add(new BalconistaVendaViewModel
                    {
                        BalconistaId = balconistaId,
                        CategoriaId = j,
                        Mes = i,
                        ValorTotal = viewBalconista?.Valor ?? 0
                    });
                }
            }
            return Json(new { categorias, balconistaViewModel }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [ClaimsAuthorize("ChartBalconistaVendasMensal", "Allowed")]
        public ActionResult VendasMensal()
        {
            ViewBag.Years = ListGeneratorHelper.GenerateYears();
            ViewBag.Stores = new SelectList(_uadCabAppService.GetStores(CompanyUser.GetDbConnection()), "Uad", "Des");
            return View();
        }

        [HttpPost]
        [ClaimsAuthorize("ChartBalconistaVendasMensal", "Allowed")]
        public ActionResult VendasMensal(int lojaId, string mes, int? top)
        {
            //Action para buscar os dados de venda dos balconistas separando por loja e categoria de produto e mês
            //Utilizado para popular os gráficos da página Vendas dos Balconistas Mensal
            //Buscando os nomes das 8 Categorias
            var categorias = new List<string>();
            for (var i = 1; i < 9; i++)
            {
                var categoria = _clsCabAppService.GetByFilter(c => c.Cod == i.ToString(), CompanyUser.GetDbConnection()).FirstOrDefault();
                if (categoria != null) categorias.Add(categoria.Des);
            }
            //Buscando os balconistas que possuem vendas no mes e loja selecionada
            var vendas = _viewBalconistaAppService.BuscarPorLojaEPeriodo(lojaId, mes, CompanyUser.GetDbConnection());
            //Fazendo um distinct nas vendas para saber quais balconistas possuem vendas
            var vendasPorBalconista = vendas.GroupBy(v => v.IdBalconista).Select(g => g.First()).ToList();
            //Criando as lista com valores de venda
            var balconistas = new List<BalconistaViewModel>();
            var vendasBalconista = new List<List<BalconistaVendaViewModel>>();
            for (int i = 1; i < 9; i++)
            {
                vendasBalconista.Add(new List<BalconistaVendaViewModel>());
            }
            foreach (var viewBalconista in vendasPorBalconista)
            {
                //Buscando todas as vendas desse balconista no mês
                var todasAsVendaDoBalconista = _viewBalconistaAppService
                    .GetByFilter(v => v.IdBalconista == viewBalconista.IdBalconista &&
                                      v.IdLoja == lojaId && v.MesAno == mes, CompanyUser.GetDbConnection()).ToList();
                //Buscando o valor total vendido pelo balconista
                var funcionarios = _funCabAppService
                    .GetByFilter(f => f.Cod == viewBalconista.IdBalconista.ToString(), CompanyUser.GetDbConnection());
                var balconista = new BalconistaViewModel
                {
                    BalconistaId = viewBalconista.IdBalconista,
                    Nome = funcionarios.Any() ? funcionarios.First().Nom : "Sem Identificação",
                    ValorTotal = todasAsVendaDoBalconista.Sum(v => v.Valor)
                };
                //Segmentando as vendas por categoria
                for (int i = 1; i < 9; i++)
                {
                    balconista.BalconistaVendaViewModels.Add(
                        new BalconistaVendaViewModel
                        {
                            BalconistaId = viewBalconista.IdBalconista,
                            CategoriaId = i,
                            ValorTotal = todasAsVendaDoBalconista.Where(v => v.Classificacao == i).Sum(v => v.Valor)
                        }
                    );
                }
                balconistas.Add(balconista);
            }
            //ordenando por mais vendas e pegando o top 20
            balconistas = balconistas.OrderByDescending(b => b.ValorTotal).Take(top ?? 20).ToList();
            foreach (var balconista in balconistas)
            {
                for (int i = 0; i < 8; i++)
                {
                    vendasBalconista.ElementAt(i).Add(balconista.BalconistaVendaViewModels.ElementAt(i));
                }
            }
            var result = Json(new
            {
                categorias,
                vendasBalconista,
                balconistas
            }, JsonRequestBehavior.AllowGet);
            return result;
        }

        [HttpPost]
        public JsonResult BalconistaPorLoja(short uad)
        {
            var listBalconista = new List<FunCabViewModel>();
            listBalconista = uad != -1 ? _funCabAppService.GetByFilter(f => f.Uad == uad, CompanyUser.GetDbConnection()).OrderByDescending(a => a.Nom).ToList()
                : _funCabAppService.GetAll(CompanyUser.GetDbConnection()).ToList();
            return Json(new { listBalconista }, JsonRequestBehavior.AllowGet);
        }

        [ClaimsAuthorize("ChartIndicadorGeralBalconista", "Allowed")]
        public ActionResult IndicadorGeral()
        {
            ViewBag.Years = ListGeneratorHelper.GenerateYears();
            ViewBag.Months = ListGeneratorHelper.GenerateMonths();
            ViewBag.Stores = new SelectList(_uadCabAppService.GetStores(CompanyUser.GetDbConnection()), "Uad", "Des");
            ViewBag.Cliente = (LoggedUser != null ? LoggedUser.Name : string.Empty);
            return View();
        }

        [HttpPost]
        [ClaimsAuthorize("ChartIndicadorGeralBalconista", "Allowed")]
        public JsonResult RetornarLucroLiquidoMeses(string ano, int balconistaId)
        {
            JsonResult result;
            var listRetorno = new List<TicketMedio>();
            try
            {
                var anoAnterior = Convert.ToInt32(ano) - 1;
                var anoAnteriorString = anoAnterior.ToString();
                var vendasBalconistas = _vendaBalconistaPorClassificacaoRepository.GetByFilter(c => c.Bal == balconistaId && (c.My.StartsWith(ano) || c.My.StartsWith(anoAnteriorString)), CompanyUser.GetDbConnection());
                var venAllJaneiro = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("01"));
                var venAllFevereiro = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("02"));
                var venAllMarço = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("03"));
                var venAllAbril = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("04"));
                var venAllMaio = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("05"));
                var venAllJunho = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("06"));
                var venAllJulho = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("07"));
                var venAllAgosto = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("08"));
                var venAllSetembro = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("09"));
                var venAllOutubro = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("10"));
                var venAllNovembro = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("11"));
                var venAllDezembro = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("12"));


                var venAllJaneiroAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("01"));
                var venAllFevereiroAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("02"));
                var venAllMarçoAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("03"));
                var venAllAbrilAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("04"));
                var venAllMaioAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("05"));
                var venAllJunhoAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("06"));
                var venAllJulhoAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("07"));
                var venAllAgostoAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("08"));
                var venAllSetembroAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("09"));
                var venAllOutubroAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("10"));
                var venAllNovembroAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("11"));
                var venAllDezembroAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("12"));



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
        [ClaimsAuthorize("ChartIndicadorGeralBalconista", "Allowed")]
        public JsonResult RetornarTicketMedioMeses(string ano, int balconistaId)
        {
            JsonResult result;
            var listRetorno = new List<TicketMedio>();
            try
            {
                var anoAnterior = Convert.ToInt32(ano) - 1;
                var anoAnteriorString = anoAnterior.ToString();
                var vendasBalconistas = _vendaBalconistaPorClassificacaoRepository.GetByFilter(c => c.Bal == balconistaId && (c.My.StartsWith(ano) || c.My.StartsWith(anoAnteriorString)), CompanyUser.GetDbConnection());
                var venAllJaneiro = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("01"));
                var venAllFevereiro = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("02"));
                var venAllMarço = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("03"));
                var venAllAbril = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("04"));
                var venAllMaio = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("05"));
                var venAllJunho = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("06"));
                var venAllJulho = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("07"));
                var venAllAgosto = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("08"));
                var venAllSetembro = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("09"));
                var venAllOutubro = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("10"));
                var venAllNovembro = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("11"));
                var venAllDezembro = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("12"));


                var venAllJaneiroAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("01"));
                var venAllFevereiroAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("02"));
                var venAllMarçoAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("03"));
                var venAllAbrilAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("04"));
                var venAllMaioAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("05"));
                var venAllJunhoAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("06"));
                var venAllJulhoAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("07"));
                var venAllAgostoAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("08"));
                var venAllSetembroAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("09"));
                var venAllOutubroAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("10"));
                var venAllNovembroAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("11"));
                var venAllDezembroAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("12"));


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
        [ClaimsAuthorize("ChartIndicadorGeralBalconista", "Allowed")]
        public JsonResult RetornarTicketMedioClientesMeses(string ano, int balconistaId)
        {
            JsonResult result;
            var listRetorno = new List<TicketMedio>();
            try
            {          
                var anoAnterior = Convert.ToInt32(ano) - 1;
                var anoAnteriorString = anoAnterior.ToString();
                var vendasBalconistas = _vendaBalconistaPorClassificacaoRepository.GetByFilter(c => c.Bal == balconistaId && (c.My.StartsWith(ano) || c.My.StartsWith(anoAnteriorString)), CompanyUser.GetDbConnection());
                var venAllJaneiro = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("01"));
                var venAllFevereiro = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("02"));
                var venAllMarço = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("03"));
                var venAllAbril = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("04"));
                var venAllMaio = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("05"));
                var venAllJunho = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("06"));
                var venAllJulho = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("07"));
                var venAllAgosto = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("08"));
                var venAllSetembro = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("09"));
                var venAllOutubro = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("10"));
                var venAllNovembro = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("11"));
                var venAllDezembro = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("12"));


                var venAllJaneiroAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("01"));
                var venAllFevereiroAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("02"));
                var venAllMarçoAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("03"));
                var venAllAbrilAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("04"));
                var venAllMaioAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("05"));
                var venAllJunhoAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("06"));
                var venAllJulhoAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("07"));
                var venAllAgostoAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("08"));
                var venAllSetembroAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("09"));
                var venAllOutubroAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("10"));
                var venAllNovembroAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("11"));
                var venAllDezembroAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("12"));



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
        [ClaimsAuthorize("ChartIndicadorGeralBalconista", "Allowed")]
        public JsonResult RetornarTicketMedioItensMeses(string ano, int balconistaId)
        {
            JsonResult result;
            var listRetorno = new List<TicketMedio>();
            try
            {
                var anoAnterior = Convert.ToInt32(ano) - 1;
                var anoAnteriorString = anoAnterior.ToString();
                var vendasBalconistas = _vendaBalconistaPorClassificacaoRepository.GetByFilter(c => c.Bal == balconistaId && (c.My.StartsWith(ano) || c.My.StartsWith(anoAnteriorString)), CompanyUser.GetDbConnection());
                var venAllJaneiro = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("01"));
                var venAllFevereiro = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("02"));
                var venAllMarço = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("03"));
                var venAllAbril = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("04"));
                var venAllMaio = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("05"));
                var venAllJunho = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("06"));
                var venAllJulho = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("07"));
                var venAllAgosto = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("08"));
                var venAllSetembro = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("09"));
                var venAllOutubro = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("10"));
                var venAllNovembro = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("11"));
                var venAllDezembro = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("12"));


                var venAllJaneiroAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("01"));
                var venAllFevereiroAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("02"));
                var venAllMarçoAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("03"));
                var venAllAbrilAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("04"));
                var venAllMaioAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("05"));
                var venAllJunhoAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("06"));
                var venAllJulhoAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("07"));
                var venAllAgostoAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("08"));
                var venAllSetembroAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("09"));
                var venAllOutubroAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("10"));
                var venAllNovembroAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("11"));
                var venAllDezembroAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("12"));

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
        [ClaimsAuthorize("ChartIndicadorGeralBalconista", "Allowed")]
        public JsonResult RetornarItensPorCliente(string ano, int balconistaId)
        {
            JsonResult result;
            var listRetorno = new List<TicketMedio>();
            try
            {
                var anoAnterior = Convert.ToInt32(ano) - 1;
                var anoAnteriorString = anoAnterior.ToString();
                var vendasBalconistas = _vendaBalconistaPorClassificacaoRepository.GetByFilter(c => c.Bal == balconistaId && (c.My.StartsWith(ano) || c.My.StartsWith(anoAnteriorString)), CompanyUser.GetDbConnection());
                var venAllJaneiro = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("01"));
                var venAllFevereiro = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("02"));
                var venAllMarço = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("03"));
                var venAllAbril = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("04"));
                var venAllMaio = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("05"));
                var venAllJunho = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("06"));
                var venAllJulho = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("07"));
                var venAllAgosto = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("08"));
                var venAllSetembro = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("09"));
                var venAllOutubro = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("10"));
                var venAllNovembro = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("11"));
                var venAllDezembro = vendasBalconistas.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("12"));


                var venAllJaneiroAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("01"));
                var venAllFevereiroAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("02"));
                var venAllMarçoAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("03"));
                var venAllAbrilAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("04"));
                var venAllMaioAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("05"));
                var venAllJunhoAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("06"));
                var venAllJulhoAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("07"));
                var venAllAgostoAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("08"));
                var venAllSetembroAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("09"));
                var venAllOutubroAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("10"));
                var venAllNovembroAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("11"));
                var venAllDezembroAnterior = vendasBalconistas.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("12"));



                var janeiroItensCliente = venAllJaneiro.Count() > 0 ? venAllJaneiro.Sum(v => v.Qtp) / venAllJaneiro.Sum(v => v.Reg) : 0;
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
        [ClaimsAuthorize("ChartIndicadorGeralBalconista", "Allowed")]
        public ActionResult VendasPorHorarioIndicador(int balconistaId, string ano)
        {
            //Action para buscar as vendas dos balconistas dividido por hora
            //Buscando todas as vendas da loja por balconistas e hora
            var vendasBalconistasPorHora = _vendaBalconistaPorHoraAppService.GetByFilter(v => v.Balconista == balconistaId && v.My.StartsWith(ano), CompanyUser.GetDbConnection());
            var vendasBalconistaPorHoraViewModel = new List<VendaBalconistaPorHoraViewModel>();
            //Interando as vendas dando um distinct por balconista
            foreach (var vendasBalconistaPorHora in vendasBalconistasPorHora.GroupBy(v => v.Balconista).Select(group => group.First()).ToList())
            {
                //Buscando o nome do balconista
                var balconista = _funCabAppService.GetByFilter(v => v.Cod == vendasBalconistaPorHora.Balconista.ToString(), CompanyUser.GetDbConnection()).FirstOrDefault();
                var vendaBalconistaPorHoraViewModel = new VendaBalconistaPorHoraViewModel();
                vendaBalconistaPorHoraViewModel.Loja = vendasBalconistaPorHora.Loja;
                vendaBalconistaPorHoraViewModel.Balconista = vendasBalconistaPorHora.Balconista;
                vendaBalconistaPorHoraViewModel.Nome = balconista?.Nom != null ? balconista.Nom : "Sem Identificação";
                for (int i = 8; i <= 22; i++)
                {
                    //Buscando as vendas totais de cada hora da balconista
                    var vendas = vendasBalconistasPorHora.Where(v => v.Balconista == vendasBalconistaPorHora.Balconista && v.Hora == i);
                    //Caso possuir venda nesse horário buscar o valor vendido
                    if (vendas.Any())
                    {
                        vendaBalconistaPorHoraViewModel.HorasVendaViewModels.Add(new HoraVendaViewModel
                        {
                            Hora = i + ":00",
                            Valor = Convert.ToDecimal(vendas.Sum(v => v.Valor)),
                            TicketMedio = Convert.ToDecimal(vendas.Sum(v => v.Valor)) / vendas.Sum(v => v.ClientesAtendidos),
                            ClientesAtendidos = vendas.Sum(v => v.ClientesAtendidos)
                        });
                        vendaBalconistaPorHoraViewModel.ValorTotal += vendas.Sum(v => v.ClientesAtendidos);
                    }
                    else
                    {
                        //Senão deve colocar o valor zerado no horário
                        vendaBalconistaPorHoraViewModel.HorasVendaViewModels.Add(new HoraVendaViewModel
                        {
                            Hora = i + ":00",
                            Valor = 0,
                            TicketMedio = 0,
                            ClientesAtendidos = 0
                        });
                    }
                }
                vendasBalconistaPorHoraViewModel.Add(vendaBalconistaPorHoraViewModel);
            }
            return Json(vendasBalconistaPorHoraViewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [ClaimsAuthorize("ChartIndicadorPorClassificacaoBalconista", "Allowed")]
        public ActionResult IndicadorPorClassificacao()
        {
            ViewBag.Years = ListGeneratorHelper.GenerateYears();
            ViewBag.Months = ListGeneratorHelper.GenerateMonths();
            ViewBag.Cliente = (LoggedUser != null ? LoggedUser.Name : string.Empty);
            ViewBag.Stores = new SelectList(_uadCabAppService.GetStores(CompanyUser.GetDbConnection()), "Uad", "Des");
            return View();
        }

        [HttpPost]
        [ClaimsAuthorize("ChartIndicadorPorClassificacaoBalconista", "Allowed")]
        public JsonResult RetornarClsVenAllDiasTotal(string mes, string ano, int balconistaId)
        {
            JsonResult result;
            var listRetorno = new List<SomaClassificacaoMesAno>();
            try
            {
                var mesAno = ano + mes;
                var balVenAllGraficoTotals = _balVenAllGraficoTotalRepository.GetByFilter(c => c.Mes == mesAno && c.Bal == balconistaId, CompanyUser.GetDbConnection()).ToList();
                foreach (var dataFiltrada in balVenAllGraficoTotals.GroupBy(c => c.Dat).Select(group => group.First()))
                {
                    var objeto = new SomaClassificacaoMesAno
                    {
                        Classificação1 = Convert.ToDouble(balVenAllGraficoTotals.Where(c => c.Dat == dataFiltrada.Dat).Sum(c => c.ValorBruto)),
                        Dia = dataFiltrada.Dat.Day,
                        Meta = Convert.ToDouble(balVenAllGraficoTotals.Where(c => c.Dat == dataFiltrada.Dat).Sum(c => c.Meta)),
                        Percentual = Convert.ToDouble(balVenAllGraficoTotals.Where(c => c.Dat == dataFiltrada.Dat).Sum(c => c.Percentual))
                    };
                    listRetorno.Add(objeto);
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
        [ClaimsAuthorize("ChartIndicadorPorClassificacaoBalconista", "Allowed")]
        public JsonResult RetornarClsVenAllDias(string mes, string ano, int cls, int balconistaId)
        {
            JsonResult result;
            var listRetorno = new List<SomaClassificacaoMesAno>();
            try
            {
                var mesAno = ano + mes;
                var balVenAllGraficoPorClss = _balVenAllGraficoPorClsRepository.GetByFilter(c => c.Cls == cls && c.Mes == mesAno && c.Bal == balconistaId, CompanyUser.GetDbConnection());

                foreach (var balVenAllGraficoPorCls in balVenAllGraficoPorClss)
                {
                    var objeto = new SomaClassificacaoMesAno
                    {
                        Classificação1 = Convert.ToDouble(balVenAllGraficoPorCls.ValorBruto),
                        Dia = balVenAllGraficoPorCls.Dias.Day,
                        Meta = Convert.ToDouble(balVenAllGraficoPorCls.Meta),
                        Percentual = Convert.ToDouble(balVenAllGraficoPorCls.Percentual)

                    };
                    listRetorno.Add(objeto);
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
        [ClaimsAuthorize("ChartIndicadorPorClassificacaoBalconista", "Allowed")]
        public JsonResult RetornarClsVenAllMesesTotal(string ano, int balconistaId)
        {
            JsonResult result;
            var listRetorno = new List<TicketMedio>();
            try
            {             
                var anoAnterior = Convert.ToInt32(ano) - 1;
                var anoAnteriorString = anoAnterior.ToString();
                var balVenAllGraficoPorCls = _vendaBalconistaPorClassificacaoRepository.GetByFilter(c => c.Bal == balconistaId && (c.My.StartsWith(ano) || c.My.StartsWith(anoAnteriorString)), CompanyUser.GetDbConnection());

                var nomeClassificacao1 = _clsCabAppService.GetByFilter(c => c.Cod == "1", CompanyUser.GetDbConnection()).FirstOrDefault().Des;
                var nomeClassificacao2 = _clsCabAppService.GetByFilter(c => c.Cod == "2", CompanyUser.GetDbConnection()).FirstOrDefault().Des;
                var nomeClassificacao3 = _clsCabAppService.GetByFilter(c => c.Cod == "3", CompanyUser.GetDbConnection()).FirstOrDefault().Des;
                var nomeClassificacao4 = _clsCabAppService.GetByFilter(c => c.Cod == "4", CompanyUser.GetDbConnection()).FirstOrDefault().Des;
                var nomeClassificacao5 = _clsCabAppService.GetByFilter(c => c.Cod == "5", CompanyUser.GetDbConnection()).FirstOrDefault().Des;
                var nomeClassificacao6 = _clsCabAppService.GetByFilter(c => c.Cod == "6", CompanyUser.GetDbConnection()).FirstOrDefault().Des;
                var nomeClassificacao7 = _clsCabAppService.GetByFilter(c => c.Cod == "7", CompanyUser.GetDbConnection()).FirstOrDefault().Des;
                var nomeClassificacao8 = _clsCabAppService.GetByFilter(c => c.Cod == "8", CompanyUser.GetDbConnection()).FirstOrDefault().Des;


                var clsVenAllJaneiro = balVenAllGraficoPorCls.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("01"));
                var clsVenAllFevereiro = balVenAllGraficoPorCls.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("02"));
                var clsVenAllMarço = balVenAllGraficoPorCls.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("03"));
                var clsVenAllAbril = balVenAllGraficoPorCls.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("04"));
                var clsVenAllMaio = balVenAllGraficoPorCls.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("05"));
                var clsVenAllJunho = balVenAllGraficoPorCls.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("06"));
                var clsVenAllJulho = balVenAllGraficoPorCls.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("07"));
                var clsVenAllAgosto = balVenAllGraficoPorCls.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("08"));
                var clsVenAllSetembro = balVenAllGraficoPorCls.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("09"));
                var clsVenAllOutubro = balVenAllGraficoPorCls.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("10"));
                var clsVenAllNovembro = balVenAllGraficoPorCls.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("11"));
                var clsVenAllDezembro = balVenAllGraficoPorCls.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("12"));


                var clsVenAllJaneiroAnterior = balVenAllGraficoPorCls.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("01"));
                var clsVenAllFevereiroAnterior = balVenAllGraficoPorCls.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("02"));
                var clsVenAllMarçoAnterior = balVenAllGraficoPorCls.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("03"));
                var clsVenAllAbrilAnterior = balVenAllGraficoPorCls.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("04"));
                var clsVenAllMaioAnterior = balVenAllGraficoPorCls.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("05"));
                var clsVenAllJunhoAnterior = balVenAllGraficoPorCls.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("06"));
                var clsVenAllJulhoAnterior = balVenAllGraficoPorCls.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("07"));
                var clsVenAllAgostoAnterior = balVenAllGraficoPorCls.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("08"));
                var clsVenAllSetembroAnterior = balVenAllGraficoPorCls.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("09"));
                var clsVenAllOutubroAnterior = balVenAllGraficoPorCls.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("10"));
                var clsVenAllNovembroAnterior = balVenAllGraficoPorCls.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("11"));
                var clsVenAllDezembroAnterior = balVenAllGraficoPorCls.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("12"));



                var janeiroCls = clsVenAllJaneiro.Count() > 0 ? clsVenAllJaneiro.Sum(v => v.Vlb) : 0;
                var fevereiroCls = clsVenAllFevereiro.Count() > 0 ? clsVenAllFevereiro.Sum(v => v.Vlb) : 0;
                var marçoCls = clsVenAllMarço.Count() > 0 ? clsVenAllMarço.Sum(v => v.Vlb) : 0;
                var abrilCls = clsVenAllAbril.Count() > 0 ? clsVenAllAbril.Sum(v => v.Vlb) : 0;
                var maioCls = clsVenAllMaio.Count() > 0 ? clsVenAllMaio.Sum(v => v.Vlb) : 0;
                var junhoCls = clsVenAllJunho.Count() > 0 ? clsVenAllJunho.Sum(v => v.Vlb) : 0;
                var julhoCls = clsVenAllJulho.Count() > 0 ? clsVenAllJulho.Sum(v => v.Vlb) : 0;
                var agostoCls = clsVenAllAgosto.Count() > 0 ? clsVenAllAgosto.Sum(v => v.Vlb) : 0;
                var setembroCls = clsVenAllSetembro.Count() > 0 ? clsVenAllSetembro.Sum(v => v.Vlb) : 0;
                var outubroCls = clsVenAllOutubro.Count() > 0 ? clsVenAllOutubro.Sum(v => v.Vlb) : 0;
                var novembroCls = clsVenAllNovembro.Count() > 0 ? clsVenAllNovembro.Sum(v => v.Vlb) : 0;
                var dezembroCls = clsVenAllDezembro.Count() > 0 ? clsVenAllDezembro.Sum(v => v.Vlb) : 0;




                var janeiroAnteriorCls = clsVenAllJaneiroAnterior.Count() > 0 ? clsVenAllJaneiroAnterior.Sum(v => v.Vlb) : 0;
                var fevereiroAnteriorCls = clsVenAllFevereiroAnterior.Count() > 0 ? clsVenAllFevereiroAnterior.Sum(v => v.Vlb) : 0;
                var marçoAnteriorCls = clsVenAllMarçoAnterior.Count() > 0 ? clsVenAllMarçoAnterior.Sum(v => v.Vlb) : 0;
                var abrilAnteriorCls = clsVenAllAbrilAnterior.Count() > 0 ? clsVenAllAbrilAnterior.Sum(v => v.Vlb) : 0;
                var maioAnteriorCls = clsVenAllMaioAnterior.Count() > 0 ? clsVenAllMaioAnterior.Sum(v => v.Vlb) : 0;
                var junhoAnteriorCls = clsVenAllJunhoAnterior.Count() > 0 ? clsVenAllJunhoAnterior.Sum(v => v.Vlb) : 0;
                var julhoAnteriorCls = clsVenAllJulhoAnterior.Count() > 0 ? clsVenAllJulhoAnterior.Sum(v => v.Vlb) : 0;
                var agostoAnteriorCls = clsVenAllAgostoAnterior.Count() > 0 ? clsVenAllAgostoAnterior.Sum(v => v.Vlb) : 0;
                var setembroAnteriorCls = clsVenAllSetembroAnterior.Count() > 0 ? clsVenAllSetembroAnterior.Sum(v => v.Vlb) : 0;
                var outubroAnteriorCls = clsVenAllOutubroAnterior.Count() > 0 ? clsVenAllOutubroAnterior.Sum(v => v.Vlb) : 0;
                var novembroAnteriorCls = clsVenAllNovembroAnterior.Count() > 0 ? clsVenAllNovembroAnterior.Sum(v => v.Vlb) : 0;
                var dezembroAnteriorCls = clsVenAllDezembroAnterior.Count() > 0 ? clsVenAllDezembroAnterior.Sum(v => v.Vlb) : 0;



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
        [ClaimsAuthorize("ChartIndicadorPorClassificacaoBalconista", "Allowed")]
        public JsonResult RetornarClsVenAllMeses(string ano, int cls, int balconistaId)
        {
            JsonResult result;
            var listRetorno = new List<TicketMedio>();
            try
            {
                var anoAnterior = Convert.ToInt32(ano) - 1;
                var anoAnteriorString = anoAnterior.ToString();
                var balVenAllGraficoPorCls = _vendaBalconistaPorClassificacaoRepository.GetByFilter(c => c.Bal == balconistaId && c.Cls == cls && (c.My.StartsWith(ano) || c.My.StartsWith(anoAnteriorString)), CompanyUser.GetDbConnection());
                var nomeClassificacao1 = _clsCabAppService.GetByFilter(c => c.Cod == "1", CompanyUser.GetDbConnection()).FirstOrDefault().Des;
                var nomeClassificacao2 = _clsCabAppService.GetByFilter(c => c.Cod == "2", CompanyUser.GetDbConnection()).FirstOrDefault().Des;
                var nomeClassificacao3 = _clsCabAppService.GetByFilter(c => c.Cod == "3", CompanyUser.GetDbConnection()).FirstOrDefault().Des;
                var nomeClassificacao4 = _clsCabAppService.GetByFilter(c => c.Cod == "4", CompanyUser.GetDbConnection()).FirstOrDefault().Des;
                var nomeClassificacao5 = _clsCabAppService.GetByFilter(c => c.Cod == "5", CompanyUser.GetDbConnection()).FirstOrDefault().Des;
                var nomeClassificacao6 = _clsCabAppService.GetByFilter(c => c.Cod == "6", CompanyUser.GetDbConnection()).FirstOrDefault().Des;
                var nomeClassificacao7 = _clsCabAppService.GetByFilter(c => c.Cod == "7", CompanyUser.GetDbConnection()).FirstOrDefault().Des;
                var nomeClassificacao8 = _clsCabAppService.GetByFilter(c => c.Cod == "8", CompanyUser.GetDbConnection()).FirstOrDefault().Des;


                var clsVenAllJaneiro = balVenAllGraficoPorCls.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("01"));
                var clsVenAllFevereiro = balVenAllGraficoPorCls.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("02"));
                var clsVenAllMarço = balVenAllGraficoPorCls.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("03"));
                var clsVenAllAbril = balVenAllGraficoPorCls.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("04"));
                var clsVenAllMaio = balVenAllGraficoPorCls.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("05"));
                var clsVenAllJunho = balVenAllGraficoPorCls.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("06"));
                var clsVenAllJulho = balVenAllGraficoPorCls.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("07"));
                var clsVenAllAgosto = balVenAllGraficoPorCls.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("08"));
                var clsVenAllSetembro = balVenAllGraficoPorCls.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("09"));
                var clsVenAllOutubro = balVenAllGraficoPorCls.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("10"));
                var clsVenAllNovembro = balVenAllGraficoPorCls.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("11"));
                var clsVenAllDezembro = balVenAllGraficoPorCls.Where(c => c.My.StartsWith(ano) && c.My.EndsWith("12"));


                var clsVenAllJaneiroAnterior = balVenAllGraficoPorCls.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("01"));
                var clsVenAllFevereiroAnterior = balVenAllGraficoPorCls.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("02"));
                var clsVenAllMarçoAnterior = balVenAllGraficoPorCls.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("03"));
                var clsVenAllAbrilAnterior = balVenAllGraficoPorCls.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("04"));
                var clsVenAllMaioAnterior = balVenAllGraficoPorCls.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("05"));
                var clsVenAllJunhoAnterior = balVenAllGraficoPorCls.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("06"));
                var clsVenAllJulhoAnterior = balVenAllGraficoPorCls.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("07"));
                var clsVenAllAgostoAnterior = balVenAllGraficoPorCls.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("08"));
                var clsVenAllSetembroAnterior = balVenAllGraficoPorCls.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("09"));
                var clsVenAllOutubroAnterior = balVenAllGraficoPorCls.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("10"));
                var clsVenAllNovembroAnterior = balVenAllGraficoPorCls.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("11"));
                var clsVenAllDezembroAnterior = balVenAllGraficoPorCls.Where(c => c.My.StartsWith(anoAnteriorString) && c.My.EndsWith("12"));


                var janeiroCls = clsVenAllJaneiro.Count() > 0 ? clsVenAllJaneiro.Sum(v => v.Vlb) : 0;
                var fevereiroCls = clsVenAllFevereiro.Count() > 0 ? clsVenAllFevereiro.Sum(v => v.Vlb) : 0;
                var marçoCls = clsVenAllMarço.Count() > 0 ? clsVenAllMarço.Sum(v => v.Vlb) : 0;
                var abrilCls = clsVenAllAbril.Count() > 0 ? clsVenAllAbril.Sum(v => v.Vlb) : 0;
                var maioCls = clsVenAllMaio.Count() > 0 ? clsVenAllMaio.Sum(v => v.Vlb) : 0;
                var junhoCls = clsVenAllJunho.Count() > 0 ? clsVenAllJunho.Sum(v => v.Vlb) : 0;
                var julhoCls = clsVenAllJulho.Count() > 0 ? clsVenAllJulho.Sum(v => v.Vlb) : 0;
                var agostoCls = clsVenAllAgosto.Count() > 0 ? clsVenAllAgosto.Sum(v => v.Vlb) : 0;
                var setembroCls = clsVenAllSetembro.Count() > 0 ? clsVenAllSetembro.Sum(v => v.Vlb) : 0;
                var outubroCls = clsVenAllOutubro.Count() > 0 ? clsVenAllOutubro.Sum(v => v.Vlb) : 0;
                var novembroCls = clsVenAllNovembro.Count() > 0 ? clsVenAllNovembro.Sum(v => v.Vlb) : 0;
                var dezembroCls = clsVenAllDezembro.Count() > 0 ? clsVenAllDezembro.Sum(v => v.Vlb) : 0;




                var janeiroAnteriorCls = clsVenAllJaneiroAnterior.Count() > 0 ? clsVenAllJaneiroAnterior.Sum(v => v.Vlb) : 0;
                var fevereiroAnteriorCls = clsVenAllFevereiroAnterior.Count() > 0 ? clsVenAllFevereiroAnterior.Sum(v => v.Vlb) : 0;
                var marçoAnteriorCls = clsVenAllMarçoAnterior.Count() > 0 ? clsVenAllMarçoAnterior.Sum(v => v.Vlb) : 0;
                var abrilAnteriorCls = clsVenAllAbrilAnterior.Count() > 0 ? clsVenAllAbrilAnterior.Sum(v => v.Vlb) : 0;
                var maioAnteriorCls = clsVenAllMaioAnterior.Count() > 0 ? clsVenAllMaioAnterior.Sum(v => v.Vlb) : 0;
                var junhoAnteriorCls = clsVenAllJunhoAnterior.Count() > 0 ? clsVenAllJunhoAnterior.Sum(v => v.Vlb) : 0;
                var julhoAnteriorCls = clsVenAllJulhoAnterior.Count() > 0 ? clsVenAllJulhoAnterior.Sum(v => v.Vlb) : 0;
                var agostoAnteriorCls = clsVenAllAgostoAnterior.Count() > 0 ? clsVenAllAgostoAnterior.Sum(v => v.Vlb) : 0;
                var setembroAnteriorCls = clsVenAllSetembroAnterior.Count() > 0 ? clsVenAllSetembroAnterior.Sum(v => v.Vlb) : 0;
                var outubroAnteriorCls = clsVenAllOutubroAnterior.Count() > 0 ? clsVenAllOutubroAnterior.Sum(v => v.Vlb) : 0;
                var novembroAnteriorCls = clsVenAllNovembroAnterior.Count() > 0 ? clsVenAllNovembroAnterior.Sum(v => v.Vlb) : 0;
                var dezembroAnteriorCls = clsVenAllDezembroAnterior.Count() > 0 ? clsVenAllDezembroAnterior.Sum(v => v.Vlb) : 0;



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
    }
}