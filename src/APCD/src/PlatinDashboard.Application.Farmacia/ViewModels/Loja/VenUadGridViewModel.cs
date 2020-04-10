using System;

namespace PlatinDashboard.Application.Farmacia.ViewModels.Loja
{
    public class VenUadGridViewModel
    {
        public DateTime Data { get; set; }
        public string Loja { get; set; }
        public decimal Bruto { get; set; }
        public decimal Desconto { get; set; }
        public decimal Devolucao { get; set; }
        public decimal Liquida { get; set; }
        public decimal Lucro { get; set; }
        public decimal PercentualMargem { get; set; }
        public decimal TicketMedio { get; set; }
        public decimal QtMediaClientes { get; set; }
        public decimal ClientesAtendidos { get; set; }
        public decimal TotalLojas { get; set; }
        public decimal Custo { get; set; }
    }
}
