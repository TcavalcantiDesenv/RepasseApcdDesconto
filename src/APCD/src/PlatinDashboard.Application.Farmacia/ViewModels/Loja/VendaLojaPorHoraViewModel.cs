using PlatinDashboard.Application.Farmacia.ViewModels.Balconista;
using System.Collections.Generic;

namespace PlatinDashboard.Application.Farmacia.ViewModels.Loja
{
    public class VendaLojaPorHoraViewModel
    {
        public int Loja { get; set; }
        public string My { get; set; }
        public decimal? Valor { get; set; }
        public int ClientesAtendidos { get; set; }
        public int Hora { get; set; }
        public decimal? Desconto { get; set; }
        public decimal? Devolucao { get; set; }

        //Propriedades necessária para o gráfico
        public string Nome { get; set; }
        public decimal ValorTotal { get; set; }
        public List<HoraVendaViewModel> HorasVendaViewModels { get; set; }

        public VendaLojaPorHoraViewModel()
        {
            HorasVendaViewModels = new List<HoraVendaViewModel>();
        }
    }
}
