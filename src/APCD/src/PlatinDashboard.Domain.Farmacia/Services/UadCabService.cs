using System.Collections.Generic;
using System.Data.Common;
using PlatinDashboard.Domain.Farmacia.Entities;
using PlatinDashboard.Domain.Farmacia.Interfaces.Repositories;
using PlatinDashboard.Domain.Farmacia.Interfaces.Services;

namespace PlatinDashboard.Domain.Farmacia.Services
{
    public class UadCabService : ServiceBase<UadCab>, IUadCabService
    {
        private readonly IUadCabRepository _uadCabRepository;

        public UadCabService(IUadCabRepository uadCabRepository) : base(uadCabRepository)
        {
            _uadCabRepository = uadCabRepository;
        }

        public IEnumerable<UadCab> GetStores(DbConnection dbConnection)
        {
            return _uadCabRepository.GetStores(dbConnection);
        }
    }
}
