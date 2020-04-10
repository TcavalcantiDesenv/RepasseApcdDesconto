using PlatinDashboard.Application.Farmacia.ViewModels.Balconista;
using PlatinDashboard.Domain.Farmacia.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq.Expressions;

namespace PlatinDashboard.Application.Farmacia.Interfaces
{
    public interface IVendaBalconistaPorHoraAppService
    {
        IEnumerable<VendaBalconistaPorHoraViewModel> GetByFilter(Expression<Func<VendaBalconistaPorHora, bool>> consulta, DbConnection dbConnection);
    }
}
