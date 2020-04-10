using PlatinDashboard.Domain.Farmacia.Entities;
using PlatinDashboard.Domain.Farmacia.Interfaces.StoredProcedures;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PlatinDashboard.Infra.Data.Farmacia.StoredProcedures
{
    public class VendasClassificacaoLojaTotalStoredProcedure : StoredProcedureBase,
        IVendasClassificacaoLojaTotalStoredProcedure
    {
        public IEnumerable<VendaClassificacaoLojaTotalSp> Buscar(string my, string connectionString)
        {
            var dataTable = ExecuteProcedure("vendas_classificacao_loja_total", new object[] { "pmyr", my }, connectionString);
            var vendas = new List<VendaClassificacaoLojaTotalSp>();
            foreach (DataRow row in dataTable.Rows)
            {
                vendas.Add(new VendaClassificacaoLojaTotalSp(row));
            }
            return vendas;
        }

        public IEnumerable<VendaClassificacaoLojaTotalSp> Buscar(string my, int[] uads, string connectionString)
        {
            var filter = $"WHERE uad in ({FormatUads(uads)})";
            var dataTable = ExecuteProcedure("vendas_classificacao_loja_total", new object[] { "pmyr", my }, filter, connectionString);
            var vendas = new List<VendaClassificacaoLojaTotalSp>();
            foreach (DataRow row in dataTable.Rows)
            {
                vendas.Add(new VendaClassificacaoLojaTotalSp(row));
            }
            return vendas;
        }

        public IEnumerable<VendaClassificacaoLojaTotalSp> BuscarPorMyEUad(string my, int uad, string connectionString)
        {
            var filter = $"WHERE uad = {uad}";
            var dataTable = ExecuteProcedure("vendas_classificacao_loja_total", new object[] { "pmyr", my }, filter, connectionString);
            var vendas = new List<VendaClassificacaoLojaTotalSp>();
            foreach (DataRow row in dataTable.Rows)
            {
                vendas.Add(new VendaClassificacaoLojaTotalSp(row));
            }
            return vendas;
        }

        public IEnumerable<VendaClassificacaoLojaTotalSp> BuscarPorMyEUad(string my, int uad, int[] uads, string connectionString)
        {
            var filter = $"WHERE uad = {uad} AND uad in ({FormatUads(uads)})";
            var dataTable = ExecuteProcedure("vendas_classificacao_loja_total", new object[] { "pmyr", my }, filter, connectionString);
            var vendas = new List<VendaClassificacaoLojaTotalSp>();
            foreach (DataRow row in dataTable.Rows)
            {
                vendas.Add(new VendaClassificacaoLojaTotalSp(row));
            }
            return vendas;
        }        
    }
}
