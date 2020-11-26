namespace KP.TimeSheets.Persistance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class s : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DailyLeaves", "OrganisationId", c => c.Guid());
            AddColumn("dbo.HourlyLeaves", "OrganisationId", c => c.Guid());
            AddColumn("dbo.HourlyMissions", "OrganisationId", c => c.Guid());
            CreateIndex("dbo.DailyLeaves", "OrganisationId");
            CreateIndex("dbo.HourlyLeaves", "OrganisationId");
            CreateIndex("dbo.HourlyMissions", "OrganisationId");
            AddForeignKey("dbo.DailyLeaves", "OrganisationId", "dbo.OrganizationUnits", "ID");
            AddForeignKey("dbo.HourlyLeaves", "OrganisationId", "dbo.OrganizationUnits", "ID");
            AddForeignKey("dbo.HourlyMissions", "OrganisationId", "dbo.OrganizationUnits", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HourlyMissions", "OrganisationId", "dbo.OrganizationUnits");
            DropForeignKey("dbo.HourlyLeaves", "OrganisationId", "dbo.OrganizationUnits");
            DropForeignKey("dbo.DailyLeaves", "OrganisationId", "dbo.OrganizationUnits");
            DropIndex("dbo.HourlyMissions", new[] { "OrganisationId" });
            DropIndex("dbo.HourlyLeaves", new[] { "OrganisationId" });
            DropIndex("dbo.DailyLeaves", new[] { "OrganisationId" });
            DropColumn("dbo.HourlyMissions", "OrganisationId");
            DropColumn("dbo.HourlyLeaves", "OrganisationId");
            DropColumn("dbo.DailyLeaves", "OrganisationId");
        }
    }
}
