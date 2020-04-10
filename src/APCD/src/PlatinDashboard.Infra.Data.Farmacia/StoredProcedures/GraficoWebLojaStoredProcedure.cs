using System.Collections.Generic;
using System.Data;
using System.Text;
using PlatinDashboard.Domain.Farmacia.Entities;
using PlatinDashboard.Domain.Farmacia.Interfaces.StoredProcedures;

namespace PlatinDashboard.Infra.Data.Farmacia.StoredProcedures
{
    public class GraficoWebLojaStoredProcedure : StoredProcedureBase,
        IGraficoWebLojaStoredProcedure
    {
        public IEnumerable<GraficoWeb> Buscar(string my, int[] uads, string connectionString, string provider)
        {
            var uadList = new StringBuilder();
            for (int i = 0; i < uads.Length; i++)
            {
                uadList.Append(uads[i]);
                if (i < uads.Length - 1)
                    uadList.Append(",");
            }
            var dataTable = new DataTable();
            if (!provider.Equals("MySQL"))
            {
                dataTable = ExecuteProcedure("grafico_web_loja", new object[] { "myvar", my, "uads", uadList.ToString() }, connectionString);
            }
            else
            {
                dataTable = ExecuteProcedure("grafico_web_loja", new object[] { "myvar", my, "uads", uadList.ToString() }, connectionString, true);
            }
            var vendas = new List<GraficoWeb>();
            foreach (DataRow row in dataTable.Rows)
            {
                vendas.Add(new GraficoWeb(row));
            }
            return vendas;
        }
    }
}
