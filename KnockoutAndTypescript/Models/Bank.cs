using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KnockoutAndTypescript.Models
{
    public class Bank
    {
        [Key]
       // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BankId { get; set; }
        
        public int BankedPoints { get; set; }
       // [ForeignKey("Child")]
        //public int ChildId { get; set; }
      //  public Child Child { get; set; }
    }
}