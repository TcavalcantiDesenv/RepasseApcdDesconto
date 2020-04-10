using System;

namespace PlatinDashboard.Domain.Farmacia.Entities
{
    public class BalVenAllGraficoPorCls
    {
        public DateTime Dias { get; set; }
        public int Bal { get; set; }
        public short Cls { get; set; }
        public string Mes { get; set; }
        public decimal ValorBruto { get; set; }
        public decimal? Meta { get; set; }
        public decimal Percentual { get; set; }
    }
}
