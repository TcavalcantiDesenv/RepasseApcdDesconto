using AutoMapper;
using PlatinDashboard.Application.Farmacia.ViewModels.Loja;
using PlatinDashboard.Application.Farmacia.ViewModels.Balconista;
using PlatinDashboard.Domain.Farmacia.Entities;
using PlatinDashboard.Application.Farmacia.ViewModels.Classificacao;

namespace PlatinDashboard.Application.Farmacia.AutoMapper
{
    public class FarmaciaDomainToViewModelMappingProfile : Profile
    {
        public FarmaciaDomainToViewModelMappingProfile()
        {
            #region LojaMaps
            CreateMap<UadCab, UadCabViewModel>();
            CreateMap<GraficoWeb, GraficoWebViewModel>();
            CreateMap<VenUad, VenUadViewModel>();
            CreateMap<VendaLojaPorMes, VendaLojaPorMesViewModel>();
            CreateMap<VendaLojaPorHora, VendaLojaPorHoraViewModel>();
            CreateMap<VendasLojaPorDiaHora, VendasLojaPorDiaHoraViewModel>();
            #endregion

            #region BalconistaMaps
            CreateMap<ViewBalconista, ViewBalconistaViewModel>();
            CreateMap<FunCab, FunCabViewModel>();
            CreateMap<VendaBalconistaPorHora, VendaBalconistaPorHoraViewModel>();
            #endregion

            #region ClassificacaoMaps
            CreateMap<ClsCab, ClsCabViewModel>();
            CreateMap<ClsVenAll, ClsVenAllViewModel>();
            #endregion
        }
    }
}
