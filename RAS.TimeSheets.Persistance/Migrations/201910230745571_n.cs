namespace RAS.TimeSheets.Persistance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class n : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.HourlyLeaves", "Date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.HourlyLeaves", "Date", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
    }
}
