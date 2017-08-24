using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KnockoutAndTypescript.ViewModels
{
    public class AllChildrenGraphViewModel
    {
        public AllChildrenGraphViewModel()
        {
            ChildGraphList = new List<ChildGraphViewModel>();
        }
        public ChildGraphViewModel ChildGraph { get; set; }
        public List<ChildGraphViewModel> ChildGraphList { get; set; }
    }
}