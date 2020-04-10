using PlatinDashboard.Domain.Farmacia.Entities;
using System.Collections.Generic;
using System.Data.Common;

namespace PlatinDashboard.Domain.Farmacia.Interfaces.Repositories
{
    public interface IViewBalconistaRepository : IRepositoryBase<ViewBalconista>
    {
        List<ViewBalconista> BuscarPorLojaEPeriodo(int lojaId, string periodo, DbConnection connection);
    }
}
