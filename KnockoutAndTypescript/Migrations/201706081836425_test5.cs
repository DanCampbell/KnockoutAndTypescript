namespace KnockoutAndTypescript.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Points2", "PointId", "dbo.Behaviour2");
            DropIndex("dbo.Points2", new[] { "PointId" });
            DropColumn("dbo.Points2", "BehaviourId");
            RenameColumn(table: "dbo.Points2", name: "PointId", newName: "BehaviourId");
            AlterColumn("dbo.Points2", "BehaviourId", c => c.Int(nullable: false));
            CreateIndex("dbo.Points2", "BehaviourId");
            AddForeignKey("dbo.Points2", "BehaviourId", "dbo.Behaviour2", "BehaviourId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Points2", "BehaviourId", "dbo.Behaviour2");
            DropIndex("dbo.Points2", new[] { "BehaviourId" });
            AlterColumn("dbo.Points2", "BehaviourId", c => c.Int(nullable: false, identity: true));
            RenameColumn(table: "dbo.Points2", name: "BehaviourId", newName: "PointId");
            AddColumn("dbo.Points2", "BehaviourId", c => c.Int(nullable: false));
            CreateIndex("dbo.Points2", "PointId");
            AddForeignKey("dbo.Points2", "PointId", "dbo.Behaviour2", "BehaviourId");
        }
    }
}
