namespace KnockoutAndTypescript.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rewardrequested : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChildRewards", "RewardRequested", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ChildRewards", "RewardRequested");
        }
    }
}
