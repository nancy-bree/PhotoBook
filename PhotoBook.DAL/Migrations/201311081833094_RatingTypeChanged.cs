namespace PhotoBook.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RatingTypeChanged : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Ratings", "Like");
            DropColumn("dbo.Ratings", "Dislike");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ratings", "Dislike", c => c.Byte(nullable: false));
            AddColumn("dbo.Ratings", "Like", c => c.Byte(nullable: false));
        }
    }
}
