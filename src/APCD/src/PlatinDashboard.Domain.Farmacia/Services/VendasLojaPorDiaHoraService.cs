using PlatinDashboard.Domain.Farmacia.Entities;
using PlatinDashboard.Domain.Farmacia.Interfaces.Repositories;
using PlatinDashboard.Domain.Farmacia.Interfaces.Services;

namespace PlatinDashboard.Domain.Farmacia.Services
{
    public class VendasLojaPorDiaHoraService : ServiceBase<VendasLojaPorDiaHora>,
        IVendasLojaPorDiaHoraService
    {
        private readonly IVendasLojaPorDiaHoraRepository _vendasLojaPorDiaHoraRepository;

        public VendasLojaPorDiaHoraService(IVendasLojaPorDiaHoraRepository vendasLojaPorDiaHoraRepository)
            : base(vendasLojaPorDiaHoraRepository)
        {
            _vendasLojaPorDiaHoraRepository = vendasLojaPorDiaHoraRepository;
        }
    }
}
