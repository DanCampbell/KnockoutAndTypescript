namespace KnockoutAndTypescript.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userimage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Goals", "UserImage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Goals", "UserImage");
        }
    }
}
