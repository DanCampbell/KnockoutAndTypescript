using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KnockoutAndTypescript.Models
{
    public class RewardAwarded
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RewardAwardedId { get; set; }
        public int ChildId { get; set; }
        public int GoalId { get; set; }
        public int PointsAllocated { get; set; }
        public string GoalDescription { get; set; }
        
        public DateTime RewardReceivedDate { get; set; }

       
    }
}