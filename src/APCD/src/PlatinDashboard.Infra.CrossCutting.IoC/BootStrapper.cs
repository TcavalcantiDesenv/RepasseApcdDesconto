using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PlatinDashboard.Application.Administrativo.AppServices;
using PlatinDashboard.Application.Administrativo.Interfaces;
using PlatinDashboard.Application.AppServices;
using PlatinDashboard.Application.Farmacia.AppServices;
using PlatinDashboard.Application.Farmacia.Interfaces;
using PlatinDashboard.Application.Interfaces;
using PlatinDashboard.Domain.Administrativo.Interfaces.Repositories;
using PlatinDashboard.Domain.Administrativo.Interfaces.Services;
using PlatinDashboard.Domain.Administrativo.Services;
using PlatinDashboard.Domain.Farmacia.Interfaces.Repositories;
using PlatinDashboard.Domain.Farmacia.Interfaces.Services;
using PlatinDashboard.Domain.Farmacia.Interfaces.StoredProcedures;
using PlatinDashboard.Domain.Farmacia.Services;
using PlatinDashboard.Domain.Interfaces.Repositories;
using PlatinDashboard.Domain.Interfaces.Services;
using PlatinDashboard.Domain.Services;
using PlatinDashboard.Infra.CrossCutting.Identity.Configuration;
using PlatinDashboard.Infra.CrossCutting.Identity.Contexto;
using PlatinDashboard.Infra.CrossCutting.Identity.Model;
using PlatinDashboard.Infra.Data.Administrativo.Repositories;
using PlatinDashboard.Infra.Data.Farmacia.Repositories;
using PlatinDashboard.Infra.Data.Farmacia.StoredProcedures;
using PlatinDashboard.Infra.Data.Repositories;
using SimpleInjector;

