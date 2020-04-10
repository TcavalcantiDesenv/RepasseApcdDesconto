using System;

namespace PlatinDashboard.Application.Farmacia.ViewModels.Loja
{
    public class GraficoWebViewModel
    {
        public DateTime? Dat { get; set; }
        public string My { get; set; }
        public int? TotaldeLojas { get; set; }
        public decimal? Lucro { get; set; }
        public decimal? VendaBruta { get; set; }
        public decimal? Liquida { get; set; }
        public decimal? Desconto { get; set; }    
        public decimal? PercentualMargem { get; set; }
        public decimal? QtMediaClientes { get; set; }
        public decimal? ClientesAtendidos { get; set; }
        public decimal? Custo { get; set; }
        public decimal? Devolucao { get; set; }
    }
}
