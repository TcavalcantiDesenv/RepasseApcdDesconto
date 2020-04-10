using PlatinDashboard.Domain.Farmacia.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlatinDashboard.Infra.Data.Farmacia.EntityConfig.MySql
{
    class GraficoWebConfiguration : EntityTypeConfiguration<GraficoWeb>
    {
        public GraficoWebConfiguration()
        {
            ToTable("grafico_web");

            HasKey(g => g.Dat);

            Property(g => g.Dat)
                .HasColumnName("dat")
                .HasColumnType("DATE");

            Property(g => g.My)
                .HasColumnName("my")
                .HasColumnType("VARCHAR");

            Property(g => g.TotaldeLojas)
                .HasColumnName("totaldelojas")
                .HasColumnType("INT");

            Property(g => g.Lucro)
                .HasColumnName("lucro")
                .HasColumnType("DECIMAL");

            Property(g => g.VendaBruta)
                .HasColumnName("vendabruta")
                .HasColumnType("DECIMAL");

            Property(g => g.Liquida)
                .HasColumnName("liquida")
                .HasColumnType("DECIMAL");

            Property(g => g.Desconto)
                .HasColumnName("desconto")
                .HasColumnType("DECIMAL");

            Property(g => g.PercentualMargem)
                .HasColumnName("percentual_margem")
                .HasColumnType("DECIMAL");

            Property(g => g.QtMediaClientes)
                .HasColumnName("qt_media_clientes")
                .HasColumnType("DECIMAL");

            Property(g => g.ClientesAtendidos)
                .HasColumnName("clientes_atendidos")
                .HasColumnType("DECIMAL");

            Property(g => g.Custo)
                .HasColumnName("custo")
                .HasColumnType("DECIMAL");

            Property(g => g.Devolucao)
                .HasColumnName("devolucao")
                .HasColumnType("DECIMAL");
        }
    }
}
