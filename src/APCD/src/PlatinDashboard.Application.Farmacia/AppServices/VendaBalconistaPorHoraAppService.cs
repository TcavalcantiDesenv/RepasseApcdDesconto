using AutoMapper;
using PlatinDashboard.Application.Farmacia.Interfaces;
using PlatinDashboard.Application.Farmacia.ViewModels.Balconista;
using PlatinDashboard.Domain.Farmacia.Entities;
using PlatinDashboard.Domain.Farmacia.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq.Expressions;

namespace PlatinDashboard.Application.Farmacia.AppServices
{
    public class VendaBalconistaPorHoraAppService : IVendaBalconistaPorHoraAppService
    {
        private readonly IVendaBalconistaPorHoraService _vendaBalconistaPorHoraService;

        public VendaBalconistaPorHoraAppService(IVendaBalconistaPorHoraService vendaBalconistaPorHoraService)
        {
            _vendaBalconistaPorHoraService = vendaBalconistaPorHoraService;
        }

        public IEnumerable<VendaBalconistaPorHoraViewModel> GetByFilter(Expression<Func<VendaBalconistaPorHora, bool>> consulta, DbConnection dbConnection)
        {
            return Mapper.Map<IEnumerable<VendaBalconistaPorHora>, IEnumerable<VendaBalconistaPorHoraViewModel>>(_vendaBalconistaPorHoraService.GetByFilter(consulta, dbConnection));
        }
    }
}
