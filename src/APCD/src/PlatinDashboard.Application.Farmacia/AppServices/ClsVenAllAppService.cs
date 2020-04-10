using AutoMapper;
using PlatinDashboard.Application.Farmacia.Interfaces;
using PlatinDashboard.Application.Farmacia.ViewModels.Classificacao;
using PlatinDashboard.Domain.Farmacia.Entities;
using PlatinDashboard.Domain.Farmacia.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq.Expressions;

namespace PlatinDashboard.Application.Farmacia.AppServices
{
    public class ClsVenAllAppService : IClsVenAllAppService
    {
        private readonly IClsVenAllService _clsVenAllService;

        public ClsVenAllAppService(IClsVenAllService clsVenAllService)
        {
            _clsVenAllService = clsVenAllService;
        }

        public IEnumerable<ClsVenAllViewModel> GetAll(DbConnection dbConnection)
        {
            return Mapper.Map<IEnumerable<ClsVenAll>, IEnumerable<ClsVenAllViewModel>>(_clsVenAllService.GetAll(dbConnection));
        }

        public IEnumerable<ClsVenAllViewModel> GetByFilter(Expression<Func<ClsVenAll, bool>> consulta, DbConnection dbConnection)
        {
            return Mapper.Map<IEnumerable<ClsVenAll>, IEnumerable<ClsVenAllViewModel>>(_clsVenAllService.GetByFilter(consulta ,dbConnection));
        }

        public ClsVenAllViewModel GetById(int id, DbConnection dbConnection)
        {
            return Mapper.Map<ClsVenAll, ClsVenAllViewModel>(_clsVenAllService.GetById(id, dbConnection));
        }
    }
}
