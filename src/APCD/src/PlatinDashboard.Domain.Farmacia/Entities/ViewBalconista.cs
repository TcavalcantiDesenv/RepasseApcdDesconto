namespace PlatinDashboard.Domain.Farmacia.Entities
{
    public class ViewBalconista
    {
        public string MesAno { get; set; }
        public int IdBalconista { get; set; }
        public int Classificacao { get; set; }
        public decimal Valor { get; set; }
        public int IdLoja { get; set; }
    }
}
