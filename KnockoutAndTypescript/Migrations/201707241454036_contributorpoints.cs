namespace KnockoutAndTypescript.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class contributorpoints : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PointAllocations", "ContributorId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PointAllocations", "ContributorId");
        }
    }
}
