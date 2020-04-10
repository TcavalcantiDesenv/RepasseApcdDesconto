using PlatinDashboard.Domain.Administrativo.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlatinDashboard.Infra.Data.Administrativo.EntityConfig
{
    public class ClienteConfiguration : EntityTypeConfiguration<Cliente>
    {
        public ClienteConfiguration()
        {
            //Table
            ToTable("tbl_cliente");

            //Key
            HasKey(c => c.Id);

            //Properties
            Property(c => c.Id)
                .HasColumnName("id")
                .HasColumnType("INT")
                .IsRequired();

            Property(c => c.Tipo)
                .HasColumnName("Tipo")
                .HasColumnType("VARCHAR");

            Property(c => c.CodigoCliente)
                .HasColumnName("Codigo_Cliente")
                .HasColumnType("INT");

            Property(c => c.LojaCliente)
                .HasColumnName("Loja_Cliente")
                .HasColumnType("INT");

            Property(c => c.Pessoa)
                .HasColumnName("Pessoa")
                .HasColumnType("VARCHAR");

            Property(c => c.RazaoSocial)
                .HasColumnName("Razao_Social")
                .HasColumnType("VARCHAR");

            Property(c => c.Cnpj)
                .HasColumnName("Cnpj")
                .HasColumnType("VARCHAR");

            Property(c => c.InscricaoEstadual)
                .HasColumnName("Insc_Estadual")
                .HasColumnType("VARCHAR");

            Property(c => c.NomeFantasia)
                .HasColumnName("Nome_Fantasia")
                .HasColumnType("VARCHAR");

            Property(c => c.Responsavel)
                .HasColumnName("Responsavel")
                .HasColumnType("VARCHAR");

            Property(c => c.Responsavel2)
                .HasColumnName("Responsavel_2")
                .HasColumnType("VARCHAR");

            Property(c => c.Responsavel3)
                .HasColumnName("Responsavel_3")
                .HasColumnType("VARCHAR");

            Property(c => c.Responsavel4)
                .HasColumnName("Responsavel_4")
                .HasColumnType("VARCHAR");

            Property(c => c.Cpf)
                .HasColumnName("Cpf")
                .HasColumnType("VARCHAR");

            Property(c => c.Telefone)
                .HasColumnName("Telefone")
                .HasColumnType("VARCHAR");

            Property(c => c.Telefone2)
                .HasColumnName("Telefone_2")
                .HasColumnType("VARCHAR");

            Property(c => c.Telefone3)
                .HasColumnName("Telefone_3")
                .HasColumnType("VARCHAR");

            Property(c => c.Telefone4)
                .HasColumnName("Telefone_4")
                .HasColumnType("VARCHAR");

            Property(c => c.Email)
                .HasColumnName("Email")
                .HasColumnType("VARCHAR");

            Property(c => c.Email2)
                .HasColumnName("Email_2")
                .HasColumnType("VARCHAR");

            Property(c => c.Email3)
                .HasColumnName("Email_3")
                .HasColumnType("VARCHAR");

            Property(c => c.Email4)
                .HasColumnName("Email_4")
                .HasColumnType("VARCHAR");

            Property(c => c.Cep)
                .HasColumnName("Cep")
                .HasColumnType("VARCHAR");

            Property(c => c.Endereco)
                .HasColumnName("Endereco")
                .HasColumnType("VARCHAR");

            Property(c => c.EnderecoNumero)
                .HasColumnName("Endereco_Num")
                .HasColumnType("VARCHAR");

            Property(c => c.Complemento)
                .HasColumnName("Endereco_Com")
                .HasColumnType("VARCHAR");

            Property(c => c.Bairro)
                .HasColumnName("Bairro")
                .HasColumnType("VARCHAR");

            Property(c => c.Cidade)
                .HasColumnName("Cidade")
                .HasColumnType("VARCHAR");

            Property(c => c.Estado)
                .HasColumnName("Estado")
                .HasColumnType("VARCHAR");

            Property(c => c.Login)
                .HasColumnName("Login")
                .HasColumnType("VARCHAR");

            Property(c => c.Senha)
                .HasColumnName("Senha")
                .HasColumnType("VARCHAR");

            Property(c => c.Observacao)
                .HasColumnName("Observacao")
                .HasColumnType("VARCHAR");

            Property(c => c.Observacao2)
                .HasColumnName("Observacao_2")
                .HasColumnType("VARCHAR");

            Property(c => c.Situacao)
                .HasColumnName("Situacao")
                .HasColumnType("VARCHAR");

            Property(c => c.Plano)
                .HasColumnName("Plano")
                .HasColumnType("VARCHAR");

            Property(c => c.QuantidadeLojas)
                .HasColumnName("Qtd_Lojas")
                .HasColumnType("INT");

            Property(c => c.MalaDireta)
                .HasColumnName("Mala_Direta")
                .HasColumnType("VARCHAR");

            Property(c => c.BloqueioAutomatico)
                .HasColumnName("BloqueioAuto")
                .HasColumnType("VARCHAR");

            Property(c => c.Perfil)
                .HasColumnName("Perfil")
                .HasColumnType("VARCHAR");

            Property(c => c.SituacaoLoja)
                .HasColumnName("Sit")
                .HasColumnType("VARCHAR");

            Property(c => c.DataCadastro)
                .HasColumnName("Data_Cadastro")
                .HasColumnType("DATE");

            Property(c => c.DataSenha)
                .HasColumnName("Data_Senha")
                .HasColumnType("DATE");

            Property(c => c.IpUltimoAcesso)
                .HasColumnName("IP_UltAcesso")
                .HasColumnType("VARCHAR");

            Property(c => c.DataUltimoAcesso)
                .HasColumnName("Data_UltAcesso")
                .HasColumnType("DATE");

            Property(c => c.ChamadoAvaliado)
                .HasColumnName("Chamado_Avaliado")
                .HasColumnType("INT");

            Property(c => c.ChamadoAvaliadoTotal)
                .HasColumnName("Chamado_Avaliado_Total")
                .HasColumnType("INT");

            Property(c => c.ChamadoAvaliadoMedia)
                .HasColumnName("Chamado_Avaliado_Media")
                .HasColumnType("INT");

            Property(c => c.VisitaAvaliado)
                .HasColumnName("Visita_Avaliado")
                .HasColumnType("INT");

            Property(c => c.VisitaAvaliadoTotal)
                .HasColumnName("Visita_Avaliado_Total")
                .HasColumnType("INT");

            Property(c => c.VisitaAvaliadoMedia)
                .HasColumnName("Visita_Avaliado_Media")
                .HasColumnType("INT");

            Property(c => c.Avaliado)
                .HasColumnName("Avaliado")
                .HasColumnType("INT");

            Property(c => c.AvaliadoTotal)
                .HasColumnName("Avaliado_Total")
                .HasColumnType("INT");

            Property(c => c.AvaliadoMedia)
                .HasColumnName("Avaliado_Media")
                .HasColumnType("INT");
        }
    }
}
