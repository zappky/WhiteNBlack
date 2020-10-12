namespace SmokeFreeApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class profiePicture : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.GeneralUsers", "profilePicture", c => c.Binary());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.GeneralUsers", "profilePicture", c => c.String());
        }
    }
}
