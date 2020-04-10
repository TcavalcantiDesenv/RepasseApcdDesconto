using PlatinDashboard.Domain.Farmacia.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlatinDashboard.Infra.Data.Farmacia.EntityConfig.MySql
{
    public class VendaLojaPorMesConfiguration : EntityTypeConfiguration<VendaLojaPorMes>
    {
        public VendaLojaPorMesConfiguration()
        {
            ToTable("v_vendas_loja_por_mes");

            HasKey(c => new { c.Uad });

            Property(v => v.Uad)
                .HasColumnName("id_loja")
                .HasColumnType("INT");

            Property(v => v.AnoMes)
                .HasColumnName("mes_ano")
                .HasColumnType("VARCHAR")
                .HasMaxLength(50);

            Property(v => v.Bruto)
                .HasColumnName("valor_bruto")
                .HasColumnType("DECIMAL");

            Property(v => v.Desconto)
                .HasColumnName("valor_desconto")
                .HasColumnType("DECIMAL");

            Property(v => v.Custo)
                .HasColumnName("valor_do_custo")
                .HasColumnType("DECIMAL");

            Property(v => v.Devolucao)
                .HasColumnName("valor_da_devolucao")
                .HasColumnType("DECIMAL");

            Property(v => v.Vdc)
                .HasColumnName("valor_do_custo_devolucao")
                .HasColumnType("DECIMAL");

            Property(v => v.ClientesAtendidos)
                .HasColumnName("clientes_atendidos")
                .HasColumnType("DECIMAL");

            Property(v => v.TicketMedio)
                .HasColumnName("ticket_medio")
                .HasColumnType("DECIMAL");

            Property(v => v.Vme)
                .HasColumnName("valor_medio")
                .HasColumnType("DECIMAL");

            Property(v => v.ItensVendidos)
                .HasColumnName("itens_vendidos")
                .HasColumnType("INT");

            Property(v => v.UnidadesVendidas)
                .HasColumnName("unidades_vendidas")
                .HasColumnType("INT");

            Property(v => v.Cvl)
                .HasColumnName("cvl")
                .HasColumnType("DECIMAL");

            Property(v => v.Vmm)
                .HasColumnName("vmm")
                .HasColumnType("DECIMAL");
        }
    }
}
