namespace KP.TimeSheets.Persistance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.HourlyLeaves", "ProjectID", "dbo.Projects");
            DropForeignKey("dbo.HourlyMissions", "ProjectID", "dbo.Projects");
            DropIndex("dbo.HourlyLeaves", new[] { "ProjectID" });
            DropIndex("dbo.HourlyMissions", new[] { "ProjectID" });
            AlterColumn("dbo.HourlyLeaves", "ProjectID", c => c.Guid());
            AlterColumn("dbo.HourlyMissions", "ProjectID", c => c.Guid());
            CreateIndex("dbo.HourlyLeaves", "ProjectID");
            CreateIndex("dbo.HourlyMissions", "ProjectID");
            AddForeignKey("dbo.HourlyLeaves", "ProjectID", "dbo.Projects", "ID");
            AddForeignKey("dbo.HourlyMissions", "ProjectID", "dbo.Projects", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HourlyMissions", "ProjectID", "dbo.Projects");
            DropForeignKey("dbo.HourlyLeaves", "ProjectID", "dbo.Projects");
            DropIndex("dbo.HourlyMissions", new[] { "ProjectID" });
            DropIndex("dbo.HourlyLeaves", new[] { "ProjectID" });
            AlterColumn("dbo.HourlyMissions", "ProjectID", c => c.Guid(nullable: false));
            AlterColumn("dbo.HourlyLeaves", "ProjectID", c => c.Guid(nullable: false));
            CreateIndex("dbo.HourlyMissions", "ProjectID");
            CreateIndex("dbo.HourlyLeaves", "ProjectID");
            AddForeignKey("dbo.HourlyMissions", "ProjectID", "dbo.Projects", "ID", cascadeDelete: true);
            AddForeignKey("dbo.HourlyLeaves", "ProjectID", "dbo.Projects", "ID", cascadeDelete: true);
        }
    }
}
