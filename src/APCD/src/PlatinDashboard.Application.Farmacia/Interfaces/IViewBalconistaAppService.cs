using PlatinDashboard.Application.Farmacia.ViewModels.Balconista;
using PlatinDashboard.Domain.Farmacia.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq.Expressions;

namespace PlatinDashboard.Application.Farmacia.Interfaces
{
    public interface IViewBalconistaAppService
    {
        List<ViewBalconistaViewModel> BuscarPorLojaEPeriodo(int lojaId, string periodo, DbConnection connection);
        IEnumerable<ViewBalconistaViewModel> GetByFilter(Expression<Func<ViewBalconista, bool>> consulta, DbConnection dbConnection);
    }
}
