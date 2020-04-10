using PlatinDashboard.Domain.Farmacia.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlatinDashboard.Infra.Data.Farmacia.EntityConfig.MySql
{
    public class VendaClassificacaoLojaSpConfiguration : EntityTypeConfiguration<VendaClassificacaoLojaSp>
    {
        public VendaClassificacaoLojaSpConfiguration()
        {
            ToTable("vendas_classificacao_loja");

            HasKey(c => c.Dias);

            Property(c => c.Dias)
                .HasColumnName("dias")
                .HasColumnType("DATE");

            Property(c => c.Cls)
                .HasColumnName("cls")
                .HasColumnType("INT");

            Property(c => c.Mes)
                .HasColumnName("mes")
                .HasColumnType("VARCHAR");

            Property(u => u.Uad)
                .HasColumnName("uad")
                .HasColumnType("INT");

            Property(c => c.ValorBruto)
                .HasColumnName("valorbruto")
                .HasColumnType("DECIMAL");

            Property(c => c.Meta)
                .HasColumnName("meta")
                .HasColumnType("DECIMAL");

            Property(c => c.Percentual)
                .HasColumnName("percentual")
                .HasColumnType("DECIMAL");
        }
    }
}
