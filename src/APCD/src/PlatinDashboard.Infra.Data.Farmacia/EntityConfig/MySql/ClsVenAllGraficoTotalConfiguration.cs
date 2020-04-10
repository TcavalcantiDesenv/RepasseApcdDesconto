using PlatinDashboard.Domain.Farmacia.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlatinDashboard.Infra.Data.Farmacia.EntityConfig.MySql
{
    public class ClsVenAllGraficoTotalConfiguration : EntityTypeConfiguration<ClsVenAllGraficoTotal>
    {
        public ClsVenAllGraficoTotalConfiguration()
        {
            ToTable("cls_ven_all_grafico_total");

            HasKey(c => c.Dat);

            Property(c => c.Dat)
                .HasColumnName("dias")
                .HasColumnType("DATE");

            Property(c => c.Mes)
                .HasColumnName("mes")
                .HasColumnType("VARCHAR");

            Property(u => u.Uad)
                .HasColumnName("uad")
                .HasColumnType("INT");

            Property(c => c.ValorBruto)
                .HasColumnName("valorbruto")
                .HasColumnType("DECIMAL");

            Property(c => c.Meta)
                .HasColumnName("meta")
                .HasColumnType("DECIMAL");

            Property(c => c.Percentual)
                .HasColumnName("percentual")
                .HasColumnType("DECIMAL");
        }
    }
}
