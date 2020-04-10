using Microsoft.AspNet.Identity.EntityFramework;
using PlatinDashboard.Infra.CrossCutting.Identity.Model;
using System;
using System.Data.Entity;
using System.Linq;

namespace PlatinDashboard.Infra.CrossCutting.Identity.Contexto
{
    public class IdentityContext : IdentityDbContext<ApplicationUser>, IDisposable

    {
        public IdentityContext() : base("PortalConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Claims> Claims { get; set; }
        public DbSet<ClienteWeb> ClientesWeb { get; set; }

        public static IdentityContext Create()
        {
            return new IdentityContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>().ToTable("Users")
                .Property(u => u.Id).HasColumnName("UserId");        
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("CreatedAt") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreatedAt").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("CreatedAt").IsModified = false;
                }
            }

            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("UpdatedAt") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("UpdatedAt").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("UpdatedAt").CurrentValue = DateTime.Now;
                }
            }
            return base.SaveChanges();
        }
    }
}
