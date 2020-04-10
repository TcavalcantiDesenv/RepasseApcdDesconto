using PlatinDashboard.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PlatinDashboard.Infra.Data.EntityConfig
{
    public class UserStoreConfiguration : EntityTypeConfiguration<UserStore>
    {
        public UserStoreConfiguration()
        {
            //Table
            ToTable("UsersStores");

            //Properties
            Property(c => c.UserId)
                .HasColumnType("nvarchar")
                .HasMaxLength(128)
                .IsRequired();

            //Key
            HasKey(us => us.UserStoreId);

            //Relationships
            HasRequired(us => us.User)
                .WithMany(u => u.UsersStores)
                .HasForeignKey(us => us.UserId)
                .WillCascadeOnDelete(true);

            HasRequired(us => us.Store)
                .WithMany(s => s.UsersStores)
                .HasForeignKey(us => us.StoreId);
        }
    }
}
