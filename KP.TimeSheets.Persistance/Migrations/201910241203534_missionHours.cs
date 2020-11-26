namespace KP.TimeSheets.Persistance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class missionHours : DbMigration
    {
        public override void Up()
        {
            AddColumn("tsm.PresenceHours", "MissionHours", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("tsm.PresenceHours", "MissionHours");
        }
    }
}
