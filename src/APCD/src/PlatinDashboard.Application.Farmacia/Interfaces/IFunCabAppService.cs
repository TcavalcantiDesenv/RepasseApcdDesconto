using PlatinDashboard.Application.Farmacia.ViewModels.Balconista;
using PlatinDashboard.Domain.Farmacia.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq.Expressions;

namespace PlatinDashboard.Application.Farmacia.Interfaces
{
    public interface IFunCabAppService
    {
        IEnumerable<FunCabViewModel> GetByFilter(Expression<Func<FunCab, bool>> consulta, DbConnection dbConnection);
        IEnumerable<FunCabViewModel> GetAll(DbConnection dbConnection);
    }
}
