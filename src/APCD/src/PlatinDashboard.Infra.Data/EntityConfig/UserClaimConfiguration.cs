using PlatinDashboard.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlatinDashboard.Infra.Data.EntityConfig
{
    public class UserClaimConfiguration : EntityTypeConfiguration<UserClaim>
    {
        public UserClaimConfiguration()
        {
            //Table
            ToTable("UserClaims");

            //Key
            HasKey(c => c.Id);

            //Properties
            Property(c => c.UserId)
                .HasColumnType("nvarchar")
                .HasMaxLength(128)
                .IsRequired();

            Property(c => c.ClaimType)
                .HasColumnType("nvarchar")
                .IsMaxLength();

            Property(c => c.ClaimValue)
                .HasColumnType("nvarchar")
                .IsMaxLength();

            //Relationship
            HasRequired(c => c.User)
                .WithMany(u => u.UserClaims)
                .HasForeignKey(c => c.UserId)
                .WillCascadeOnDelete(true);
        }
    }
}
