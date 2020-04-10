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
    public class FunCabAppService : IFunCabAppService

    {
        private readonly IFunCabService _funCabService;

        public FunCabAppService(IFunCabService funCabService)
        {
            _funCabService = funCabService;
        }

        public IEnumerable<FunCabViewModel> GetAll(DbConnection dbConnection)
        {
            return Mapper.Map<IEnumerable<FunCab>, IEnumerable<FunCabViewModel>>(_funCabService.GetAll(dbConnection));
        }

        public IEnumerable<FunCabViewModel> GetByFilter(Expression<Func<FunCab, bool>> consulta, DbConnection dbConnection)
        {
            return Mapper.Map<IEnumerable<FunCab>, IEnumerable<FunCabViewModel>>(_funCabService.GetByFilter(consulta, dbConnection));
        }
    }
}
