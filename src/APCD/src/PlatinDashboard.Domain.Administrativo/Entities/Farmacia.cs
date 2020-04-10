namespace PlatinDashboard.Domain.Administrativo.Entities
{
    public class Farmacia
    {
        public int Id { get; set; }
        public int CodigoCliente { get; set; }
        public string NomeFantasia { get; set; }
        public string RazaoSocial { get; set; }
        public string Pessoa { get; set; }
        public string Cnpj { get; set; }
        public string InscricaoEstadual { get; set; }
        public string Tipo { get; set; }
        public string Plano { get; set; }

        public Farmacia()
        {

        }

        public Farmacia(Cliente cliente)
        {
            Id = cliente.Id;
            CodigoCliente = cliente.CodigoCliente;
            NomeFantasia = cliente.NomeFantasia;
            RazaoSocial = cliente.RazaoSocial;
            Pessoa = cliente.Pessoa;
            Cnpj = cliente.Cnpj;
            InscricaoEstadual = cliente.InscricaoEstadual;
            Tipo = cliente.Tipo;
            Plano = cliente.Plano;
        }
    }
}
