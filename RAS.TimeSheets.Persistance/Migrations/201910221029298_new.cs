namespace RAS.TimeSheets.Persistance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DailyLeaves", "ProjectID", "dbo.Projects");
            DropIndex("dbo.DailyLeaves", new[] { "ProjectID" });
            AlterColumn("dbo.DailyLeaves", "ProjectID", c => c.Guid());
            CreateIndex("dbo.DailyLeaves", "ProjectID");
            AddForeignKey("dbo.DailyLeaves", "ProjectID", "dbo.Projects", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DailyLeaves", "ProjectID", "dbo.Projects");
            DropIndex("dbo.DailyLeaves", new[] { "ProjectID" });
            AlterColumn("dbo.DailyLeaves", "ProjectID", c => c.Guid(nullable: false));
            CreateIndex("dbo.DailyLeaves", "ProjectID");
            AddForeignKey("dbo.DailyLeaves", "ProjectID", "dbo.Projects", "ID", cascadeDelete: true);
        }
    }
}
