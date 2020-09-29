namespace RAS.TimeSheets.Persistance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class s1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HourlyMissions", "Hours", c => c.Double(nullable: false));
            DropColumn("tsm.PresenceHours", "MissionHours");
        }
        
        public override void Down()
        {
            AddColumn("tsm.PresenceHours", "MissionHours", c => c.Double(nullable: false));
            DropColumn("dbo.HourlyMissions", "Hours");
        }
    }
}
