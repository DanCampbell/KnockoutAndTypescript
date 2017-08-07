namespace KnockoutAndTypescript.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class retypecontributor : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PointAllocations", "ContributorId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PointAllocations", "ContributorId", c => c.Int());
        }
    }
}
