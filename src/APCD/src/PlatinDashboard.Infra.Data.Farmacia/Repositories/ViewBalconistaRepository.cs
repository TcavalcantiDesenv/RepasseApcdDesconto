using PlatinDashboard.Domain.Farmacia.Entities;
using PlatinDashboard.Domain.Farmacia.Interfaces.Repositories;
using PlatinDashboard.Infra.Data.Farmacia.Context;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace PlatinDashboard.Infra.Data.Farmacia.Repositories
{
    public class ViewBalconistaRepository : RepositoryBase<ViewBalconista>, IViewBalconistaRepository
    {
        public List<ViewBalconista> BuscarPorLojaEPeriodo(int lojaId, string periodo, DbConnection connection)
        {
            //Método para buscar as vendas por loja e mês
            if (connection.ToString() == "MySql.Data.MySqlClient.MySqlConnection")
            {
                var db = new MySqlContext(connection);
                return db.ViewBalconistas.AsNoTracking().Where(v => v.IdLoja == lojaId && v.MesAno == periodo).ToList();
            }
            else
            {
                var db = new PostgreContext(connection);
                return db.ViewBalconistas.AsNoTracking().Where(v => v.IdLoja == lojaId && v.MesAno == periodo).ToList();
            }
        }
    }
}
