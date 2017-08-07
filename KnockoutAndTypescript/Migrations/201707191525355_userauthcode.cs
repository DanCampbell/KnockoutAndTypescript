namespace KnockoutAndTypescript.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userauthcode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Children", "UserAuthCode", c => c.String());
            AddColumn("dbo.Children", "CanBank", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Children", "CanBank");
            DropColumn("dbo.Children", "UserAuthCode");
        }
    }
}
