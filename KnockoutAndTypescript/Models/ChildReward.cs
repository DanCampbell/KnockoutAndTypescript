using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KnockoutAndTypescript.Models
{
    public class ChildReward
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ChildRewardId { get; set; }
        public int ChildId { get; set; }
        public int GoalId { get; set; }
        public int PointsAllocated { get; set; }
        public bool RewardComplete { get; set; }
        public bool RewardReceived { get; set; }
        public bool RewardRequested { get; set; }
        public DateTime RewardReceivedDate { get; set; }

        [ForeignKey("GoalId")]
        public Goal Goal { get; set; }

        [ForeignKey("ChildId")]
        public Child Child { get; set; }


    }
}