namespace KnockoutAndTypescript.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Behaviour2",
                c => new
                    {
                        BehaviourId = c.Int(nullable: false, identity: true),
                        BehaviourName = c.String(),
                        BehaviourPoints = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BehaviourId);
            
            CreateTable(
                "dbo.Points2",
                c => new
                    {
                        PointId = c.Int(nullable: false),
                        ChildId = c.Int(nullable: false),
                        AllocationDate = c.DateTime(nullable: false),
                        Points = c.Int(nullable: false),
                        BehaviourId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PointId)
                .ForeignKey("dbo.Behaviour2", t => t.PointId)
                .Index(t => t.PointId);
            
            AddColumn("dbo.PointAllocations", "BehaviourId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Points2", "PointId", "dbo.Behaviour2");
            DropIndex("dbo.Points2", new[] { "PointId" });
            DropColumn("dbo.PointAllocations", "BehaviourId");
            DropTable("dbo.Points2");
            DropTable("dbo.Behaviour2");
        }
    }
}
