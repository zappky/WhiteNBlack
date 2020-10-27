namespace SmokeFreeApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class checkindateid : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.CheckInDates");
            AlterColumn("dbo.CheckInDates", "checkInId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.CheckInDates", "checkInId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.CheckInDates");
            AlterColumn("dbo.CheckInDates", "checkInId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.CheckInDates", "checkInId");
        }
    }
}
