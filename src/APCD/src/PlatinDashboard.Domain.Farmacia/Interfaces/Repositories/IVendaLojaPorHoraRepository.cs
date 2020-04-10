using PlatinDashboard.Domain.Farmacia.Entities;
using System.Collections.Generic;
using System.Data.Common;

namespace PlatinDashboard.Domain.Farmacia.Interfaces.Repositories
{
    public interface IVendaLojaPorHoraRepository : IRepositoryBase<VendaLojaPorHora>
    {
        List<VendaLojaPorHora> BuscarPorLoja(int lojaId, DbConnection connection);
    }
}
