using PlatinDashboard.Domain.Farmacia.Entities;
using PlatinDashboard.Domain.Farmacia.Interfaces.Repositories;
using PlatinDashboard.Domain.Farmacia.Interfaces.Services;

namespace PlatinDashboard.Domain.Farmacia.Services
{
    public class ClsVenAllGraficoTotalService : ServiceBase<ClsVenAllGraficoTotal>, IClsVenAllGraficoTotalService
    {
        private readonly IClsVenAllGraficoTotalRepository _clsVenAllGraficoTotalRepository;

        public ClsVenAllGraficoTotalService(IClsVenAllGraficoTotalRepository clsVenAllGraficoTotalRepository)
            :base(clsVenAllGraficoTotalRepository)
        {
            _clsVenAllGraficoTotalRepository = clsVenAllGraficoTotalRepository;
        }
    }
}
