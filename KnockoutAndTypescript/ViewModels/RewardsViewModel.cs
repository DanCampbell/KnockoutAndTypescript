using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KnockoutAndTypescript.Models;

namespace KnockoutAndTypescript.ViewModels
{
    public class ChildRewardVM
    {
        
        public string GoalName { get; set; }
        public int GoalId { get; set; }
        public int GoalPoints { get; set; }
        public int GoalCompletePoints { get; set; }
        public int GoalPointsOriginal { get; set; }
        public bool WantReward { get; set; }
        public bool SliderVisible { get; set; }
        public bool Administrator { get; set; }
        public bool LimitedUser { get; set; }
        public bool GiveReward { get; set; }
        public bool RewardComplete { get; set; }
        public int ChildId { get; set; }
    }

    public class RewardAwardedVM
    {
        public int PointsAllocated { get; set; }
        public string GoalDescription { get; set; }
        public string RewardReceivedDate { get; set; }
    }

    public class RewardsViewModel
    {
        // add a bool to say whether points can be reallocated by child
       public  Child Child { get; set; } // Child has a list of rewards
       // List<Goal> Goals { get; set; } // Full set of Goals
       // List<ChildReward> ChildRewards {get;set;}
       public List<ChildRewardVM> ChildRewardsVM { get; set; } // flattened List of all Goals
       public bool Administrator { get; set; }
       public bool UserLevel { get; set; }
       public List<RewardAwardedVM> RewardsAwardedVM { get; set; } // List of Rewards Awarded
       public bool ShowRewards { get; set; }
       public bool ShowAllocation { get; set; }
    }
}