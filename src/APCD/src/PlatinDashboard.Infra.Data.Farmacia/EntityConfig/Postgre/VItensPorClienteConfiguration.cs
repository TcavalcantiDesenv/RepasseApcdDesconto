using PlatinDashboard.Domain.Farmacia.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlatinDashboard.Infra.Data.Farmacia.EntityConfig.Postgre
{
    public class VItensPorClienteConfiguration : EntityTypeConfiguration<VItensPorCliente>
    {
        public VItensPorClienteConfiguration()
        {
            ToTable("web.v_itensporcliente");

            HasKey(c => c.My);

            Property(c => c.My)
                .HasColumnName("my")
                .HasColumnType("varchar");

            Property(c => c.ItensCliente)
                .HasColumnName("itenscliente")
                .HasColumnType("numeric");

            Property(c => c.TotalCli)
                .HasColumnName("totalcli")
                .HasColumnType("numeric");

            Property(c => c.TotalItem)
                .HasColumnName("totalitem")
                .HasColumnType("numeric");

        }
    }
}
