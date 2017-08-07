using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KnockoutAndTypescript.Models
{
    public class Contributor
    {
        public int ContributorId { get; set; }
        public string ContributorName { get; set; }
        public string ParentUser { get; set; }        
        public string ContributorImage { get; set; }        
        public string ContributorAuthCode { get; set; }        
    }
}