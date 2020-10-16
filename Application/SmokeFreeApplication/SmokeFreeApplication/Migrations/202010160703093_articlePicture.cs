namespace SmokeFreeApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class articlePicture : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Articles", "articlePicture", c => c.Binary());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Articles", "articlePicture", c => c.String());
        }
    }
}
