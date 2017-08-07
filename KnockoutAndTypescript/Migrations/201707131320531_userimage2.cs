namespace KnockoutAndTypescript.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userimage2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Children", "UserImage", c => c.String());
            DropColumn("dbo.Goals", "UserImage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Goals", "UserImage", c => c.String());
            DropColumn("dbo.Children", "UserImage");
        }
    }
}
