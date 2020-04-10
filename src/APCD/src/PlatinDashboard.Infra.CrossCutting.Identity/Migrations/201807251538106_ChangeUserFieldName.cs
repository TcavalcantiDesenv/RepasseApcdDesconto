namespace PlatinDashboard.Infra.CrossCutting.Identity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeUserFieldName : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.Users", "ImportedFromAdministrative", c => c.Boolean(nullable: false));
            //DropColumn("dbo.Users", "IsAdministrativeAuthentication");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "IsAdministrativeAuthentication", c => c.Boolean(nullable: false));
            DropColumn("dbo.Users", "ImportedFromAdministrative");
        }
    }
}
