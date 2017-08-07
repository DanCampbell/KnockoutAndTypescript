using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KnockoutAndTypescript.Models
{
    public class Points2
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Point2Id { get; set; }
        public int ChildId { get; set; }
        public DateTime AllocationDate { get; set; }
        public int Points { get; set; }

        
        public int BehaviourId { get; set; }
        [ForeignKey("BehaviourId")]
        public Behaviour2 Behaviour2 { get; set; }

    }
}