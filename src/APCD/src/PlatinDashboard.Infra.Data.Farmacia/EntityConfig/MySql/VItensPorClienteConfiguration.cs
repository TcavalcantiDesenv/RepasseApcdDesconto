using PlatinDashboard.Domain.Farmacia.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlatinDashboard.Infra.Data.Farmacia.EntityConfig.MySql
{
    public class VItensPorClienteConfiguration : EntityTypeConfiguration<VItensPorCliente>
    {
        public VItensPorClienteConfiguration()
        {
            ToTable("v_itensporcliente");

            HasKey(c => c.My);

            Property(c => c.My)
                .HasColumnName("mes_ano")
                .HasColumnType("VARCHAR");

            Property(c => c.ItensCliente)
                .HasColumnName("qtde_de_itens_por_cliente")
                .HasColumnType("DECIMAL");

            Property(c => c.TotalCli)
                .HasColumnName("qtde_de_clientes")
                .HasColumnType("DECIMAL");

            Property(c => c.TotalItem)
                .HasColumnName("qtde_total_itens")
                .HasColumnType("DECIMAL");

        }
    }
}
