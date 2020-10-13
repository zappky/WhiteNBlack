namespace SmokeFreeApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class progress : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CheckInDates",
                c => new
                    {
                        checkInId = c.String(nullable: false, maxLength: 128),
                        checkInDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.checkInId);
            
            CreateTable(
                "dbo.Progresses",
                c => new
                    {
                        userName = c.String(nullable: false, maxLength: 128),
                        totalCheckins = c.Int(nullable: false),
                        cigaIntake = c.Int(nullable: false),
                        cigaPrice = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.userName);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Progresses");
            DropTable("dbo.CheckInDates");
        }
    }
}
