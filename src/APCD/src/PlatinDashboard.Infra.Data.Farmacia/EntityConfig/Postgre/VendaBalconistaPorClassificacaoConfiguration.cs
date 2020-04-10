using PlatinDashboard.Domain.Farmacia.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlatinDashboard.Infra.Data.Farmacia.EntityConfig.Postgre
{
    public class VendaBalconistaPorClassificacaoConfiguration : EntityTypeConfiguration<VendaBalconistaPorClassificacao>
    {
        public VendaBalconistaPorClassificacaoConfiguration()
        {
            ToTable("web.bal_ven_cls_all");

            HasKey(v => v.Dat);

            Property(c => c.Dat)
                .HasColumnName("dat")
                .HasColumnType("date")
                .IsRequired();

            Property(c => c.My)
                .HasColumnName("my")
                .HasColumnType("varchar")
                .HasMaxLength(6);

            Property(c => c.Uad)
                .HasColumnName("uad")
                .HasColumnType("int2");

            Property(v => v.Bal)
                .HasColumnName("bal")
                .HasColumnType("int4");

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
                .HasColumnType("numeric");

            Property(c => c.Tme)
                .HasColumnName("tme")
                .HasColumnType("numeric");            

            Property(c => c.Vme)
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
