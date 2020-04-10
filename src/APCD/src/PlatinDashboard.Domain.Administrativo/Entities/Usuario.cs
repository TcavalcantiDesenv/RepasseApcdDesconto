namespace PlatinDashboard.Domain.Administrativo.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public int CodigoCliente { get; set; }
        public string NomeFantasia { get; set; }
        public string RazaoSocial { get; set; }
        public string Tipo { get; set; }        
        public string Pessoa { get; set; }        
        public string Plano { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Perfil { get; set; }
        public string Situacao { get; set; }

        public Usuario()
        {

        }

        public Usuario(Cliente cliente)
        {
            Id = cliente.Id;
            CodigoCliente = cliente.CodigoCliente;
            NomeFantasia = cliente.NomeFantasia;
            RazaoSocial = cliente.RazaoSocial;
            Tipo = cliente.Tipo;
            Pessoa = cliente.Pessoa;
            Plano = cliente.Plano;
            Email = cliente.Email;
            Login = cliente.Login;
            Senha = cliente.Senha;
            Perfil = cliente.Perfil;
            Situacao = cliente.Situacao;
        }
    }
}
