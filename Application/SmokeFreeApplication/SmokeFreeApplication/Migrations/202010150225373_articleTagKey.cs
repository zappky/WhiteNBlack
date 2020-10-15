namespace SmokeFreeApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class articleTagKey : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ArticlesTags");
            AlterColumn("dbo.ArticlesTags", "tagID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.ArticlesTags", new[] { "tagID", "articleID" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.ArticlesTags");
            AlterColumn("dbo.ArticlesTags", "tagID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.ArticlesTags", "tagID");
        }
    }
}
