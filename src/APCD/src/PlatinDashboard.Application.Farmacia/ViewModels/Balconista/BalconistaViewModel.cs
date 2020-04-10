using System.Collections.Generic;

namespace PlatinDashboard.Application.Farmacia.ViewModels.Balconista
{
    public class BalconistaViewModel
    {
        public int BalconistaId { get; set; }
        public string Nome { get; set; }
        public decimal ValorTotal { get; set; }
        public List<BalconistaVendaViewModel> BalconistaVendaViewModels { get; set; }

        public BalconistaViewModel()
        {
            BalconistaVendaViewModels = new List<BalconistaVendaViewModel>();
        }
    }
}
