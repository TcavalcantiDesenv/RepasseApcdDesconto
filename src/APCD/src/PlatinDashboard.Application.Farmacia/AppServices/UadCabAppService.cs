using AutoMapper;
using PlatinDashboard.Application.Farmacia.Interfaces;
using PlatinDashboard.Application.Farmacia.ViewModels.Loja;
using PlatinDashboard.Domain.Farmacia.Entities;
using PlatinDashboard.Domain.Farmacia.Interfaces.Services;
using System.Collections.Generic;
using System.Data.Common;

namespace PlatinDashboard.Application.Farmacia.AppServices
{
    public class UadCabAppService : IUadCabAppService
    {
        private readonly IUadCabService _uadCabService;

        public UadCabAppService(IUadCabService uadCabService)
        {
            _uadCabService = uadCabService;
        }

        public IEnumerable<UadCabViewModel> GetAll(DbConnection dbConnection)
        {
            return Mapper.Map<IEnumerable<UadCab>, IEnumerable<UadCabViewModel>>(_uadCabService.GetAll(dbConnection));
        }

        public UadCabViewModel GetById(int id, DbConnection dbConnection)
        {
            return Mapper.Map<UadCab, UadCabViewModel>(_uadCabService.GetById(id, dbConnection));
        }

        public IEnumerable<UadCabViewModel> GetStores(DbConnection dbConnection)
        {
            return Mapper.Map<IEnumerable<UadCab>, IEnumerable<UadCabViewModel>>(_uadCabService.GetStores(dbConnection));
        }
    }
}
