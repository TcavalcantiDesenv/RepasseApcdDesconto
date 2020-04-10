using PlatinDashboard.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlatinDashboard.Infra.Data.EntityConfig
{
    public class CompanyConfiguration : EntityTypeConfiguration<Company>
    {
        public CompanyConfiguration()
        {
            //Table
            ToTable("Companies");

            //Key
            HasKey(c => c.CompanyId);

            //Properties
            Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(255);

            Property(c => c.CompanyType)
                .IsRequired()
                .HasMaxLength(100);

            Property(c => c.DatabaseServer)
                .IsRequired()
                .HasMaxLength(100);

            Property(c => c.Database)
                .IsRequired()
                .HasMaxLength(100);

            Property(c => c.DatabasePort)
                .IsRequired()
                .HasMaxLength(100);

            Property(c => c.DatabaseProvider)
                .IsRequired()
                .HasMaxLength(40)
                .HasColumnOrder(9);

            Property(c => c.DatabaseUser)
                .IsRequired()
                .HasMaxLength(100);

            Property(c => c.DatabasePassword)
                .IsRequired()
                .HasMaxLength(40);

            Property(c => c.CreatedAt)
                .IsRequired();

            Property(c => c.UpdatedAt)
                .IsOptional();

            Property(c => c.CustomerCode)
                .IsOptional();

            Property(c => c.ImportedFromAdministrative)
                .IsRequired();
        }
    }
}
