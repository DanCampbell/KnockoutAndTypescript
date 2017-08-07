using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KnockoutAndTypescript.ViewModels
{
    public class PointReviewVM
    {
        public int PointId { get; set; }
        public int ChildId { get; set; }
        public string AllocationDate { get; set; }
        public int Points { get; set; }
        public int BehaviourId { get; set; }
        public string BehaviourName { get; set; }
        public int BehaviourPoints { get; set; }
        public bool Saved { get; set; }
        public string Contributor { get; set; }
        public string ChildName { get; set; }
        public bool Approved { get; set; }
    }
}

