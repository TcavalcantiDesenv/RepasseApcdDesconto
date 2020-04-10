using PlatinDashboard.Domain.Farmacia.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlatinDashboard.Infra.Data.Farmacia.EntityConfig.Postgre
{
    public class BalVenAllGraficoTotalConfiguration : EntityTypeConfiguration<BalVenAllGraficoTotal>
    {
        public BalVenAllGraficoTotalConfiguration()
        {
            ToTable("web.bal_ven_all_grafico_total");

            HasKey(c => c.Dat);

            Property(c => c.Dat)
                .HasColumnName("dias")
                .HasColumnType("date");

            Property(c => c.Mes)
                .HasColumnName("mes")
                .HasColumnType("varchar");

            Property(v => v.Bal)
                .HasColumnName("bal")
                .HasColumnType("int4");

            Property(c => c.ValorBruto)
                .HasColumnName("valorbruto")
                .HasColumnType("numeric");

            Property(c => c.Meta)
                .HasColumnName("meta")
                .HasColumnType("numeric");

            Property(c => c.Percentual)
                .HasColumnName("percentual")
                .HasColumnType("numeric");
        }
    }
}
