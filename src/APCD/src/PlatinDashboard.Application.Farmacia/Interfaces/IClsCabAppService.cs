using PlatinDashboard.Application.Farmacia.ViewModels.Classificacao;
using PlatinDashboard.Domain.Farmacia.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq.Expressions;

namespace PlatinDashboard.Application.Farmacia.Interfaces
{
    public interface IClsCabAppService
    {
        IEnumerable<ClsCabViewModel> GetByFilter(Expression<Func<ClsCab, bool>> consulta, DbConnection dbConnection);
    }
}
