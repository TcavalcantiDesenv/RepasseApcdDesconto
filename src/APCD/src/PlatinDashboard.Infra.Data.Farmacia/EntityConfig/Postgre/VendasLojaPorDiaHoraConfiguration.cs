using PlatinDashboard.Domain.Farmacia.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlatinDashboard.Infra.Data.Farmacia.EntityConfig.Postgre
{
    public class VendasLojaPorDiaHoraConfiguration : EntityTypeConfiguration<VendasLojaPorDiaHora>
    {
        public VendasLojaPorDiaHoraConfiguration()
        {
            ToTable("web.v_vendas_loja_por_dia_hora");

            HasKey(e => new { e.Loja, e.Hora, e.My, e.Dat });

            Property(v => v.My).HasColumnName("my")
                .HasColumnType("varchar");

            Property(c => c.Dat)
                .HasColumnName("dat")
                .HasColumnType("date");

            Property(v => v.Loja)
                .HasColumnName("loja")
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
