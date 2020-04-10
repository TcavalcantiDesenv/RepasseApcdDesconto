using PlatinDashboard.Domain.Farmacia.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlatinDashboard.Infra.Data.Farmacia.EntityConfig.Postgre
{
    public class UadCabConfiguration : EntityTypeConfiguration<UadCab>
    {
        public UadCabConfiguration()
        {
            ToTable("public.uad_cab");

            HasKey(u => u.Uad);

            Property(u => u.Uad)
                .HasColumnName("uad")
                .HasColumnType("int4");

            Property(u => u.Des)
                .HasColumnName("des")
                .HasColumnType("varchar")
                .HasMaxLength(150);

            Property(u => u.Tip)
                .HasColumnName("tip")
                .HasColumnType("varchar")
                .HasMaxLength(2);

            Property(u => u.Atv)
                .HasColumnName("atv")
                .HasColumnType("bool");
        }
    }
}
