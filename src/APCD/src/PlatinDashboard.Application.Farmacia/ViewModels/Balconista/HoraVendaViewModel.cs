namespace PlatinDashboard.Application.Farmacia.ViewModels.Balconista
{
    public class HoraVendaViewModel
    {
        public string Hora { get; set; }
        public decimal Valor { get; set; }
        public decimal TicketMedio { get; set; }
        public int ClientesAtendidos { get; set; }

        public HoraVendaViewModel()
        {
            Valor = 0;
        }
    }
}
