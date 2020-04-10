using PlatinDashboard.Domain.Farmacia.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlatinDashboard.Infra.Data.Farmacia.EntityConfig.MySql
{
    public class VenUadMensalConfiguration : EntityTypeConfiguration<VenUadMensal>
    {
        public VenUadMensalConfiguration()
        {
            ToTable("v_ven_uad_mensal");

            HasKey(c => new { c.Uad, c.My });

            Property(c => c.My)
                .HasColumnName("my")
                .HasColumnType("VARCHAR")
                .HasMaxLength(6);

            Property(c => c.Uad)
                .HasColumnName("uad")
                .HasColumnType("INT");

            Property(c => c.ValorBruto)
                .HasColumnName("vlb")
                .HasColumnType("DECIMAL");
        }
    }
}
