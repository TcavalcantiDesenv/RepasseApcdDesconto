using PlatinDashboard.Domain.Farmacia.Entities;
using PlatinDashboard.Domain.Farmacia.Interfaces.Repositories;
using PlatinDashboard.Domain.Farmacia.Interfaces.Services;
using System.Collections.Generic;
using System.Data.Common;

namespace PlatinDashboard.Domain.Farmacia.Services
{
    public class ViewBalconistaService : ServiceBase<ViewBalconista>, IViewBalconistaService
    {
        private readonly IViewBalconistaRepository _viewBalconistaRepository;

        public ViewBalconistaService(IViewBalconistaRepository viewBalconistaRepository)
            :base(viewBalconistaRepository)
        {
            _viewBalconistaRepository = viewBalconistaRepository;
        }

        public List<ViewBalconista> BuscarPorLojaEPeriodo(int lojaId, string periodo, DbConnection connection)
        {
            return _viewBalconistaRepository.BuscarPorLojaEPeriodo(lojaId, periodo, connection);
        }
    }
}
