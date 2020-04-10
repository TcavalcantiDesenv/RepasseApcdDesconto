namespace PlatinDashboard.Domain.Farmacia.Entities
{
    public class VendaLojaPorMes
    {
        public short? Uad { get; set; }
        public string AnoMes { get; set; }
        public decimal? Bruto { get; set; }
        public decimal? Desconto { get; set; }
        public decimal? Custo { get; set; }
        public decimal? Devolucao { get; set; }
        public decimal? Vdc { get; set; }
        public decimal? ClientesAtendidos { get; set; }
        public decimal? TicketMedio { get; set; }
        public decimal? Vme { get; set; }
        public int? ItensVendidos { get; set; }
        public int? UnidadesVendidas { get; set; }
        public decimal? Cvl { get; set; }
        public decimal? Vmm { get; set; }        
    }
}
