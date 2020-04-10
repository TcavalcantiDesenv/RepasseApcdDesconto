using System;

namespace PlatinDashboard.Domain.Farmacia.Entities
{
    public class ClsVenAllGraficoPorCls
    {
        public DateTime Dias { get; set; }
        public int Cls { get; set; }
        public int Uad { get; set; }
        public string Mes { get; set; }
        public decimal ValorBruto { get; set; }
        public decimal Meta { get; set; }
        public decimal Percentual { get; set; }
    }
}
