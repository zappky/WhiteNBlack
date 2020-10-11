namespace SmokeFreeApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userName : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Articles", "userName", c => c.String());
            AlterColumn("dbo.Stories", "userName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Stories", "userName", c => c.Int(nullable: false));
            AlterColumn("dbo.Articles", "userName", c => c.Int(nullable: false));
        }
    }
}