namespace PlatinDashboard.Infra.CrossCutting.IoC
{
    public class BootStrapper
    {
        public static void RegisterServices(Container container)
        {
            // Lifestyle.Transient => Uma instancia para cada solicitacao;
            // Lifestyle.Singleton => Uma instancia unica para a classe
            // Lifestyle.Scoped => Uma instancia unica para o request

            #region IdentityDependences
            //Identity
            container.Register<IdentityContext>(Lifestyle.Scoped);
            container.Register<IUserStore<ApplicationUser>>(() => new UserStore<ApplicationUser>(new IdentityContext()), Lifestyle.Scoped);
            container.Register<IRoleStore<IdentityRole, string>>(() => new RoleStore<IdentityRole>(new IdentityContext()), Lifestyle.Scoped);
            container.Register<ApplicationRoleManager>(Lifestyle.Scoped);
            container.Register<ApplicationUserManager>(Lifestyle.Scoped);
            container.Register<ApplicationSignInManager>(Lifestyle.Scoped);
            #endregion

            #region AppDependences
            container.Register<ICompanyAppService, CompanyAppService>(Lifestyle.Scoped);
            container.Register<IUserAppService, UserAppService>(Lifestyle.Scoped);
            container.Register<IVideoAppService, VideoAppService>(Lifestyle.Scoped);
            container.Register<IStoreAppService, StoreAppService>(Lifestyle.Scoped);
            #endregion

            #region DomainDependences
            container.Register(typeof(Domain.Interfaces.Services.IServiceBase<>), typeof(Domain.Services.ServiceBase<>), Lifestyle.Scoped);
            container.Register<ICompanyService, CompanyService>(Lifestyle.Scoped);
            container.Register<IUserService, UserService>(Lifestyle.Scoped);
            container.Register<IVideoService, VideoService>(Lifestyle.Scoped);
            container.Register<IStoreService, StoreService>(Lifestyle.Scoped);
            #endregion

            #region DataDependences
            container.Register(typeof(Domain.Interfaces.Repositories.IRepositoryBase<>), typeof(Data.Repositories.RepositoryBase<>), Lifestyle.Scoped);
            container.Register<ICompanyRepository, CompanyRepository>(Lifestyle.Scoped);
            container.Register<IUserRepository, UserRepository>(Lifestyle.Scoped);
            container.Register<IVideoRepository, VideoRepository>(Lifestyle.Scoped);
            container.Register<IStoreRepository, StoreRepository>(Lifestyle.Scoped);
            #endregion

            #region FarmaciaDependences

            #region AppDependences
            container.Register<IUadCabAppService, UadCabAppService>(Lifestyle.Scoped);
            container.Register<IClsCabAppService, ClsCabAppService>(Lifestyle.Scoped);
            container.Register<IViewBalconistaAppService, ViewBalconistaAppService>(Lifestyle.Scoped);
            container.Register<IFunCabAppService, FunCabAppService>(Lifestyle.Scoped);
            container.Register<IClsVenAllAppService, ClsVenAllAppService>(Lifestyle.Scoped);
            container.Register<IGraficoWebAppService, GraficoWebAppService>(Lifestyle.Scoped);
            container.Register<IVenUadAppService, VenUadAppService>(Lifestyle.Scoped);
            container.Register<IVendaLojaPorMesAppService, VendaLojaPorMesAppService>(Lifestyle.Scoped);
            container.Register<IVendaLojaPorHoraAppService, VendaLojaPorHoraAppService>(Lifestyle.Scoped);
            container.Register<IVendasLojaPorDiaHoraAppService, VendasLojaPorDiaHoraAppService>(Lifestyle.Scoped);
            container.Register<IVendaBalconistaPorHoraAppService, VendaBalconistaPorHoraAppService>(Lifestyle.Scoped);
            #endregion

            #region DomainDependences
            container.Register(typeof(Domain.Farmacia.Interfaces.Services.IServiceBase<>), typeof(Domain.Farmacia.Services.ServiceBase<>), Lifestyle.Scoped);
            container.Register<IClsCabService, ClsCabService>(Lifestyle.Scoped);
            container.Register<IClsVenAllService, ClsVenAllService>(Lifestyle.Scoped);
            container.Register<IGraficoWebService, GraficoWebService>(Lifestyle.Scoped);
            container.Register<IVenUadService, VenUadService>(Lifestyle.Scoped);
            container.Register<IUadCabService, UadCabService>(Lifestyle.Scoped);
            container.Register<IFunCabService, FunCabService>(Lifestyle.Scoped);
            container.Register<IViewBalconistaService, ViewBalconistaService>(Lifestyle.Scoped);
            container.Register<IVendaBalconistaPorHoraService, VendaBalconistaPorHoraService>(Lifestyle.Scoped);
            container.Register<IVendaLojaPorHoraService, VendaLojaPorHoraService>(Lifestyle.Scoped);
            container.Register<IVendasLojaPorDiaHoraService, VendasLojaPorDiaHoraService>(Lifestyle.Scoped);
            container.Register<IVendaLojaPorMesService, VendaLojaPorMesService>(Lifestyle.Scoped);
            container.Register<IClsVenAllGraficoTotalService, ClsVenAllGraficoTotalService>(Lifestyle.Scoped);
            container.Register<IClsVenAllGraficoPorClsService, ClsVenAllGraficoPorClsService>(Lifestyle.Scoped);
            container.Register<IVenAllService, VenAllService>(Lifestyle.Scoped);
            container.Register<IVItensPorClienteService, VItensPorClienteService>(Lifestyle.Scoped);
            #endregion

            #region DataDependences
            container.Register(typeof(Domain.Farmacia.Interfaces.Repositories.IRepositoryBase<>), typeof(Data.Farmacia.Repositories.RepositoryBase<>), Lifestyle.Scoped);
            container.Register<IClsCabRepository, ClsCabRepository>(Lifestyle.Scoped);
            container.Register<IClsVenAllRepository, ClsVenAllRepository>(Lifestyle.Scoped);
            container.Register<IGraficoWebRepository, GraficoWebRepository>(Lifestyle.Scoped);
            container.Register<IVenUadRepository, VenUadRepository>(Lifestyle.Scoped);
            container.Register<IVenUadClsRepository, VenUadClsRepository>(Lifestyle.Scoped);
            container.Register<IVenUadClsDashRepository, VenUadClsDashRepository>(Lifestyle.Scoped);
            container.Register<IVenUadMensalRepository, VenUadMensalRepository>(Lifestyle.Scoped);
            container.Register<IUadCabRepository, UadCabRepository>(Lifestyle.Scoped);
            container.Register<IFunCabRepository, FunCabRepository>(Lifestyle.Scoped);
            container.Register<IViewBalconistaRepository, ViewBalconistaRepository>(Lifestyle.Scoped);
            container.Register<IVendaBalconistaPorHoraRepository, VendaBalconistaPorHoraRepository>(Lifestyle.Scoped);
            container.Register<IVendaLojaPorHoraRepository, VendaLojaPorHoraRepository>(Lifestyle.Scoped);
            container.Register<IVendasLojaPorDiaHoraRepository, VendasLojaPorDiaHoraRepository>(Lifestyle.Scoped);
            container.Register<IVendaLojaPorMesRepository, VendaLojaPorMesRepository>(Lifestyle.Scoped);
            container.Register<IClsVenAllGraficoTotalRepository, ClsVenAllGraficoTotalRepository>(Lifestyle.Scoped);
            container.Register<IClsVenAllGraficoPorClsRepository, ClsVenAllGraficoPorClsRepository>(Lifestyle.Scoped);
            container.Register<IVenAllRepository, VenAllRepository>(Lifestyle.Scoped);
            container.Register<IVItensPorClienteRepository, VItensPorClienteRepository>(Lifestyle.Scoped);
            container.Register<IVendaBalconistaPorClassificacaoRepository, VendaBalconistaPorClassificacaoRepository>(Lifestyle.Scoped);
            container.Register<IBalVenAllGraficoTotalRepository, BalVenAllGraficoTotalRepository>(Lifestyle.Scoped);
            container.Register<IBalVenAllGraficoPorClsRepository, BalVenAllGraficoPorClsRepository>(Lifestyle.Scoped);
            container.Register<IVendaClassificacaoLojaTotalRepository, VendaClassificacaoLojaTotalRepository>(Lifestyle.Scoped);
            container.Register<IVendaClassificacaoLojaRepository, VendaClassificacaoLojaRepository>(Lifestyle.Scoped);

            container.Register<IStoredProcedureBase, StoredProcedureBase>(Lifestyle.Scoped);
            container.Register<IVendasClassificacaoLojaStoredProcedure, VendasClassificacaoLojaStoredProcedure>(Lifestyle.Scoped);
            container.Register<IVendasClassificacaoLojaTotalStoredProcedure, VendasClassificacaoLojaTotalStoredProcedure>(Lifestyle.Scoped);
            container.Register<IGraficoWebLojaStoredProcedure, GraficoWebLojaStoredProcedure>(Lifestyle.Scoped);
            #endregion

            #endregion

            #region AdministrativoDependences

            #region AppDependences
            container.Register<IClienteAppService, ClienteAppService>(Lifestyle.Scoped);

            #endregion

            #region DomainDependences
            container.Register(typeof(Domain.Administrativo.Interfaces.Services.IServiceBase<>), typeof(Domain.Administrativo.Services.ServiceBase<>), Lifestyle.Scoped);
            container.Register<IClienteService, ClienteService>(Lifestyle.Scoped);
            #endregion

            #region DataDependences
            container.Register(typeof(Domain.Administrativo.Interfaces.Repositories.IRepositoryBase<>), typeof(Data.Administrativo.Repositories.RepositoryBase<>), Lifestyle.Scoped);
            container.Register<IClienteRepository, ClienteRepository>(Lifestyle.Scoped);
            #endregion

            #endregion
        }
    }
}
