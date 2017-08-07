namespace KnockoutAndTypescript.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pointssaved : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PointAllocations", "Saved", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PointAllocations", "Saved");
        }
    }
}
