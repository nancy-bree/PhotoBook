namespace PhotoBook.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RatingTbl : DbMigration
    {
        public override void Up()
        {
            AddForeignKey("dbo.Ratings", "UserID", "dbo.Users", "ID");
            AddForeignKey("dbo.Ratings", "PhotoID", "dbo.Photos", "ID", cascadeDelete: true);
            CreateIndex("dbo.Ratings", "UserID");
            CreateIndex("dbo.Ratings", "PhotoID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Ratings", new[] { "PhotoID" });
            DropIndex("dbo.Ratings", new[] { "UserID" });
            DropForeignKey("dbo.Ratings", "PhotoID", "dbo.Photos");
            DropForeignKey("dbo.Ratings", "UserID", "dbo.Users");
        }
    }
}
