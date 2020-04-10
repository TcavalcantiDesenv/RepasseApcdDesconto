using PlatinDashboard.Domain.Farmacia.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlatinDashboard.Infra.Data.Farmacia.EntityConfig.Postgre
{
    public class VenUadClsDashConfiguration : EntityTypeConfiguration<VenUadClsDash>
    {
        public VenUadClsDashConfiguration()
        {
            ToTable("web.v_ven_uad_cls_dash");

            HasKey(c => new { c.Cls, c.Uad, c.My });

            Property(c => c.My)
                .HasColumnName("my")
                .HasColumnType("varchar")
                .HasMaxLength(6);

            Property(c => c.Uad)
                .HasColumnName("uad")
                .HasColumnType("int4");

            Property(c => c.Cls)
                .HasColumnName("cls")
                .HasColumnType("int4");

            Property(c => c.ValorBruto)
                .HasColumnName("vlb")
                .HasColumnType("numeric");           
        }
    }
}
