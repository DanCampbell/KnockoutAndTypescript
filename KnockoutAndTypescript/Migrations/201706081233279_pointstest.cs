namespace KnockoutAndTypescript.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pointstest : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.PointAllocations", new[] { "PointId" });
            DropPrimaryKey("dbo.PointAllocations");
            AlterColumn("dbo.PointAllocations", "PointId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.PointAllocations", "PointId");
            CreateIndex("dbo.PointAllocations", "PointId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.PointAllocations", new[] { "PointId" });
            DropPrimaryKey("dbo.PointAllocations");
            AlterColumn("dbo.PointAllocations", "PointId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.PointAllocations", "PointId");
            CreateIndex("dbo.PointAllocations", "PointId");
        }
    }
}
