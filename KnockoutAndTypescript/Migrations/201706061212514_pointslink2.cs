namespace KnockoutAndTypescript.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pointslink2 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.PointAllocations");
            AlterColumn("dbo.PointAllocations", "PointId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.PointAllocations", "PointId");
            CreateIndex("dbo.PointAllocations", "PointId");
            AddForeignKey("dbo.PointAllocations", "PointId", "dbo.Behaviours", "BehaviourId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PointAllocations", "PointId", "dbo.Behaviours");
            DropIndex("dbo.PointAllocations", new[] { "PointId" });
            DropPrimaryKey("dbo.PointAllocations");
            AlterColumn("dbo.PointAllocations", "PointId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.PointAllocations", "PointId");
        }
    }
}
