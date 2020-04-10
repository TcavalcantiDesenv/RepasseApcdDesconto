using System;

namespace PlatinDashboard.Domain.Farmacia.Entities
{
    public class UadVenAllGraficoPorCls
    {
        public DateTime Dat { get; set; }
        public int Uad { get; set; }
        public int Cls { get; set; }
        public string Mes { get; set; }
        public decimal ValorBruto { get; set; }
        public decimal Meta { get; set; }
        public decimal Percentual { get; set; }
    }
}
