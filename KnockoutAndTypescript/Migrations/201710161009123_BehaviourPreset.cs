namespace KnockoutAndTypescript.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BehaviourPreset : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Behaviours", "Preset", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Behaviours", "Preset");
        }
    }
}
