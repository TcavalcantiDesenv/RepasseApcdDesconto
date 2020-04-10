using PlatinDashboard.Domain.Farmacia.Entities;
using PlatinDashboard.Domain.Farmacia.Interfaces.Repositories;
using PlatinDashboard.Domain.Farmacia.Interfaces.Services;
using System.Collections.Generic;
using System.Data.Common;

namespace PlatinDashboard.Domain.Farmacia.Services
{
    public class VendaBalconistaPorHoraService : ServiceBase<VendaBalconistaPorHora>, IVendaBalconistaPorHoraService
    {
        private readonly IVendaBalconistaPorHoraRepository _vendaBalconistaPorHoraRepository;

        public VendaBalconistaPorHoraService(IVendaBalconistaPorHoraRepository vendaBalconistaPorHoraRepository)
            :base(vendaBalconistaPorHoraRepository)
        {
            _vendaBalconistaPorHoraRepository = vendaBalconistaPorHoraRepository;
        }

        public List<VendaBalconistaPorHora> BuscarPorLoja(int lojaId, DbConnection connection)
        {
            return _vendaBalconistaPorHoraRepository.BuscarPorLoja(lojaId, connection);
        }
    }
}
