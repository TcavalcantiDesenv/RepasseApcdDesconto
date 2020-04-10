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
    public class VendaLojaPorMesAppService : IVendaLojaPorMesAppService
    {
        private readonly IVendaLojaPorMesService _vendaLojaPorMesService;

        public VendaLojaPorMesAppService(IVendaLojaPorMesService vendaLojaPorMesService)
        {
            _vendaLojaPorMesService = vendaLojaPorMesService;
        }

        public IEnumerable<VendaLojaPorMesViewModel> GetByFilter(Expression<Func<VendaLojaPorMes, bool>> consulta, DbConnection dbConnection)
        {
            return Mapper.Map<IEnumerable<VendaLojaPorMes>, IEnumerable<VendaLojaPorMesViewModel>>(_vendaLojaPorMesService.GetByFilter(consulta, dbConnection));
        }
    }
}
