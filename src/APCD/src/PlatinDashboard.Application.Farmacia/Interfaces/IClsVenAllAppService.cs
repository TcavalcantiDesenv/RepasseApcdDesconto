using PlatinDashboard.Application.Farmacia.ViewModels.Classificacao;
using PlatinDashboard.Domain.Farmacia.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq.Expressions;

namespace PlatinDashboard.Application.Farmacia.Interfaces
{
    public interface IClsVenAllAppService
    {
        ClsVenAllViewModel GetById(int id, DbConnection dbConnection);
        IEnumerable<ClsVenAllViewModel> GetAll(DbConnection dbConnection);
        IEnumerable<ClsVenAllViewModel> GetByFilter(Expression<Func<ClsVenAll, bool>> consulta, DbConnection dbConnection);
    }
}
