namespace PhotoBook.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewHasEffectColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Photos", "HasEffect", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Photos", "HasEffect");
        }
    }
}
