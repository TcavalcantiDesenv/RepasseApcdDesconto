using PlatinDashboard.Domain.Farmacia.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlatinDashboard.Infra.Data.Farmacia.EntityConfig.Postgre
{
    public class VenUadConfiguration : EntityTypeConfiguration<VenUad>
    {
        public VenUadConfiguration()
        {
            ToTable("web.ven_uad");
           
            HasKey(c => new { c.Dat, c.Uad });

            Property(v => v.Dat)
                .HasColumnName("dat")
                .HasColumnType("date");

            Property(v => v.Uad)
                .HasColumnName("uad")
                .HasColumnType("int2");

            Property(v => v.My)
                .HasColumnName("my")
                .HasColumnType("varchar")
                .HasMaxLength(50);

            Property(v => v.Vlb)
                .HasColumnName("vlb")
                .HasColumnType("numeric");

            Property(v => v.Vlc)
                .HasColumnName("vlc")
                .HasColumnType("numeric");

            Property(v => v.Vld)
                .HasColumnName("vld")
                .HasColumnType("numeric");

            Property(v => v.Vde)
                .HasColumnName("vde")
                .HasColumnType("numeric");

            Property(v => v.Vdc)
                .HasColumnName("vdc")
                .HasColumnType("numeric");

            Property(v => v.Reg)
                .HasColumnName("reg")
                .HasColumnType("numeric");

            Property(v => v.Tme)
                .HasColumnName("tme")
                .HasColumnType("numeric");

            Property(v => v.Vme)
                .HasColumnName("vme")
                .HasColumnType("numeric");

            Property(v => v.Ite)
                .HasColumnName("ite")
                .HasColumnType("int4");

            Property(v => v.Qtp)
                .HasColumnName("qtp")
                .HasColumnType("int4");
        }
    }
}
