namespace PhotoBook.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RatingTbl2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ratings", "Vote", c => c.Int(nullable: false));
            DropColumn("dbo.Ratings", "Like");
            DropColumn("dbo.Ratings", "Dislike");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ratings", "Dislike", c => c.Int(nullable: false));
            AddColumn("dbo.Ratings", "Like", c => c.Int(nullable: false));
            DropColumn("dbo.Ratings", "Vote");
        }
    }
}
