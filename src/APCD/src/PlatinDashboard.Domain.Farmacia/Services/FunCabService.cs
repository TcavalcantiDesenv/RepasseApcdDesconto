using PlatinDashboard.Domain.Farmacia.Entities;
using PlatinDashboard.Domain.Farmacia.Interfaces.Repositories;
using PlatinDashboard.Domain.Farmacia.Interfaces.Services;

namespace PlatinDashboard.Domain.Farmacia.Services
{
    public class FunCabService : ServiceBase<FunCab>, IFunCabService
    {
        private readonly IFunCabRepository _funCabRepository;

        public FunCabService(IFunCabRepository funCabRepository) : base(funCabRepository)
        {
            _funCabRepository = funCabRepository;
        }
    }
}
