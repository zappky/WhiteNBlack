namespace SmokeFreeApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changecigpricetype : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Progresses", "cigaPrice", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Progresses", "cigaPrice", c => c.Int(nullable: false));
        }
    }
}
