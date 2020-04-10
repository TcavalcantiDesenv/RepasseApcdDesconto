using PlatinDashboard.Domain.Farmacia.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlatinDashboard.Infra.Data.Farmacia.EntityConfig.Postgre
{
    public class BalVenAllGraficoPorClsConfiguration : EntityTypeConfiguration<BalVenAllGraficoPorCls>
    {
        public BalVenAllGraficoPorClsConfiguration()
        {
            ToTable("web.bal_ven_all_grafico_por_cls");

            HasKey(c => c.Dias);

            Property(c => c.Dias)
                .HasColumnName("dias")
                .HasColumnType("date");

            Property(c => c.Mes)
                .HasColumnName("mes")
                .HasColumnType("varchar");

            Property(v => v.Bal)
                .HasColumnName("bal")
                .HasColumnType("int4");

            Property(c => c.Cls)
                .HasColumnName("cls")
                .HasColumnType("int2");

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
