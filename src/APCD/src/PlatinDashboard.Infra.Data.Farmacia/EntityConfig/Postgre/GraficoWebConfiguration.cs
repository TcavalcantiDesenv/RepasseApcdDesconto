using PlatinDashboard.Domain.Farmacia.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlatinDashboard.Infra.Data.Farmacia.EntityConfig.Postgre
{
    class GraficoWebConfiguration : EntityTypeConfiguration<GraficoWeb>
    {
        public GraficoWebConfiguration()
        {
            ToTable("web.grafico_web");

            HasKey(g => g.Dat);

            Property(g => g.Dat)
                .HasColumnName("dat")
                .HasColumnType("date");

            Property(g => g.My)
                .HasColumnName("my")
                .HasColumnType("varchar");

            Property(g => g.TotaldeLojas)
                .HasColumnName("totaldelojas")
                .HasColumnType("int4");

            Property(g => g.Lucro)
                .HasColumnName("lucro")
                .HasColumnType("numeric");

            Property(g => g.VendaBruta)
                .HasColumnName("vendabruta")
                .HasColumnType("numeric");

            Property(g => g.Liquida)
                .HasColumnName("liquida")
                .HasColumnType("numeric");

            Property(g => g.Desconto)
                .HasColumnName("desconto")
                .HasColumnType("numeric");

            Property(g => g.PercentualMargem)
                .HasColumnName("percentual_margem")
                .HasColumnType("numeric");

            Property(g => g.QtMediaClientes)
                .HasColumnName("qt_media_clientes")
                .HasColumnType("numeric");

            Property(g => g.ClientesAtendidos)
                .HasColumnName("clientes_atendidos")
                .HasColumnType("numeric");

            Property(g => g.Custo)
                .HasColumnName("custo")
                .HasColumnType("numeric");

            Property(g => g.Devolucao)
                .HasColumnName("devolucao")
                .HasColumnType("numeric");
        }
    }
}
