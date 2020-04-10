using PlatinDashboard.Domain.Farmacia.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlatinDashboard.Infra.Data.Farmacia.EntityConfig.Postgre
{
    public class FunCabConfiguration : EntityTypeConfiguration<FunCab>
    {
        public FunCabConfiguration()
        {
            ToTable("cadastro.fun_cab");

            HasKey(c => c.Ide);

            Property(c => c.Ide)
                .HasColumnName("ide")
                .HasColumnType("int8")
                .IsRequired();

            Property(c => c.Cod)
                .HasColumnName("cod")
                .HasColumnType("varchar")
                .HasMaxLength(6);

            Property(c => c.Nom)
                .HasColumnName("nom")
                .HasColumnType("varchar")
                .HasMaxLength(80);

            Property(c => c.Uad)
                .HasColumnName("uad")
                .HasColumnType("int2");
        }
    }
}
