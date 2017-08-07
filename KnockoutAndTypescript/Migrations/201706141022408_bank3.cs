namespace KnockoutAndTypescript.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bank3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Banks", "ChildId", "dbo.Children");
            DropIndex("dbo.Banks", new[] { "ChildId" });
            AddColumn("dbo.Children", "BankedPoints", c => c.Int(nullable: false));
            DropColumn("dbo.Banks", "ChildId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Banks", "ChildId", c => c.Int(nullable: false));
            DropColumn("dbo.Children", "BankedPoints");
            CreateIndex("dbo.Banks", "ChildId");
            AddForeignKey("dbo.Banks", "ChildId", "dbo.Children", "ChildId", cascadeDelete: true);
        }
    }
}
