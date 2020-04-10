using System;

namespace PlatinDashboard.Domain.Farmacia.Entities
{
    public class BalVenAllGraficoTotal
    {
        public DateTime Dat { get; set; }
        public int Bal { get; set; }
        public string Mes { get; set; }
        public decimal ValorBruto { get; set; }
        public decimal Meta { get; set; }
        public decimal Percentual { get; set; }
    }
}
