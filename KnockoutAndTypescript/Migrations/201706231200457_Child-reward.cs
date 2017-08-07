namespace KnockoutAndTypescript.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Childreward : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChildRewards",
                c => new
                    {
                        ChildRewardId = c.Int(nullable: false, identity: true),
                        ChildId = c.Int(nullable: false),
                        GoalId = c.Int(nullable: false),
                        PointsAllocated = c.Int(nullable: false),
                        RewardComplete = c.Boolean(nullable: false),
                        RewardReceived = c.Boolean(nullable: false),
                        RewardReceivedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ChildRewardId)
                .ForeignKey("dbo.Children", t => t.ChildId, cascadeDelete: true)
                .ForeignKey("dbo.Goals", t => t.GoalId, cascadeDelete: true)
                .Index(t => t.ChildId)
                .Index(t => t.GoalId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChildRewards", "GoalId", "dbo.Goals");
            DropForeignKey("dbo.ChildRewards", "ChildId", "dbo.Children");
            DropIndex("dbo.ChildRewards", new[] { "GoalId" });
            DropIndex("dbo.ChildRewards", new[] { "ChildId" });
            DropTable("dbo.ChildRewards");
        }
    }
}
