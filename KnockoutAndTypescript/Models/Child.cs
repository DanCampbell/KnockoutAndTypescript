using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KnockoutAndTypescript.Models
{
    public class Goal
    {
        public int GoalId { get; set; }
        public string GoalName { get; set; }
        public int GoalPointsRequired { get; set; }
        public bool AutoCreate { get; set; }
        public string ParentUser { get; set; }
        
        public ICollection<ChildReward> ChildRewards { get; set; }
    }

    
}