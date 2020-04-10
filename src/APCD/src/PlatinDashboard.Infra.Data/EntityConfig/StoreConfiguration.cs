using PlatinDashboard.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlatinDashboard.Infra.Data.EntityConfig
{
    public class StoreConfiguration : EntityTypeConfiguration<Store>
    {
        public StoreConfiguration()
        {
            //Table
            ToTable("Stores");

            //Key
            HasKey(s => s.StoreId);

            //Properties
            Property(s => s.Name)
                .IsRequired();

            Property(s => s.ExternalStoreId)
                .IsRequired();

            //Relationships
            HasRequired(s => s.Company)
                .WithMany(c => c.Stores)
                .HasForeignKey(s => s.CompanyId)
                .WillCascadeOnDelete(true);
        }
    }
}
