using System;

namespace PlatinDashboard.Domain.Farmacia.Entities
{
    public class VenUadCls
    {
        public DateTime Dat { get; set; }
        public short Uad { get; set; }
        public short Cls { get; set; }
        public string My { get; set; }             
        public decimal Vlb { get; set; }
        public decimal Vld { get; set; }
        public decimal Vlc { get; set; }
        public decimal Vde { get; set; }
        public decimal Vdc { get; set; }
        public decimal Reg { get; set; }
        public decimal? Tme { get; set; }
        public decimal? Vme { get; set; }
        public int? Ite { get; set; }
        public int? Qtp { get; set; }
    }
}
