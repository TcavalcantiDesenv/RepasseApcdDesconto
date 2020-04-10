using PlatinDashboard.Domain.Farmacia.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlatinDashboard.Infra.Data.Farmacia.EntityConfig.Postgre
{
    public class ClsVenAllConfiguration : EntityTypeConfiguration<ClsVenAll>
    {
        public ClsVenAllConfiguration()
        {
            ToTable("web.cls_ven_all");

            HasKey(c => new { c.Dat, c.Cls });

            Property(c => c.Dat)
                .HasColumnName("dat")
                .HasColumnType("date")
                .IsRequired();

            Property(c => c.My)
                .HasColumnName("my")
                .HasColumnType("varchar")
                .HasMaxLength(6);

            Property(c => c.Cls)
                .HasColumnName("cls")
                .HasColumnType("int2")
                .IsRequired();

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

        }
    }
}
