using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KnockoutAndTypescript;
using KnockoutAndTypescript.Models;
using KnockoutAndTypescript.Business;


namespace KnockoutAndTypescript.ViewModels
{
    public class ChildGraphViewModel
    {
        public int ChildId;
        public string ChildName;
        public List<int> sevenDays;
        public List<int> currentDay;
        public List<int> lastMonth;
        public List<PointsGraph> groupPointsForDay;
        public List<PointsGraph> groupPointsForWeek;
        public List<PointsGraph> groupPointsForMonth;
        public List<PointsPlusNegative> plusnegativeForWeek;
        public List<PointsPlusNegative> plusnegativeForMonth;
    }
}