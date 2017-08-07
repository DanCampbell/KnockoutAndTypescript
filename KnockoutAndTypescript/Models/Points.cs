using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KnockoutAndTypescript.Models
{
    public class PointAllocation
    {
       
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PointId { get; set; }
        public int ChildId { get; set; }
        public DateTime AllocationDate { get; set; }
        public int Points { get; set; }
        public int BehaviourId { get; set; }
        public bool Saved { get; set; }
        public int ContributorId { get; set; }
        public bool Approved { get; set; }
        [ForeignKey("BehaviourId")]
        public Behaviour Behaviour { get; set; }
        //public virtual Behaviour Behaviour { get; set; }

    }    
}