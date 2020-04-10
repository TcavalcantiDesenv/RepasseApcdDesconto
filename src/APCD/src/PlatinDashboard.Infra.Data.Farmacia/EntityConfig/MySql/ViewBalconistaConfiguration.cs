using PlatinDashboard.Domain.Farmacia.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlatinDashboard.Infra.Data.Farmacia.EntityConfig.MySql
{
    public class ViewBalconistaConfiguration : EntityTypeConfiguration<ViewBalconista>
    {
        public ViewBalconistaConfiguration()
        {
            ToTable("bal_ven_cls_all_grafico_por_balconista");

            HasKey(e => new { e.MesAno, e.IdBalconista, e.Classificacao });

            Property(v => v.MesAno)
                .HasColumnName("mes_ano")
                .HasColumnType("VARCHAR");

            Property(v => v.IdBalconista)
                .HasColumnName("id_balconista")
                .HasColumnType("INT");

            Property(v => v.Classificacao)
                .HasColumnName("classificacao")
                .HasColumnType("INT");

            Property(v => v.IdLoja)
                .HasColumnName("id_loja")
                .HasColumnType("INT");

            Property(v => v.Valor)
                .HasColumnName("valor_total")
                .HasColumnType("DECIMAL");
        }
    }
}
