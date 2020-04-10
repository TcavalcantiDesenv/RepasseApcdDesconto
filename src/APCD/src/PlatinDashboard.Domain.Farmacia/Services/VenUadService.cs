using PlatinDashboard.Domain.Farmacia.Entities;
using PlatinDashboard.Domain.Farmacia.Interfaces.Repositories;
using PlatinDashboard.Domain.Farmacia.Interfaces.Services;

namespace PlatinDashboard.Domain.Farmacia.Services
{
    public class VenUadService : ServiceBase<VenUad>, IVenUadService
    {
        private readonly IVenUadRepository _venUadRepository;

        public VenUadService(IVenUadRepository venUadRepository) : base(venUadRepository)
        {
            _venUadRepository = venUadRepository;
        }
    }
}
