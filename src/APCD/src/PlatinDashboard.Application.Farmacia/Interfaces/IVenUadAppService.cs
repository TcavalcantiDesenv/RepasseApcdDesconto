﻿using PlatinDashboard.Application.Farmacia.ViewModels.Loja;
using PlatinDashboard.Domain.Farmacia.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq.Expressions;

namespace PlatinDashboard.Application.Farmacia.Interfaces
{
    public interface IVenUadAppService
    {
        IEnumerable<VenUadViewModel> GetByFilter(Expression<Func<VenUad, bool>> consulta, DbConnection dbConnection);
    }
}
