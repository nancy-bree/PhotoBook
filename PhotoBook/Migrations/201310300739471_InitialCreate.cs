namespace PhotoBook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Filename = c.String(nullable: false),
                        Description = c.String(),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PhotoID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        Vote = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PhotoTag",
                c => new
                    {
                        PhotoID = c.Int(nullable: false),
                        TagID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PhotoID, t.TagID })
                .ForeignKey("dbo.Photos", t => t.PhotoID, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagID, cascadeDelete: true)
                .Index(t => t.PhotoID)
                .Index(t => t.TagID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.PhotoTag", new[] { "TagID" });
            DropIndex("dbo.PhotoTag", new[] { "PhotoID" });
            DropIndex("dbo.Photos", new[] { "UserID" });
            DropForeignKey("dbo.PhotoTag", "TagID", "dbo.Tags");
            DropForeignKey("dbo.PhotoTag", "PhotoID", "dbo.Photos");
            DropForeignKey("dbo.Photos", "UserID", "dbo.User");
            DropTable("dbo.PhotoTag");
            DropTable("dbo.User");
            DropTable("dbo.Ratings");
            DropTable("dbo.Tags");
            DropTable("dbo.Photos");
        }
    }
}
