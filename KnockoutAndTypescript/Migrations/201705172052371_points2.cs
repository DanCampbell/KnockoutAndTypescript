namespace KnockoutAndTypescript.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class points2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PointAllocations", "ChildId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PointAllocations", "ChildId", c => c.String());
        }
    }
}
