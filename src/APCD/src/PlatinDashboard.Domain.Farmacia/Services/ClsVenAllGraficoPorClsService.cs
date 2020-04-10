using PlatinDashboard.Domain.Farmacia.Entities;
using PlatinDashboard.Domain.Farmacia.Interfaces.Repositories;
using PlatinDashboard.Domain.Farmacia.Interfaces.Services;

namespace PlatinDashboard.Domain.Farmacia.Services
{
    public class ClsVenAllGraficoPorClsService : ServiceBase<ClsVenAllGraficoPorCls>, IClsVenAllGraficoPorClsService
    {
        private readonly IClsVenAllGraficoPorClsRepository _clsVenAllGraficoPorClsRepository;

        public ClsVenAllGraficoPorClsService(IClsVenAllGraficoPorClsRepository clsVenAllGraficoPorClsRepository)
            : base(clsVenAllGraficoPorClsRepository)
        {
            _clsVenAllGraficoPorClsRepository = clsVenAllGraficoPorClsRepository;
        }
    }
}
