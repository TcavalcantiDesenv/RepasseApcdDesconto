namespace PlatinDashboard.Domain.Administrativo.Entities
{
    public class Rede
    {
        public int Id { get; set; }
        public int CodigoCliente { get; set; }        
        public string NomeFantasia { get; set; }
        public string Pessoa { get; set; }
        public string Situacao { get; set; }

        public Rede()
        {

        }

        public Rede(Cliente cliente)
        {
            Id = cliente.Id;
            CodigoCliente = cliente.CodigoCliente;
            NomeFantasia = cliente.NomeFantasia;
            Pessoa = cliente.Pessoa;
            Situacao = cliente.Situacao;
        }
    }
}
