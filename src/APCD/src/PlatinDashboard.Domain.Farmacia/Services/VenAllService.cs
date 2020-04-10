using PlatinDashboard.Domain.Farmacia.Entities;
using PlatinDashboard.Domain.Farmacia.Interfaces.Repositories;
using PlatinDashboard.Domain.Farmacia.Interfaces.Services;

namespace PlatinDashboard.Domain.Farmacia.Services
{
    public class VenAllService : ServiceBase<VenAll>, IVenAllService
    {
        private readonly IVenAllRepository _venAllRepository;

        public VenAllService(IVenAllRepository venAllRepository) : base(venAllRepository)
        {
            _venAllRepository = venAllRepository;
        }
    }
}
