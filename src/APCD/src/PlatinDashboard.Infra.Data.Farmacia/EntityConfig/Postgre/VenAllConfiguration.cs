using PlatinDashboard.Domain.Farmacia.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlatinDashboard.Infra.Data.Farmacia.EntityConfig.Postgre
{
    public class VenAllConfiguration : EntityTypeConfiguration<VenAll>
    {
        public VenAllConfiguration()
        {
            ToTable("web.ven_all");

            HasKey(v => v.Dat);

            Property(c => c.Dat)
                .HasColumnName("dat")
                .HasColumnType("date")
                .IsRequired();

            Property(c => c.My)
                .HasColumnName("my")
                .HasColumnType("varchar")
                .HasMaxLength(6);

            Property(c => c.Vlb)
                .HasColumnName("vlb")
                .HasColumnType("numeric");

            Property(c => c.Vld)
                .HasColumnName("vld")
                .HasColumnType("numeric");

            Property(c => c.Vlc)
                .HasColumnName("vlc")
                .HasColumnType("numeric");

            Property(c => c.Vde)
                .HasColumnName("vde")
                .HasColumnType("numeric");

            Property(c => c.Vdc)
                .HasColumnName("vdc")
                .HasColumnType("numeric");

            Property(c => c.Reg)
                .HasColumnName("reg")
                .HasColumnType("int4");

            Property(c => c.Tme)
                .HasColumnName("tme")
                .HasColumnType("numeric");

            Property(c => c.Nua)
                .HasColumnName("nua")
                .HasColumnType("int2");

            Property(c => c.Vme)
                .HasColumnName("vme")
                .HasColumnType("numeric");

            Property(c => c.Qtp)
                .HasColumnName("qtp")
                .HasColumnType("int4");            
        }
    }
}
