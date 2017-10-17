using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace KnockoutAndTypescript.Models
{
    public class Behaviour
    {
        //[ForeignKey("Student")]
        public int BehaviourId { get; set; }
        public string BehaviourName { get; set; }
        public int BehaviourPoints { get; set; }
        public string ParentUser { get; set; }
        public bool Preset { get; set; }

        public ICollection<PointAllocation> Points { get; set; }
        //public virtual PointAllocation Points { get; set; }
        // public virtual PointAllocation PointAllocation { get; set; }
    }
}