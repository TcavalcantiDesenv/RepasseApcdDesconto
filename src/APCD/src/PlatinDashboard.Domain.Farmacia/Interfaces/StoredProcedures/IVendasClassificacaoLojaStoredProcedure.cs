using PlatinDashboard.Domain.Farmacia.Entities;
using System.Collections.Generic;

namespace PlatinDashboard.Domain.Farmacia.Interfaces.StoredProcedures
{
    public interface IVendasClassificacaoLojaStoredProcedure : IStoredProcedureBase
    {
        IEnumerable<VendaClassificacaoLojaSp> Buscar(string my, string connectionString);
        IEnumerable<VendaClassificacaoLojaSp> Buscar(string my, int[] uads, string connectionString);
        IEnumerable<VendaClassificacaoLojaSp> BuscarPorMyEUad(string my, int uad, string connectionString);
        IEnumerable<VendaClassificacaoLojaSp> BuscarPorMyECls(string my, int cls, string connectionString);
        IEnumerable<VendaClassificacaoLojaSp> BuscarPorMyECls(string my, int cls, int[] uads, string connectionString);
        IEnumerable<VendaClassificacaoLojaSp> Buscar(string my, int uad, int cls, string connectionString);
        IEnumerable<VendaClassificacaoLojaSp> Buscar(string my, int uad, int cls, int[] uads, string connectionString);
    }
}
