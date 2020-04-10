using PlatinDashboard.Domain.Farmacia.Entities;
using PlatinDashboard.Domain.Farmacia.Interfaces.Repositories;
using PlatinDashboard.Infra.Data.Farmacia.Context;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace PlatinDashboard.Infra.Data.Farmacia.Repositories
{
    public class VendaLojaPorHoraRepository : RepositoryBase<VendaLojaPorHora>, IVendaLojaPorHoraRepository
    {
        public List<VendaLojaPorHora> BuscarPorLoja(int lojaId, DbConnection connection)
        {
            //Método para buscar as vendas por loja separado por horas
            if (connection.ToString() == "MySql.Data.MySqlClient.MySqlConnection")
            {
                var db = new MySqlContext(connection);
                return db.VendasLojaPorHora.Where(v => v.Loja == lojaId).ToList();
            }
            else
            {
                var db = new PostgreContext(connection);
                return db.VendasLojaPorHora.Where(v => v.Loja == lojaId).ToList();
            }
        }
    }
}
