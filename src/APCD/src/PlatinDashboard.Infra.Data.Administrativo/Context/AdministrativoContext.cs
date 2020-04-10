using PlatinDashboard.Domain.Administrativo.Entities;
using PlatinDashboard.Infra.Data.Administrativo.EntityConfig;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace PlatinDashboard.Infra.Data.Administrativo.Context
{
    public class AdministrativoContext : DbContext
    {
        public AdministrativoContext() : base("AdministrativoConnection")
        {

        }

        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Configurations.Add(new ClienteConfiguration());
        }
    }
}
