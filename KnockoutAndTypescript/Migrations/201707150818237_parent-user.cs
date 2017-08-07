namespace KnockoutAndTypescript.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class parentuser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Behaviours", "ParentUser", c => c.String());
            AddColumn("dbo.Children", "ParentUser", c => c.String());
            AddColumn("dbo.Goals", "ParentUser", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Goals", "ParentUser");
            DropColumn("dbo.Children", "ParentUser");
            DropColumn("dbo.Behaviours", "ParentUser");
        }
    }
}
