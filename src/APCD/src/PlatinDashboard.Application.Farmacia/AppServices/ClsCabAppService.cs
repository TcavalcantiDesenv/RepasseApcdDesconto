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
    public class ClsCabAppService : IClsCabAppService
    {
        private readonly IClsCabService _clsCabService;

        public ClsCabAppService(IClsCabService clsCabService)
        {
            _clsCabService = clsCabService;
        }

        public IEnumerable<ClsCabViewModel> GetByFilter(Expression<Func<ClsCab, bool>> consulta, DbConnection dbConnection)
        {
            return Mapper.Map<IEnumerable<ClsCab>, IEnumerable<ClsCabViewModel>>(_clsCabService.GetByFilter(consulta, dbConnection));
        }
    }
}
