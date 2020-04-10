using PlatinDashboard.Domain.Farmacia.Entities;
using System.Collections.Generic;
using System.Data.Common;

namespace PlatinDashboard.Domain.Farmacia.Interfaces.Services
{
    public interface IVendaLojaPorHoraService : IServiceBase<VendaLojaPorHora>
    {
        List<VendaLojaPorHora> BuscarPorLoja(int lojaId, DbConnection connection);
    }
}
