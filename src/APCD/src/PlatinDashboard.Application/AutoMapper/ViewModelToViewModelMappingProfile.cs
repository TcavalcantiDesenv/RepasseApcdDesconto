using AutoMapper;
using PlatinDashboard.Application.ViewModels.UserViewModels;

namespace PlatinDashboard.Application.AutoMapper
{
    public class ViewModelToViewModelMappingProfile : Profile
    {
        public ViewModelToViewModelMappingProfile()
        {
            #region UserMaps
            CreateMap<UserViewModel, EditUserViewModel>();
            #endregion
        }
    }
}
