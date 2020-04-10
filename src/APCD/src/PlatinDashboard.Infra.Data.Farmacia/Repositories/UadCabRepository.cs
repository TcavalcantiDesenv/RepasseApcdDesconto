using PlatinDashboard.Domain.Farmacia.Entities;
using PlatinDashboard.Domain.Farmacia.Interfaces.Repositories;
using PlatinDashboard.Infra.Data.Farmacia.Context;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace PlatinDashboard.Infra.Data.Farmacia.Repositories
{
    public class UadCabRepository : RepositoryBase<UadCab>, IUadCabRepository
    {
        public IEnumerable<UadCab> GetStores(DbConnection dbConnection)
        {
            if (dbConnection.ToString() == "MySql.Data.MySqlClient.MySqlConnection")
            {
                var db = new MySqlContext(dbConnection);
                return db.UadCabs.AsNoTracking().Where(u => u.Tip == "LJ" && u.Atv == true && db.FunCabs.Any(f => f.Uad == u.Uad)).ToList();
            }
            else
            {
                var db = new PostgreContext(dbConnection);
                return db.UadCabs.AsNoTracking().Where(u => u.Tip == "LJ" && u.Atv == true && db.FunCabs.Any(f => f.Uad == u.Uad)).ToList();
            }            
        }

        public new UadCab GetById(int uad, DbConnection dbConnection)
        {
            if (dbConnection.ToString() == "MySql.Data.MySqlClient.MySqlConnection")
            {
                var db = new MySqlContext(dbConnection);
                return db.UadCabs.AsNoTracking().FirstOrDefault(u => u.Uad == uad);
            }
            else
            {
                var db = new PostgreContext(dbConnection);
                return db.UadCabs.AsNoTracking().FirstOrDefault(u => u.Uad == uad);
            }            
        }
    }
}
