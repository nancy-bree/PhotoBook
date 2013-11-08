namespace PhotoBook.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RatingTypeChanged2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ratings", "Like", c => c.Int(nullable: false));
            AddColumn("dbo.Ratings", "Dislike", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ratings", "Dislike");
            DropColumn("dbo.Ratings", "Like");
        }
    }
}
