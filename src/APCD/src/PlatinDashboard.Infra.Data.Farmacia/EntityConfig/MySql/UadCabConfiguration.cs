using PlatinDashboard.Domain.Farmacia.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlatinDashboard.Infra.Data.Farmacia.EntityConfig.MySql
{
    public class UadCabConfiguration : EntityTypeConfiguration<UadCab>
    {
        public UadCabConfiguration()
        {
            ToTable("uad_cab");

            HasKey(u => u.Uad);

            Property(u => u.Uad)
                .HasColumnName("id_loja")
                .HasColumnType("INT");

            Property(u => u.Des)
                .HasColumnName("descricao")
                .HasColumnType("VARCHAR")
                .HasMaxLength(150);

            Property(u => u.Tip)
                .HasColumnName("tipo")
                .HasColumnType("VARCHAR")
                .HasMaxLength(2);

            Property(u => u.Atv)
                .HasColumnName("status")
                .HasColumnType("BOOL");
        }
    }
}
