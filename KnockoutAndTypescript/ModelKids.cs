namespace KnockoutAndTypescript
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using KnockoutAndTypescript.Models;

    public partial class ModelKids : DbContext
    {
        public ModelKids()
            : base("name=ModelKids")
        {
            
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
        }
        public DbSet<Child> Children { get; set; }
        public DbSet<Behaviour> Behaviour { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<PointAllocation> PointAllocation { get; set; }
        public DbSet<Points2> Points2 { get; set; }
        public DbSet<Behaviour2> Behaviour2 { get; set; }
        public DbSet<Bank> Bank { get; set; }
        public DbSet<ChildReward> ChildReward { get; set; }
        public DbSet<RewardAwarded> RewardAwarded { get; set; }
        public DbSet<Contributor> Contributor { get; set; }
    }
}
