using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KnockoutAndTypescript.Models
{
    public class Child
    {
        public int ChildId { get; set; }
        public string ChildName { get; set; }
        public int ChildAge { get; set; }
        public bool ChildSex { get; set; }
        public int BankedPoints { get; set; }
        public string UserImage { get; set; }
        public string ParentUser { get; set; }
        public string UserAuthCode { get; set; }
        public bool CanBank { get; set; }

        public ICollection<ChildReward> ChildRewards { get; set; }
        // public  Bank Bank { get; set; }
    }

    
}