using PlatinDashboard.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace PlatinDashboard.Infra.Data.EntityConfig
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            //Table
            ToTable("Users");

            //Key
            HasKey(u => u.UserId);

            //Properties
            Property(u => u.UserId)
                .HasMaxLength(128)
                .HasColumnType("nvarchar");

            Property(u => u.Name)
                .HasMaxLength(128)
                .IsRequired();

            Property(u => u.LastName)
                .HasMaxLength(128)
                .IsOptional();

            Property(u => u.UserName)
                .HasMaxLength(256)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                                            new IndexAnnotation(
                                                    new IndexAttribute("UserNameIndex") { IsUnique = true }));

            Property(u => u.Email)
                .HasMaxLength(256)
                .IsRequired();

            Property(u => u.Active)
                .IsRequired();

            Property(u => u.UserType)
                .IsRequired();

            Property(u => u.EmailConfirmed)
                .IsRequired();

            Property(u => u.PhoneNumber)
                .IsOptional();

            Property(u => u.PhoneNumberConfirmed)
                .IsRequired();

            Property(u => u.TwoFactorEnabled)
                .IsRequired();

            Property(u => u.PasswordHash)
                .IsRequired();

            Property(u => u.SecurityStamp)
                .IsRequired();

            Property(u => u.LockoutEnabled)
                .IsRequired();

            Property(u => u.LockoutEndDateUtc)
                .HasColumnType("datetime2")
                .IsOptional();

            Property(u => u.AccessFailedCount)
                .IsRequired();

            Property(c => c.CreatedAt)
                .IsRequired();

            Property(c => c.UpdatedAt)
                .IsOptional();

            Property(c => c.AdministrativeCode)
                .IsOptional();

            Property(c => c.ImportedFromAdministrative)
                .IsRequired();

            //Relationships
            HasRequired(u => u.Company)
                .WithMany(c => c.Users)
                .HasForeignKey(u => u.CompanyId)
                .WillCascadeOnDelete(true);
        }
    }
}
