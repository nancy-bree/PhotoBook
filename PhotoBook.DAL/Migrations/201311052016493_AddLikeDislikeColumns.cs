namespace PhotoBook.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLikeDislikeColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ratings", "Like", c => c.Byte(nullable: false));
            AddColumn("dbo.Ratings", "Dislike", c => c.Byte(nullable: false));
            DropColumn("dbo.Ratings", "Vote");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ratings", "Vote", c => c.Int(nullable: false));
            DropColumn("dbo.Ratings", "Dislike");
            DropColumn("dbo.Ratings", "Like");
        }
    }
}
