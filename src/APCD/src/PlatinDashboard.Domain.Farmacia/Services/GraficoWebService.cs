using PlatinDashboard.Domain.Farmacia.Entities;
using PlatinDashboard.Domain.Farmacia.Interfaces.Repositories;
using PlatinDashboard.Domain.Farmacia.Interfaces.Services;

namespace PlatinDashboard.Domain.Farmacia.Services
{
    public class GraficoWebService : ServiceBase<GraficoWeb>, IGraficoWebService
    {
        private readonly IGraficoWebRepository _graficoWebRepository;

        public GraficoWebService(IGraficoWebRepository graficoWebRepository) : base(graficoWebRepository)
        {
            _graficoWebRepository = graficoWebRepository;
        }
    }
}
