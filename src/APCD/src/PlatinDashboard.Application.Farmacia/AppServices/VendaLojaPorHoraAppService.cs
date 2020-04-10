using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq.Expressions;
using AutoMapper;
using PlatinDashboard.Application.Farmacia.Interfaces;
using PlatinDashboard.Application.Farmacia.ViewModels.Loja;
using PlatinDashboard.Domain.Farmacia.Entities;
using PlatinDashboard.Domain.Farmacia.Interfaces.Services;

namespace PlatinDashboard.Application.Farmacia.AppServices
{
    public class VendaLojaPorHoraAppService : IVendaLojaPorHoraAppService
    {
        private readonly IVendaLojaPorHoraService _vendaLojaPorHoraService;

        public VendaLojaPorHoraAppService(IVendaLojaPorHoraService vendaLojaPorHoraService)
        {
            _vendaLojaPorHoraService = vendaLojaPorHoraService;
        }

        public IEnumerable<VendaLojaPorHoraViewModel> GetByFilter(Expression<Func<VendaLojaPorHora, bool>> consulta, DbConnection dbConnection)
        {
            return Mapper.Map<IEnumerable<VendaLojaPorHora>, IEnumerable<VendaLojaPorHoraViewModel>>(_vendaLojaPorHoraService.GetByFilter(consulta, dbConnection));
        }
    }
}
