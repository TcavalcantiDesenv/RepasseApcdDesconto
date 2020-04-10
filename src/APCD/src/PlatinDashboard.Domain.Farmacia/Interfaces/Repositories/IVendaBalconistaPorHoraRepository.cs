using PlatinDashboard.Domain.Farmacia.Entities;
using System.Collections.Generic;
using System.Data.Common;

namespace PlatinDashboard.Domain.Farmacia.Interfaces.Repositories
{
    public interface IVendaBalconistaPorHoraRepository : IRepositoryBase<VendaBalconistaPorHora>
    {
        List<VendaBalconistaPorHora> BuscarPorLoja(int lojaId, DbConnection connection);
    }
}
