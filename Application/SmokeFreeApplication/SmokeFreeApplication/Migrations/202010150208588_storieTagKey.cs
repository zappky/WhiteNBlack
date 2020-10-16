namespace SmokeFreeApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class storieTagKey : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.StoriesTags");
            AlterColumn("dbo.StoriesTags", "tagID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.StoriesTags", new[] { "tagID", "storyID" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.StoriesTags");
            AlterColumn("dbo.StoriesTags", "tagID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.StoriesTags", "tagID");
        }
    }
}
