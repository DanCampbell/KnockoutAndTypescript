using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace KnockoutAndTypescript.Models
{
    public class PointSignal
    {
        [Key]       
        public int ChildId { get; set; }
        public string ChildName { get; set; }
        public int Points { get; set; }
        public int GoodPoints { get; set; }
        public int BadPoints { get; set; }
    }
}