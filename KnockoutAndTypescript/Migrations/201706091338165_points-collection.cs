namespace KnockoutAndTypescript.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pointscollection : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PointAllocations", "PointId", "dbo.Behaviours");
            //DropIndex("dbo.PointAllocations", new[] { "PointId" });
            //DropColumn("dbo.PointAllocations", "BehaviourId");
            //RenameColumn(table: "dbo.PointAllocations", name: "PointId", newName: "BehaviourId");
            //AlterColumn("dbo.PointAllocations", "BehaviourId", c => c.Int(nullable: false));
            //CreateIndex("dbo.PointAllocations", "BehaviourId");
            AddForeignKey("dbo.PointAllocations", "BehaviourId", "dbo.Behaviours", "BehaviourId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PointAllocations", "BehaviourId", "dbo.Behaviours");
            DropIndex("dbo.PointAllocations", new[] { "BehaviourId" });
            AlterColumn("dbo.PointAllocations", "BehaviourId", c => c.Int(nullable: false, identity: true));
            RenameColumn(table: "dbo.PointAllocations", name: "BehaviourId", newName: "PointId");
            AddColumn("dbo.PointAllocations", "BehaviourId", c => c.Int(nullable: false));
            CreateIndex("dbo.PointAllocations", "PointId");
            AddForeignKey("dbo.PointAllocations", "PointId", "dbo.Behaviours", "BehaviourId");
        }
    }
}
