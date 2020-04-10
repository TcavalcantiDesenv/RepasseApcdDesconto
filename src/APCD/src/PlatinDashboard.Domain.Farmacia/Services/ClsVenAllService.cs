using PlatinDashboard.Domain.Farmacia.Entities;
using PlatinDashboard.Domain.Farmacia.Interfaces.Repositories;
using PlatinDashboard.Domain.Farmacia.Interfaces.Services;

namespace PlatinDashboard.Domain.Farmacia.Services
{
    public class ClsVenAllService : ServiceBase<ClsVenAll>, IClsVenAllService
    {
        private readonly IClsVenAllRepository _clsVenAllRepository;

        public ClsVenAllService(IClsVenAllRepository clsVenAllRepository)
            :base(clsVenAllRepository)
        {
            _clsVenAllRepository = clsVenAllRepository;
        }
    }
}
