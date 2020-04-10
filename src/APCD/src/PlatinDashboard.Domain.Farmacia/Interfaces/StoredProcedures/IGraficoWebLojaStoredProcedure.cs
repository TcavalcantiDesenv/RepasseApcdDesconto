using PlatinDashboard.Domain.Farmacia.Entities;
using System.Collections.Generic;

namespace PlatinDashboard.Domain.Farmacia.Interfaces.StoredProcedures
{
    public interface IGraficoWebLojaStoredProcedure
    {
        IEnumerable<GraficoWeb> Buscar(string my, int[] uads, string connectionString, string provider);
    }
}
