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
    public class GraficoWebAppService : IGraficoWebAppService
    {
        private readonly IGraficoWebService _graficoWebService;

        public GraficoWebAppService(IGraficoWebService graficoWebService)
        {
            _graficoWebService = graficoWebService;
        }

        public IEnumerable<GraficoWebViewModel> GetAll(DbConnection dbConnection)
        {
            return Mapper.Map<IEnumerable<GraficoWeb>, IEnumerable<GraficoWebViewModel>>(_graficoWebService.GetAll(dbConnection));
        }

        public IEnumerable<GraficoWebViewModel> GetByFilter(Expression<Func<GraficoWeb, bool>> consulta, DbConnection dbConnection)
        {
            return Mapper.Map<IEnumerable<GraficoWeb>, IEnumerable<GraficoWebViewModel>>(_graficoWebService.GetByFilter(consulta, dbConnection));
        }

        public GraficoWebViewModel GetById(int id, DbConnection dbConnection)
        {
            return Mapper.Map<GraficoWeb, GraficoWebViewModel>(_graficoWebService.GetById(id, dbConnection));
        }
    }
}
