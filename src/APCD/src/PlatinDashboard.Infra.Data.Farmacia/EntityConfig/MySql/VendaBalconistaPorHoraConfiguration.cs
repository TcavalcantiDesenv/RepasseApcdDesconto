using PlatinDashboard.Domain.Farmacia.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlatinDashboard.Infra.Data.Farmacia.EntityConfig.MySql
{
    public class VendaBalconistaPorHoraConfiguration : EntityTypeConfiguration<VendaBalconistaPorHora>
    {
        public VendaBalconistaPorHoraConfiguration()
        {
            ToTable("v_vendas_balconista_por_hora");

            HasKey(e => new { e.Loja, e.Hora, e.My });

            Property(v => v.My).HasColumnName("mes_ano")
                .HasColumnType("VARCHAR");

            Property(v => v.Loja)
                .HasColumnName("id_loja")
                .HasColumnType("INT");

            Property(v => v.Balconista)
                .HasColumnName("id_balconista")
                .HasColumnType("INT");

            Property(v => v.ClientesAtendidos)
                .HasColumnName("clientes_atendidos")
                .HasColumnType("INT");

            Property(v => v.Valor)
                .HasColumnName("valor")
                .HasColumnType("DECIMAL");

            Property(v => v.Desconto)
                .HasColumnName("desconto")
                .HasColumnType("DECIMAL");

            Property(v => v.Devolucao)
                .HasColumnName("devolucao")
                .HasColumnType("DECIMAL");

            Property(v => v.Hora)
                .HasColumnName("hora")
                .HasColumnType("INT");
        }
    }
}
