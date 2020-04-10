using PlatinDashboard.Domain.Farmacia.Entities;
using PlatinDashboard.Domain.Farmacia.Interfaces.Repositories;
using PlatinDashboard.Domain.Farmacia.Interfaces.Services;

namespace PlatinDashboard.Domain.Farmacia.Services
{
    public class VItensPorClienteService : ServiceBase<VItensPorCliente>, IVItensPorClienteService
    {
        private readonly IVItensPorClienteRepository _vItensPorClienteRepository;

        public VItensPorClienteService(IVItensPorClienteRepository vItensPorClienteRepository)
            :base(vItensPorClienteRepository)
        {
            _vItensPorClienteRepository = vItensPorClienteRepository;
        }
    }
}
