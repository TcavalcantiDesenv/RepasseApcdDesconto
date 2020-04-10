using PlatinDashboard.Application.Farmacia.ViewModels.Loja;
using PlatinDashboard.Domain.Farmacia.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq.Expressions;

namespace PlatinDashboard.Application.Farmacia.Interfaces
{
    public interface IVendaLojaPorMesAppService
    {
        IEnumerable<VendaLojaPorMesViewModel> GetByFilter(Expression<Func<VendaLojaPorMes, bool>> consulta, DbConnection dbConnection);
    }
}
