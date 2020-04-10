using AutoMapper;
using PlatinDashboard.Application.Administrativo.ViewModels;
using PlatinDashboard.Domain.Administrativo.Entities;

namespace PlatinDashboard.Application.Administrativo.AutoMapper
{
    public class AdministrativoDomainToViewModelMappingProfile : Profile
    {
        public AdministrativoDomainToViewModelMappingProfile()
        {
            CreateMap<Cliente, ClienteViewModel>();
            CreateMap<Rede, RedeViewModel>();
            CreateMap<Farmacia, FarmaciaViewModel>();
            CreateMap<Usuario, UsuarioViewModel>();
        }
    }
}
