using System.Collections.Generic;

namespace PlatinDashboard.Application.Farmacia.ViewModels.Balconista
{
    public class VendaBalconistaPorHoraViewModel
    {
        public int Loja { get; set; }
        public string My { get; set; }        
        public int Balconista { get; set; }
        public int ClientesAtendidos { get; set; }
        public decimal? Valor { get; set; }
        public decimal? Desconto { get; set; }
        public decimal? Devolucao { get; set; }
        public int Hora { get; set; }

        //Propriedades necessárias para geração do gráfico
        public string Nome { get; set; }
        public decimal ValorTotal { get; set; }
        public List<HoraVendaViewModel> HorasVendaViewModels { get; set; }

        public VendaBalconistaPorHoraViewModel()
        {
            HorasVendaViewModels = new List<HoraVendaViewModel>();
        }
    }
}
