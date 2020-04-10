using System.Data.Entity.Migrations;

namespace PlatinDashboard.Infra.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<PlatinDashboard.Infra.Data.Context.PortalContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
           
        }

        protected override void Seed(PlatinDashboard.Infra.Data.Context.PortalContext context)
        {
            
        }
    }
}
