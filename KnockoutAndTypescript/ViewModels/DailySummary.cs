using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KnockoutAndTypescript.Models;

namespace KnockoutAndTypescript.ViewModels
{
    public class DailySummary
    {  
        public int ChildId { get; set; }
        public string ChildName { get; set; }
        public DateTime Date { get; set; }
        public int TotalPoints { get; set; }
        public int MinusPoints { get; set; }
        public int PlusPoints { get; set; }
        public int NotApprovedPoints { get; set; }
        public int RewardsCompleted { get; set; }
        public string Status { get; set; } // Very Good, Naughty etc

    }

   
}