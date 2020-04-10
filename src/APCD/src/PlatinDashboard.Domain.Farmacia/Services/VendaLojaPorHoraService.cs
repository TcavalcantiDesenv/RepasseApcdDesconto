using PlatinDashboard.Domain.Farmacia.Entities;
using PlatinDashboard.Domain.Farmacia.Interfaces.Repositories;
using PlatinDashboard.Domain.Farmacia.Interfaces.Services;
using System.Collections.Generic;
using System.Data.Common;

namespace PlatinDashboard.Domain.Farmacia.Services
{
    public class VendaLojaPorHoraService : ServiceBase<VendaLojaPorHora>, IVendaLojaPorHoraService
    {
        private readonly IVendaLojaPorHoraRepository _vendaLojaPorHoraRepository;

        public VendaLojaPorHoraService(IVendaLojaPorHoraRepository vendaLojaPorHoraRepository)
            :base(vendaLojaPorHoraRepository)
        {
            _vendaLojaPorHoraRepository = vendaLojaPorHoraRepository;
        }

        public List<VendaLojaPorHora> BuscarPorLoja(int lojaId, DbConnection connection)
        {
            return _vendaLojaPorHoraRepository.BuscarPorLoja(lojaId, connection);
        }
    }
}
