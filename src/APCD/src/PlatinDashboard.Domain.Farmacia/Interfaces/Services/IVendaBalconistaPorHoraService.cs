using PlatinDashboard.Domain.Farmacia.Entities;
using System.Collections.Generic;
using System.Data.Common;

namespace PlatinDashboard.Domain.Farmacia.Interfaces.Services
{
    public interface IVendaBalconistaPorHoraService : IServiceBase<VendaBalconistaPorHora>
    {
        List<VendaBalconistaPorHora> BuscarPorLoja(int lojaId, DbConnection connection);
    }
}
