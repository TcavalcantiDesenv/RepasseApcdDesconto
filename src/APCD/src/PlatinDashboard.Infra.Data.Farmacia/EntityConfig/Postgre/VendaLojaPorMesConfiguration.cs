using PlatinDashboard.Domain.Farmacia.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlatinDashboard.Infra.Data.Farmacia.EntityConfig.Postgre
{
    public class VendaLojaPorMesConfiguration : EntityTypeConfiguration<VendaLojaPorMes>
    {
        public VendaLojaPorMesConfiguration()
        {
            ToTable("web.v_vendas_loja_por_mes");

            HasKey(c => new { c.Uad });

            Property(v => v.Uad)
                .HasColumnName("uad")
                .HasColumnType("int2");

            Property(v => v.AnoMes)
                .HasColumnName("anomes")
                .HasColumnType("varchar")
                .HasMaxLength(50);

            Property(v => v.Bruto)
                .HasColumnName("bruto")
                .HasColumnType("numeric");

            Property(v => v.Desconto)
                .HasColumnName("desconto")
                .HasColumnType("numeric");

            Property(v => v.Custo)
                .HasColumnName("custo")
                .HasColumnType("numeric");

            Property(v => v.Devolucao)
                .HasColumnName("devolucao")
                .HasColumnType("numeric");

            Property(v => v.Vdc)
                .HasColumnName("vdc")
                .HasColumnType("numeric");

            Property(v => v.ClientesAtendidos)
                .HasColumnName("clientesatendidos")
                .HasColumnType("numeric");

            Property(v => v.TicketMedio)
                .HasColumnName("ticketmedio")
                .HasColumnType("numeric");

            Property(v => v.Vme)
                .HasColumnName("vme")
                .HasColumnType("numeric");

            Property(v => v.ItensVendidos)
                .HasColumnName("itensvendidos")
                .HasColumnType("int4");

            Property(v => v.UnidadesVendidas)
                .HasColumnName("unidadesvendidas")
                .HasColumnType("int4");

            Property(v => v.Cvl)
                .HasColumnName("cvl")
                .HasColumnType("numeric");

            Property(v => v.Vmm)
                .HasColumnName("vmm")
                .HasColumnType("numeric");
        }
    }
}
