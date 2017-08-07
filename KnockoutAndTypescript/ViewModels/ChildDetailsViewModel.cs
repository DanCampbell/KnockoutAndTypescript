using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KnockoutAndTypescript.Models;

namespace KnockoutAndTypescript.ViewModels
{
    public class ChildDetailsViewModel
    {
        public ChildDetailsViewModel()
        {
         // Points = new List<int>();
        }
        public int PointId { get; set; }
        public int ChildId { get; set; }
        public string AllocationDate { get; set; }
        public int Points { get; set; }
        public int BehaviourId { get; set; }
        public string BehaviourName { get; set; }
        public int BehaviourPoints { get; set; }
        public bool Saved { get; set; }
        public string UserImage { get; set; }
    }

    public class BehaviourViewModel
    {
        public int BehaviourId { get; set; }
        public string BehaviourName { get; set; }
        public int BehaviourPoints { get; set; }
    }


    public class ChildDetailsViewModelList
    {
        public ChildDetailsViewModelList()
        {
            ChildVM = new List<ChildDetailsViewModel>();
        }

        public List<ChildDetailsViewModel> ChildVM { get; set; }
    }
}