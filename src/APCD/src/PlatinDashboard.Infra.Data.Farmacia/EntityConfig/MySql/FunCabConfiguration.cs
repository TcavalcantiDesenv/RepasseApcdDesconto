using PlatinDashboard.Domain.Farmacia.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlatinDashboard.Infra.Data.Farmacia.EntityConfig.MySql
{
    public class FunCabConfiguration : EntityTypeConfiguration<FunCab>
    {
        public FunCabConfiguration()
        {
            ToTable("fun_cab");

            HasKey(c => c.Ide);

            Property(c => c.Ide)
                .HasColumnName("ide")
                .HasColumnType("INT")
                .IsRequired();

            Property(c => c.Cod)
                .HasColumnName("codigo")
                .HasColumnType("VARCHAR")
                .HasMaxLength(6);

            Property(c => c.Nom)
                .HasColumnName("nome")
                .HasColumnType("VARCHAR")
                .HasMaxLength(80);

            Property(c => c.Uad)
                .HasColumnName("id_loja")
                .HasColumnType("INT");
        }
    }
}
