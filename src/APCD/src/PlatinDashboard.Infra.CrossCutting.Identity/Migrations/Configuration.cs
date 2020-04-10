namespace PlatinDashboard.Infra.CrossCutting.Identity.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PlatinDashboard.Infra.CrossCutting.Identity.Contexto.IdentityContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PlatinDashboard.Infra.CrossCutting.Identity.Contexto.IdentityContext context)
        {

        }
    }
}
