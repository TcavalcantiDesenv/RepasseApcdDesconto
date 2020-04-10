using AutoMapper;
using PlatinDashboard.Application.Farmacia.Interfaces;
using PlatinDashboard.Application.Farmacia.ViewModels.Loja;
using PlatinDashboard.Domain.Farmacia.Entities;
using PlatinDashboard.Domain.Farmacia.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq.Expressions;

namespace PlatinDashboard.Application.Farmacia.AppServices
{
    public class VenUadAppService : IVenUadAppService
    {
        private readonly IVenUadService _venUadService;

        public VenUadAppService(IVenUadService venUadService)
        {
            _venUadService = venUadService;
        }

        public IEnumerable<VenUadViewModel> GetByFilter(Expression<Func<VenUad, bool>> consulta, DbConnection dbConnection)
        {
            return Mapper.Map<IEnumerable<VenUad>, IEnumerable<VenUadViewModel>>(_venUadService.GetByFilter(consulta, dbConnection));
        }
    }
}
