using PlatinDashboard.Application.Farmacia.ViewModels.Loja;
using PlatinDashboard.Domain.Farmacia.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq.Expressions;

namespace PlatinDashboard.Application.Farmacia.Interfaces
{
    public interface IGraficoWebAppService
    {
        GraficoWebViewModel GetById(int id, DbConnection dbConnection);
        IEnumerable<GraficoWebViewModel> GetAll(DbConnection dbConnection);
        IEnumerable<GraficoWebViewModel> GetByFilter(Expression<Func<GraficoWeb, bool>> consulta, DbConnection dbConnection);
    }
}
