namespace SmokeFreeApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class commentstatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "status");
        }
    }
}
