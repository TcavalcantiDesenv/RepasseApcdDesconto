using PlatinDashboard.Application.Farmacia.ViewModels.Loja;
using System.Collections.Generic;
using System.Data.Common;

namespace PlatinDashboard.Application.Farmacia.Interfaces
{
    public interface IUadCabAppService
    {
        UadCabViewModel GetById(int id, DbConnection dbConnection);
        IEnumerable<UadCabViewModel> GetStores(DbConnection dbConnection);

        IEnumerable<UadCabViewModel> GetAll(DbConnection dbConnection);
    }
}
