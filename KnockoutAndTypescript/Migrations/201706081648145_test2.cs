namespace KnockoutAndTypescript.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Points2", new[] { "PointId" });
            DropPrimaryKey("dbo.Points2");
            AlterColumn("dbo.Points2", "PointId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Points2", "PointId");
            CreateIndex("dbo.Points2", "PointId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Points2", new[] { "PointId" });
            DropPrimaryKey("dbo.Points2");
            AlterColumn("dbo.Points2", "PointId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Points2", "PointId");
            CreateIndex("dbo.Points2", "PointId");
        }
    }
}
