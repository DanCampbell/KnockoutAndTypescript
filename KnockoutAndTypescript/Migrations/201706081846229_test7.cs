namespace KnockoutAndTypescript.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test7 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Points2");
            AddColumn("dbo.Points2", "Point2Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Points2", "Point2Id");
//DropColumn("dbo.Points2", "PointId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Points2", "PointId", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Points2");
            DropColumn("dbo.Points2", "Point2Id");
            AddPrimaryKey("dbo.Points2", "PointId");
        }
    }
}
