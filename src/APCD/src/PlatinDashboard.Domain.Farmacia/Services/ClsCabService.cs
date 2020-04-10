using PlatinDashboard.Domain.Farmacia.Entities;
using PlatinDashboard.Domain.Farmacia.Interfaces.Repositories;
using PlatinDashboard.Domain.Farmacia.Interfaces.Services;

namespace PlatinDashboard.Domain.Farmacia.Services
{
    public class ClsCabService : ServiceBase<ClsCab>, IClsCabService
    {
        private readonly IClsCabRepository _clsCabRepository;

        public ClsCabService(IClsCabRepository clsCabRepository) : base(clsCabRepository)
        {
            _clsCabRepository = clsCabRepository;
        }
    }
}
