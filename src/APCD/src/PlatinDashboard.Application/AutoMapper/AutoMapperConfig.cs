using AutoMapper;
using PlatinDashboard.Application.Administrativo.AutoMapper;
using PlatinDashboard.Application.Farmacia.AutoMapper;

namespace PlatinDashboard.Application.AutoMapper
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<DomainToViewModelMappingProfile>();
                x.AddProfile<ViewModelToDomainMappingProfile>();
                x.AddProfile<ViewModelToViewModelMappingProfile>();
                x.AddProfile<FarmaciaDomainToViewModelMappingProfile>();
                x.AddProfile<FarmaciaViewModelToDomainMappingProfile>();
                x.AddProfile<AdministrativoDomainToViewModelMappingProfile>();
                x.AddProfile<AdministrativoViewModelToDomainMappingProfile>();
            });
        }
    }
}
