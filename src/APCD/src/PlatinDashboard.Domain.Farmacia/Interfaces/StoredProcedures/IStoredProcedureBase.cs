using System;
using System.Data;

namespace PlatinDashboard.Domain.Farmacia.Interfaces.StoredProcedures
{
    public interface IStoredProcedureBase
    {
        DataTable ExecuteProcedure(string procedureName, Object[] parameterList, string connectionString);
        DataTable ExecuteProcedure(string procedureName, Object[] parameterList, string filter, string connectionString);
    }
}
