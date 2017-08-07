namespace KnockoutAndTypescript.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class approval : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PointAllocations", "Approved", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PointAllocations", "Approved");
        }
    }
}
