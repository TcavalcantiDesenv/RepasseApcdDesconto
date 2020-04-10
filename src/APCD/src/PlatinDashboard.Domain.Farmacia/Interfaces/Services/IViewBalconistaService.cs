using PlatinDashboard.Domain.Farmacia.Entities;
using System.Collections.Generic;
using System.Data.Common;

namespace PlatinDashboard.Domain.Farmacia.Interfaces.Services
{
    public interface IViewBalconistaService : IServiceBase<ViewBalconista>
    {
        List<ViewBalconista> BuscarPorLojaEPeriodo(int lojaId, string periodo, DbConnection connection);
    }
}
