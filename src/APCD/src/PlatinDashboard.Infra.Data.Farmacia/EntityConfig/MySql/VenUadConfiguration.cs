using PlatinDashboard.Domain.Farmacia.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlatinDashboard.Infra.Data.Farmacia.EntityConfig.MySql
{
    public class VenUadConfiguration : EntityTypeConfiguration<VenUad>
    {
        public VenUadConfiguration()
        {
            ToTable("ven_uad");
           
            HasKey(c => new { c.Dat, c.Uad });

            Property(v => v.Dat)
                .HasColumnName("data_da_venda")
                .HasColumnType("DATE");

            Property(v => v.Uad)
                .HasColumnName("id_loja")
                .HasColumnType("INT");

            Property(v => v.My)
                .HasColumnName("mes_ano")
                .HasColumnType("VARCHAR")
                .HasMaxLength(50);

            Property(v => v.Vlb)
                .HasColumnName("valor_bruto")
                .HasColumnType("DECIMAL");

            Property(v => v.Vlc)
                .HasColumnName("valor_do_custo")
                .HasColumnType("DECIMAL");

            Property(v => v.Vld)
                .HasColumnName("valor_desconto")
                .HasColumnType("DECIMAL");

            Property(v => v.Vde)
                .HasColumnName("valor_da_devolucao")
                .HasColumnType("DECIMAL");

            Property(v => v.Vdc)
                .HasColumnName("valor_do_custo_devolucao")
                .HasColumnType("DECIMAL");

            Property(v => v.Reg)
                .HasColumnName("qtde_registro")
                .HasColumnType("DECIMAL");

            Property(v => v.Tme)
                .HasColumnName("ticket_medio")
                .HasColumnType("DECIMAL");

            Property(v => v.Vme)
                .HasColumnName("valor_medio")
                .HasColumnType("DECIMAL");

            Property(v => v.Ite)
                .HasColumnName("ite")
                .HasColumnType("INT");

            Property(v => v.Qtp)
                .HasColumnName("qtp")
                .HasColumnType("INT");
        }
    }
}
