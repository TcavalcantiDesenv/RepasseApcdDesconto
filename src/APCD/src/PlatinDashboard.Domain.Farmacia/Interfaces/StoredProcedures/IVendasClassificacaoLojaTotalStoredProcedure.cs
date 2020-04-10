using System.Collections.Generic;
using PlatinDashboard.Domain.Farmacia.Entities;

namespace PlatinDashboard.Domain.Farmacia.Interfaces.StoredProcedures
{
    public interface IVendasClassificacaoLojaTotalStoredProcedure
    {
        IEnumerable<VendaClassificacaoLojaTotalSp> Buscar(string my, string connectionString);
        IEnumerable<VendaClassificacaoLojaTotalSp> Buscar(string my, int[] uads, string connectionString);
        IEnumerable<VendaClassificacaoLojaTotalSp> BuscarPorMyEUad(string my, int uad, string connectionString);
        IEnumerable<VendaClassificacaoLojaTotalSp> BuscarPorMyEUad(string my, int uad, int[] uads,string connectionString);
    }
}
