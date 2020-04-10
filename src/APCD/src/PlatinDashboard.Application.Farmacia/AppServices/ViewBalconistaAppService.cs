using AutoMapper;
using PlatinDashboard.Application.Farmacia.Interfaces;
using PlatinDashboard.Application.Farmacia.ViewModels.Balconista;
using PlatinDashboard.Domain.Farmacia.Entities;
using PlatinDashboard.Domain.Farmacia.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace PlatinDashboard.Application.Farmacia.AppServices
{
    public class ViewBalconistaAppService : IViewBalconistaAppService
    {
        private readonly IViewBalconistaService _viewBalconistaService;

        public ViewBalconistaAppService(IViewBalconistaService viewBalconistaService)
        {
            _viewBalconistaService = viewBalconistaService;
        }

        public List<ViewBalconistaViewModel> BuscarPorLojaEPeriodo(int lojaId, string periodo, DbConnection connection)
        {
            return Mapper.Map<IEnumerable<ViewBalconista>, IEnumerable<ViewBalconistaViewModel>>(_viewBalconistaService.BuscarPorLojaEPeriodo(lojaId, periodo, connection)).ToList();
        }

        public IEnumerable<ViewBalconistaViewModel> GetByFilter(Expression<Func<ViewBalconista, bool>> consulta, DbConnection dbConnection)
        {
            return Mapper.Map<IEnumerable<ViewBalconista>, IEnumerable<ViewBalconistaViewModel>>(_viewBalconistaService.GetByFilter(consulta, dbConnection));
        }
    }
}
