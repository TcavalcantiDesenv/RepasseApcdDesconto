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
    public class VendasLojaPorDiaHoraAppService : IVendasLojaPorDiaHoraAppService
    {
        private readonly IVendasLojaPorDiaHoraService _vendasLojaPorDiaHoraService;

        public VendasLojaPorDiaHoraAppService(IVendasLojaPorDiaHoraService vendasLojaPorDiaHoraService)
        {
            _vendasLojaPorDiaHoraService = vendasLojaPorDiaHoraService;
        }

        public IEnumerable<VendasLojaPorDiaHoraViewModel> GetByFilter(Expression<Func<VendasLojaPorDiaHora, bool>> consulta, DbConnection dbConnection)
        {
            return Mapper.Map<IEnumerable<VendasLojaPorDiaHora>, IEnumerable<VendasLojaPorDiaHoraViewModel>>(_vendasLojaPorDiaHoraService.GetByFilter(consulta, dbConnection));
        }
    }
}
