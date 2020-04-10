using AutoMapper;
using PlatinDashboard.Application.ViewModels.ChartViewModels;
using PlatinDashboard.Application.ViewModels.CompanyViewModels;
using PlatinDashboard.Application.ViewModels.MidiaViewModels;
using PlatinDashboard.Application.ViewModels.StoreViewModels;
using PlatinDashboard.Application.ViewModels.UserViewModels;
using PlatinDashboard.Domain.Entities;

namespace PlatinDashboard.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            #region CompanyMaps
            CreateMap<CompanyViewModel, Company>()
                .ForMember(dest => dest.CompanyType, opt => opt.Ignore())
                .ForMember(dest => dest.CustomerCode, opt => opt.Ignore())
                .ForMember(dest => dest.ImportedFromAdministrative, opt => opt.Ignore());
            CreateMap<CreateCompanyViewModel, Company>();
            #endregion

            #region UserMaps
            CreateMap<UserViewModel, User>();
            CreateMap<CreateUserViewModel, User>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore());
            CreateMap<EditUserViewModel, User>()
                .ForMember(dest => dest.Email, opt => opt.Ignore())
                .ForMember(dest => dest.UserName, opt => opt.Ignore());
            CreateMap<CreateCompanyViewModel, User>();
            CreateMap<ProfileUserViewModel, User>()
                .ForMember(dest => dest.Email, opt => opt.Ignore())
                .ForMember(dest => dest.UserName, opt => opt.Ignore())
                .ForMember(dest => dest.Active, opt => opt.Ignore())
                .ForMember(dest => dest.UserType, opt => opt.Ignore());
            #endregion

            #region ChartMaps
            CreateMap<ChartViewModel, Chart>();
            CreateMap<EditChartViewModel, Chart>();
            #endregion

            #region VideoMaps
            CreateMap<Video, VideoViewModel>();
            #endregion

            #region StoreMaps
            CreateMap<StoreViewModel, Store>();
            #endregion
        }
    }
}
