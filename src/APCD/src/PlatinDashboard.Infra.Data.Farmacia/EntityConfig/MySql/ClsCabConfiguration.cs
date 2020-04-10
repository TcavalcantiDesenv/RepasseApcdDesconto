using PlatinDashboard.Domain.Farmacia.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlatinDashboard.Infra.Data.Farmacia.EntityConfig.MySql
{
    public class ClsCabConfiguration : EntityTypeConfiguration<ClsCab>
    {
        public ClsCabConfiguration()
        {
            ToTable("cls_cab");

            HasKey(c => c.Ide);

            Property(c => c.Ide)
                .HasColumnName("ide")
                .HasColumnType("BIGINT")
                .IsRequired();

            Property(c => c.Cod)
                .HasColumnName("cod")
                .HasColumnType("VARCHAR")
                .HasMaxLength(5);

            Property(c => c.Des)
                .HasColumnName("des")
                .HasColumnType("VARCHAR")
                .HasMaxLength(50);

            Property(c => c.Sgl)
                .HasColumnName("sgl")
                .HasColumnType("varchar")
                .HasMaxLength(5);
        }
    }
}
