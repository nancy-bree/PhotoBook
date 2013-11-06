namespace PhotoBook.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeVotecolumntype : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Ratings", "Vote", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ratings", "Vote", c => c.Byte(nullable: false));
        }
    }
}
