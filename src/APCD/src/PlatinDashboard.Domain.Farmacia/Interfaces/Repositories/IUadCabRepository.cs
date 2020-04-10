using PlatinDashboard.Domain.Farmacia.Entities;
using System.Collections.Generic;
using System.Data.Common;

namespace PlatinDashboard.Domain.Farmacia.Interfaces.Repositories
{
    public interface IUadCabRepository : IRepositoryBase<UadCab>
    {
        IEnumerable<UadCab> GetStores(DbConnection dbConnection);
    }
}
