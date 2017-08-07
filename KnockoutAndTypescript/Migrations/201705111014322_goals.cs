namespace KnockoutAndTypescript.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class goals : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Goals",
                c => new
                    {
                        GoalId = c.Int(nullable: false, identity: true),
                        GoalName = c.String(),
                        GoalPointsRequired = c.Int(nullable: false),
                        AutoCreate = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.GoalId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Goals");
        }
    }
}
