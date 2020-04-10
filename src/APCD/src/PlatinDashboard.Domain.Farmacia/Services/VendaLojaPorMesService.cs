using PlatinDashboard.Domain.Farmacia.Entities;
using PlatinDashboard.Domain.Farmacia.Interfaces.Repositories;
using PlatinDashboard.Domain.Farmacia.Interfaces.Services;

namespace PlatinDashboard.Domain.Farmacia.Services
{
    public class VendaLojaPorMesService : ServiceBase<VendaLojaPorMes>, IVendaLojaPorMesService
    {
        private readonly IVendaLojaPorMesRepository _vendaLojaPorMesRepository;

        public VendaLojaPorMesService(IVendaLojaPorMesRepository vendaLojaPorMesRepository)
            :base(vendaLojaPorMesRepository)
        {
            _vendaLojaPorMesRepository = vendaLojaPorMesRepository;
        }
    }
}
