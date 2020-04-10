using DevExpress.Web.Mvc;
using PlatinDashboard.Application.Farmacia.Interfaces;
using PlatinDashboard.Application.Farmacia.ViewModels.Loja;
using PlatinDashboard.Application.Interfaces;
using PlatinDashboard.Domain.Farmacia.Interfaces.Repositories;
using PlatinDashboard.Presentation.MVC.Helpers;
using PlatinDashboard.Presentation.MVC.MvcFilters;
using System;
using System.Linq;
using System.Web.Mvc;

namespace PlatinDashboard.Presentation.MVC.Controllers
{
    [Authorize]    
    public class CuboController : BaseController
    {
        private readonly IUadCabAppService _uadCabAppService;
        private readonly IClsCabAppService _clsCabAppService;
        private readonly IViewBalconistaAppService _viewBalconistaAppService;
        private readonly IFunCabAppService _funCabAppService;
        private readonly IGraficoWebRepository _graficoWebRepository;
        private readonly IVenUadRepository _venUadRepository;

        public CuboController(IUserAppService userAppService,
                                    ICompanyAppService companyAppService,
                                    IUadCabAppService uadCabAppService,
                                    IClsCabAppService clsCabAppService,
                                    IViewBalconistaAppService viewBalconistaAppService,
                                    IFunCabAppService funCabAppService,
                                    IGraficoWebRepository graficoWebRepository,
                                    IVenUadRepository venUadRepository)
                                    : base(userAppService, companyAppService)
        {
            _uadCabAppService = uadCabAppService;
            _clsCabAppService = clsCabAppService;
            _viewBalconistaAppService = viewBalconistaAppService;
            _funCabAppService = funCabAppService;
            _graficoWebRepository = graficoWebRepository;
            _venUadRepository = venUadRepository;
        }

        [HttpGet]
        [ClaimsAuthorize("ChartCubo", "Allowed")]
        public ActionResult Index()
        {
            ViewBag.Cliente = (Session["userId"] == null ? "" : LoggedUser.Name);
            var venUad = _venUadRepository.GetAll(CompanyUser.GetDbConnection()).OrderByDescending(v => v.Dat);
            var lojas = _uadCabAppService.GetAll(CompanyUser.GetDbConnection());
            var model = (from p in venUad
                         select new VenUadGridViewModel
                         {
                             Data = p.Dat,
                             Loja = lojas.FirstOrDefault(l => l.Uad == p.Uad).Des,
                             Bruto = p.Vlb != null ? p.Vlb.Value : 0,
                             Desconto = p.Vld != null ? p.Vld.Value : 0,
                             Devolucao = p.Vde != null ? p.Vde.Value : 0,
                             Liquida = (p.Vlb - p.Vld) != null ? (p.Vlb - p.Vld).Value : 0,
                             Lucro = ((p.Vlb - p.Vld) - p.Vlc) != null ? ((p.Vlb - p.Vld) - p.Vlc).Value : 0,
                             PercentualMargem = p.Vlc > 0 ? Math.Round((p.Vlc / (p.Vlb - p.Vld) * 100).Value, 2) : 0,
                             TicketMedio = p.Tme != null ? p.Tme.Value : 0,
                             QtMediaClientes = p.Ite > 0 ? (p.Ite / p.Reg).Value : 0,
                             ClientesAtendidos = p.Ite != null ? p.Ite.Value : 0
                         });
            return View(model);
        }


        [ClaimsAuthorize("ChartCubo", "Allowed")]
        public ActionResult PivotGrid()
        {
            var venUad = _venUadRepository.GetAll(CompanyUser.GetDbConnection()).OrderByDescending(v => v.Dat);
            var lojas = _uadCabAppService.GetAll(CompanyUser.GetDbConnection());
            var model = (from p in venUad
                         select new VenUadGridViewModel
                         {
                             Data = p.Dat,
                             Loja = lojas.FirstOrDefault(l => l.Uad == p.Uad).Des,
                             Bruto = p.Vlb != null ? p.Vlb.Value : 0,
                             Desconto = p.Vld != null ? p.Vld.Value : 0,
                             Devolucao = p.Vde != null ? p.Vde.Value : 0,
                             Liquida = (p.Vlb - p.Vld) != null ? (p.Vlb - p.Vld).Value : 0,
                             Lucro = ((p.Vlb - p.Vld) - p.Vlc) != null ? ((p.Vlb - p.Vld) - p.Vlc).Value : 0,
                             PercentualMargem = p.Vlc > 0 ? Math.Round((p.Vlc / (p.Vlb - p.Vld) * 100).Value, 2) : 0,
                             TicketMedio = p.Tme != null ? p.Tme.Value : 0,
                             QtMediaClientes = p.Ite > 0 ? (p.Ite / p.Reg).Value : 0,
                             ClientesAtendidos = p.Ite != null ? p.Ite.Value : 0
                         });
            return PartialView("_PivotGrid", model);
        }

    }
}