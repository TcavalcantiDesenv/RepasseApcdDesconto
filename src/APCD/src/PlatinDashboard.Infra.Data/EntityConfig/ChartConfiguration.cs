using PlatinDashboard.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlatinDashboard.Infra.Data.EntityConfig
{
    public class ChartConfiguration : EntityTypeConfiguration<Chart>
    {
        public ChartConfiguration()
        {
            //Table
            ToTable("Charts");

            //Key
            HasKey(c => c.ChartId);

            //Properties
            Property(c => c.Name)
                .IsRequired();

            Property(c => c.ClaimType)
                .IsRequired();

            //Relationships
            HasMany(c => c.Companies)
                .WithMany(c => c.Charts)
                .Map(cc =>
                {
                    cc.MapLeftKey("ChartId");
                    cc.MapRightKey("CompanyId");
                    cc.ToTable("CompanyChart");
                });
        }
    }
}
