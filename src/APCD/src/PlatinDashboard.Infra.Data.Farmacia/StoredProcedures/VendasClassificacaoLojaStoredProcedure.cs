using PlatinDashboard.Domain.Farmacia.Entities;
using PlatinDashboard.Domain.Farmacia.Interfaces.StoredProcedures;
using System.Collections.Generic;
using System.Data;

namespace PlatinDashboard.Infra.Data.Farmacia.StoredProcedures
{
    public class VendasClassificacaoLojaStoredProcedure : StoredProcedureBase,
        IVendasClassificacaoLojaStoredProcedure
    {
        public IEnumerable<VendaClassificacaoLojaSp> Buscar(string my, string connectionString)
        {
            var dataTable = ExecuteProcedure("vendas_classificacao_loja", new object[] { "pmyr", my }, connectionString);
            var vendas = new List<VendaClassificacaoLojaSp>();
            foreach (DataRow row in dataTable.Rows)
            {
                vendas.Add(new VendaClassificacaoLojaSp(row));
            }
            return vendas;
        }

        public IEnumerable<VendaClassificacaoLojaSp> Buscar(string my, int[] uads, string connectionString)
        {
            var filter = $"WHERE uad in ({FormatUads(uads)})";
            var dataTable = ExecuteProcedure("vendas_classificacao_loja", new object[] { "pmyr", my }, filter, connectionString);
            var vendas = new List<VendaClassificacaoLojaSp>();
            foreach (DataRow row in dataTable.Rows)
            {
                vendas.Add(new VendaClassificacaoLojaSp(row));
            }
            return vendas;
        }

        public IEnumerable<VendaClassificacaoLojaSp> Buscar(string my, int uad, int cls, string connectionString)
        {
            var filter = $"WHERE uad = {uad} and cls = {cls}";
            var dataTable = ExecuteProcedure("vendas_classificacao_loja", new object[] { "pmyr", my }, filter, connectionString);
            var vendas = new List<VendaClassificacaoLojaSp>();
            foreach (DataRow row in dataTable.Rows)
            {
                vendas.Add(new VendaClassificacaoLojaSp(row));
            }
            return vendas;
        }

        public IEnumerable<VendaClassificacaoLojaSp> Buscar(string my, int uad, int cls, int[] uads, string connectionString)
        {
            var filter = $"WHERE uad = {uad} and cls = {cls} and uad in ({FormatUads(uads)})";
            var dataTable = ExecuteProcedure("vendas_classificacao_loja", new object[] { "pmyr", my }, filter, connectionString);
            var vendas = new List<VendaClassificacaoLojaSp>();
            foreach (DataRow row in dataTable.Rows)
            {
                vendas.Add(new VendaClassificacaoLojaSp(row));
            }
            return vendas;
        }

        public IEnumerable<VendaClassificacaoLojaSp> BuscarPorMyECls(string my, int cls, string connectionString)
        {
            var filter = $"WHERE cls = {cls}";
            var dataTable = ExecuteProcedure("vendas_classificacao_loja", new object[] { "pmyr", my }, filter, connectionString);
            var vendas = new List<VendaClassificacaoLojaSp>();
            foreach (DataRow row in dataTable.Rows)
            {
                vendas.Add(new VendaClassificacaoLojaSp(row));
            }
            return vendas;
        }

        public IEnumerable<VendaClassificacaoLojaSp> BuscarPorMyECls(string my, int cls, int[] uads, string connectionString)
        {
            var filter = $"WHERE cls = {cls} AND uad in ({FormatUads(uads)})";
            var dataTable = ExecuteProcedure("vendas_classificacao_loja", new object[] { "pmyr", my }, filter, connectionString);
            var vendas = new List<VendaClassificacaoLojaSp>();
            foreach (DataRow row in dataTable.Rows)
            {
                vendas.Add(new VendaClassificacaoLojaSp(row));
            }
            return vendas;
        }

        public IEnumerable<VendaClassificacaoLojaSp> BuscarPorMyEUad(string my, int uad, string connectionString)
        {
            var filter = $"WHERE uad = {uad}";
            var dataTable = ExecuteProcedure("vendas_classificacao_loja", new object[] { "pmyr", my }, filter, connectionString);
            var vendas = new List<VendaClassificacaoLojaSp>();
            foreach (DataRow row in dataTable.Rows)
            {
                vendas.Add(new VendaClassificacaoLojaSp(row));
            }
            return vendas;
        }
    }
}
