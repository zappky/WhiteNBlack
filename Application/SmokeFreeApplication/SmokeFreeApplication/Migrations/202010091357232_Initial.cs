namespace SmokeFreeApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 128),
                        password = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        articleID = c.Int(nullable: false, identity: true),
                        articlePicture = c.String(),
                        articleStatus = c.String(),
                        userName = c.Int(nullable: false),
                        title = c.String(),
                        body = c.String(),
                        postDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.articleID);
            
            CreateTable(
                "dbo.ArticlesTags",
                c => new
                    {
                        tagID = c.Int(nullable: false, identity: true),
                        articleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.tagID);
            
            CreateTable(
                "dbo.BroadcastMessages",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        title = c.String(),
                        content = c.String(),
                        ownerName = c.String(),
                        postTime = c.DateTime(nullable: false),
                        expiresTime = c.DateTime(nullable: false),
                        neverShowAgain = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        commentID = c.Int(nullable: false, identity: true),
                        parentType = c.String(),
                        parentID = c.Int(nullable: false),
                        body = c.String(maxLength: 2000),
                        postDate = c.DateTime(nullable: false),
                        userName = c.String(),
                    })
                .PrimaryKey(t => t.commentID);
            
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        userName = c.String(nullable: false, maxLength: 50),
                        workLocation = c.String(nullable: false),
                        description = c.String(),
                        contactNo = c.Int(nullable: false),
                        doctorID = c.String(nullable: false, maxLength: 10),
                        adminVerify = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.userName);
            
            CreateTable(
                "dbo.GeneralUsers",
                c => new
                    {
                        userName = c.String(nullable: false, maxLength: 50),
                        name = c.String(nullable: false, maxLength: 100),
                        email = c.String(nullable: false),
                        password = c.String(nullable: false, maxLength: 100),
                        dateOfBirth = c.DateTime(nullable: false),
                        gender = c.String(nullable: false),
                        profilePicture = c.String(),
                    })
                .PrimaryKey(t => t.userName);
            
            CreateTable(
                "dbo.InterestedParties",
                c => new
                    {
                        userName = c.String(nullable: false, maxLength: 50),
                        smokerOrNot = c.Boolean(nullable: false),
                        bio = c.String(),
                    })
                .PrimaryKey(t => t.userName);
            
            CreateTable(
                "dbo.StoriesTags",
                c => new
                    {
                        tagID = c.Int(nullable: false, identity: true),
                        storyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.tagID);
            
            CreateTable(
                "dbo.Stories",
                c => new
                    {
                        storyID = c.Int(nullable: false, identity: true),
                        userName = c.Int(nullable: false),
                        title = c.String(),
                        body = c.String(),
                        postDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.storyID);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        tagID = c.Int(nullable: false, identity: true),
                        tagName = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.tagID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tags");
            DropTable("dbo.Stories");
            DropTable("dbo.StoriesTags");
            DropTable("dbo.InterestedParties");
            DropTable("dbo.GeneralUsers");
            DropTable("dbo.Doctors");
            DropTable("dbo.Comments");
            DropTable("dbo.BroadcastMessages");
            DropTable("dbo.ArticlesTags");
            DropTable("dbo.Articles");
            DropTable("dbo.Admins");
        }
    }
}
