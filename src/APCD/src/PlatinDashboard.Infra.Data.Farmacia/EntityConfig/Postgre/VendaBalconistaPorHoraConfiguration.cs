using PlatinDashboard.Domain.Farmacia.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlatinDashboard.Infra.Data.Farmacia.EntityConfig.Postgre
{
    public class VendaBalconistaPorHoraConfiguration : EntityTypeConfiguration<VendaBalconistaPorHora>
    {
        public VendaBalconistaPorHoraConfiguration()
        {
            ToTable("web.v_vendas_balconista_por_hora");

            HasKey(e => new { e.Loja, e.Hora, e.My });

            Property(v => v.My).HasColumnName("my")
                .HasColumnType("varchar");

            Property(v => v.Loja)
                .HasColumnName("loja")
                .HasColumnType("int4");

            Property(v => v.Balconista)
                .HasColumnName("balconista")
                .HasColumnType("int4");

            Property(v => v.ClientesAtendidos)
                .HasColumnName("clientes_atendidos")
                .HasColumnType("int8");

            Property(v => v.Valor)
                .HasColumnName("valor")
                .HasColumnType("numeric");

            Property(v => v.Desconto)
                .HasColumnName("desconto")
                .HasColumnType("numeric");

            Property(v => v.Devolucao)
                .HasColumnName("devolucao")
                .HasColumnType("numeric");

            Property(v => v.Hora)
                .HasColumnName("hora")
                .HasColumnType("int8");
        }
    }
}
