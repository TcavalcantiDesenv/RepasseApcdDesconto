using PlatinDashboard.Domain.Farmacia.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlatinDashboard.Infra.Data.Farmacia.EntityConfig.Postgre
{
    public class ViewBalconistaConfiguration : EntityTypeConfiguration<ViewBalconista>
    {
        public ViewBalconistaConfiguration()
        {
            ToTable("web.bal_ven_cls_all_grafico_por_balconista");

            HasKey(e => new { e.MesAno, e.IdBalconista, e.Classificacao });

            Property(v => v.MesAno)
                .HasColumnName("mes")
                .HasColumnType("varchar");

            Property(v => v.IdBalconista)
                .HasColumnName("balconista")
                .HasColumnType("int4");

            Property(v => v.Classificacao)
                .HasColumnName("classificacao")
                .HasColumnType("int4");

            Property(v => v.IdLoja)
                .HasColumnName("lojas")
                .HasColumnType("int4");

            Property(v => v.Valor)
                .HasColumnName("valortotal")
                .HasColumnType("numeric");
        }
    }
}
