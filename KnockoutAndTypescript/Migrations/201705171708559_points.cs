namespace KnockoutAndTypescript.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class points : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PointAllocations",
                c => new
                    {
                        PointId = c.Int(nullable: false, identity: true),
                        ChildId = c.String(),
                        AllocationDate = c.DateTime(nullable: false),
                        Points = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PointId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PointAllocations");
        }
    }
}
