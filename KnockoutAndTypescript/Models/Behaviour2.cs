using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KnockoutAndTypescript.Models
{
    public class Behaviour2
    {        
        [Key]
        public int BehaviourId { get; set; }
        public string BehaviourName { get; set; }
        public int BehaviourPoints { get; set; }

        public ICollection<Points2> Points2s { get; set; }
    }
}