using System;

namespace PlatinDashboard.Domain.Administrativo.Entities
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public int CodigoCliente { get; set; }
        public int LojaCliente { get; set; }
        public string Pessoa { get; set; }
        public string RazaoSocial { get; set; }
        public string Cnpj { get; set; }
        public string InscricaoEstadual { get; set; }
        public string NomeFantasia { get; set; }
        public string Responsavel { get; set; }
        public string Responsavel2 { get; set; }
        public string Responsavel3 { get; set; }
        public string Responsavel4 { get; set; }
        public string Cpf { get; set; }
        public string Telefone { get; set; }
        public string Telefone2 { get; set; }
        public string Telefone3 { get; set; }
        public string Telefone4 { get; set; }
        public string Email { get; set; }
        public string Email2 { get; set; }
        public string Email3 { get; set; }
        public string Email4 { get; set; }
        public string Cep { get; set; }
        public string Endereco { get; set; }
        public string EnderecoNumero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Observacao { get; set; }
        public string Observacao2 { get; set; }
        public string Situacao { get; set; }
        public string Plano { get; set; }
        public int QuantidadeLojas { get; set; }
        public string MalaDireta { get; set; }
        public string BloqueioAutomatico { get; set; }
        public string Perfil { get; set; }
        public string SituacaoLoja { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataSenha { get; set; }
        public string IpUltimoAcesso { get; set; }
        public DateTime DataUltimoAcesso { get; set; }
        public int ChamadoAvaliado { get; set; }
        public int ChamadoAvaliadoTotal { get; set; }
        public int ChamadoAvaliadoMedia { get; set; }
        public int VisitaAvaliado { get; set; }
        public int VisitaAvaliadoTotal { get; set; }
        public int VisitaAvaliadoMedia { get; set; }
        public int Avaliado { get; set; }
        public int AvaliadoTotal { get; set; }
        public int AvaliadoMedia { get; set; }
    }
}
