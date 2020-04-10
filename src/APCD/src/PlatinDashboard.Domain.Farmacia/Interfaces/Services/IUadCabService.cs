using PlatinDashboard.Domain.Farmacia.Entities;
using System.Collections.Generic;
using System.Data.Common;

namespace PlatinDashboard.Domain.Farmacia.Interfaces.Services
{
    public interface IUadCabService : IServiceBase<UadCab>
    {
        IEnumerable<UadCab> GetStores(DbConnection dbConnection);
    }
}
