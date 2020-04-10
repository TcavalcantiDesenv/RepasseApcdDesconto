namespace PlatinDashboard.Infra.CrossCutting.Identity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAdministrativeFieldsToUser : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.Users", "AdministrativeCode", c => c.Int());
            //AddColumn("dbo.Users", "IsAdministrativeAuthentication", c => c.Boolean(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.Users", "IsAdministrativeAuthentication");
            DropColumn("dbo.Users", "AdministrativeCode");
        }
    }
}
