namespace KnockoutAndTypescript.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class behaviour : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Behaviours",
                c => new
                    {
                        BehaviourId = c.Int(nullable: false, identity: true),
                        BehaviourName = c.String(),
                        BehaviourPoints = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BehaviourId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Behaviours");
        }
    }
}
