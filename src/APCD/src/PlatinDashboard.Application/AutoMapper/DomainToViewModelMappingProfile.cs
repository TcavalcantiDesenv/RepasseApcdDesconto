using AutoMapper;
using PlatinDashboard.Application.ViewModels.ChartViewModels;
using PlatinDashboard.Application.ViewModels.CompanyViewModels;
using PlatinDashboard.Application.ViewModels.MidiaViewModels;
using PlatinDashboard.Application.ViewModels.StoreViewModels;
using PlatinDashboard.Application.ViewModels.UserViewModels;
using PlatinDashboard.Domain.Administrativo.Entities;
using PlatinDashboard.Domain.Entities;

namespace PlatinDashboard.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            #region CompanyMaps
            CreateMap<Company, CompanyViewModel>();
            CreateMap<Company, CreateCompanyViewModel>();
            #endregion

            #region UserMaps
            CreateMap<User, UserViewModel>();
            CreateMap<User, CreateUserViewModel>();
            CreateMap<User, EditUserViewModel>();
            CreateMap<User, CreateCompanyViewModel>();
            #endregion

            #region ChartMaps
            CreateMap<Chart, ChartViewModel>();
            CreateMap<Chart, EditChartViewModel>();
            #endregion

            #region VideoMaps
            CreateMap<VideoViewModel, Video>();
            #endregion

            #region StoreMaps
            CreateMap<Store, StoreViewModel>();
            #endregion
        }
    }
}
