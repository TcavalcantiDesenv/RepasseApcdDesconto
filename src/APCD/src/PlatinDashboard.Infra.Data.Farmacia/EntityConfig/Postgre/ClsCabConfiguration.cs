using PlatinDashboard.Domain.Farmacia.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlatinDashboard.Infra.Data.Farmacia.EntityConfig.Postgre
{
    public class ClsCabConfiguration : EntityTypeConfiguration<ClsCab>
    {
        public ClsCabConfiguration()
        {
            ToTable("cadastro.cls_cab");

            HasKey(c => c.Ide);

            Property(c => c.Ide)
                .HasColumnName("ide")
                .HasColumnType("int8")
                .IsRequired();

            Property(c => c.Cod)
                .HasColumnName("cod")
                .HasColumnType("varchar")
                .HasMaxLength(5);

            Property(c => c.Des)
                .HasColumnName("des")
                .HasColumnType("varchar")
                .HasMaxLength(50);

            Property(c => c.Sgl)
                .HasColumnName("sgl")
                .HasColumnType("varchar")
                .HasMaxLength(5);
        }
    }
}
