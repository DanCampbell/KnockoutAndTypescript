using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using KnockoutAndTypescript.ViewModels;
using KnockoutAndTypescript.Business;
using KnockoutAndTypescript;
using System.Data.Entity;
using System.Web.Http.Results;
using System.Web.Http.Description;
using System.Resources;
using System.Reflection;

namespace KidService1.Controllers
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

    public class DailySummaryController : ApiController
    {
        public BusinessRules BL;
        public DailySummaryController()
        {
            BL = new BusinessRules();

            try
            {
                //var resman = new ResourceManager("KidService1.Resource1.resx", Assembly.GetExecutingAssembly());

                //// Load the value of string value for Client
                //var strTest = resman.GetString("Test1");
            }
            catch(Exception ex)
            {

            }
        }


        // GET api/values
        [ResponseType(typeof(DailySummary))]
       // public IEnumerable<string> Get()
        public IHttpActionResult Get(int id)
        {
            using (var db = new ModelKids())
            {
                var dailySummery = new DailySummary();
                var allPoints = db.PointAllocation.Where(a => a.ChildId == id && a.Approved == true && a.Saved ==false).Sum( a=>(int?) a.Points);
                var negativePoints = db.PointAllocation.Where(a => a.ChildId == id && a.Approved == true && a.Saved == false && a.Points < 0 ).Sum(a => (int?)a.Points);
                var plusPoints = db.PointAllocation.Where(a => a.ChildId == id && a.Approved == true && a.Saved == false && a.Points > 0).Sum(a => (int?)a.Points);
                var notApprovedPoints = db.PointAllocation.Where(a => a.ChildId == id && a.Approved == true && a.Saved == false).Sum(a => (int?)a.Points);
                dailySummery.ChildId = id;
                dailySummery.ChildName = db.Children.Where(a => a.ChildId == id).First().ChildName;
                dailySummery.Date = DateTime.Now;
                dailySummery.MinusPoints = negativePoints.GetValueOrDefault();
                dailySummery.PlusPoints = plusPoints.GetValueOrDefault();
                dailySummery.TotalPoints = allPoints.GetValueOrDefault();
                switch (allPoints)
                {
                    case int c when allPoints < -200:
                        dailySummery.Status = "Very Naughty";break;
                    case int c when allPoints < -150:
                        dailySummery.Status = "Naughty"; break;
                    case int c when allPoints < -100:
                        dailySummery.Status = "Not good"; break;
                    case int c when allPoints < -50:
                        dailySummery.Status = "Can do better"; break;
                    case int c when allPoints < 0:
                        dailySummery.Status = "Need Improvement"; break;
                    case int c when allPoints > 250:
                        dailySummery.Status = "Star"; break;
                    case int c when allPoints > 200:
                        dailySummery.Status = "Fantastic"; break;
                    case int c when allPoints > 150:
                        dailySummery.Status = "Great"; break;
                    case int c when allPoints > 100:
                        dailySummery.Status = "Good"; break;
                    case int c when allPoints > 50:
                        dailySummery.Status = "Getting Better"; break;
                    case int c when allPoints > 0:
                        dailySummery.Status = "Can Achieve More"; break;
                    default: dailySummery.Status = "No Points"; break;
                }

                return Ok(dailySummery);

            }
            // return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        //[ResponseType(typeof(List<KnockoutAndTypescript.Models.Child>))]
        //public IHttpActionResult Get(int id)
        //{
        //   // BL = new BusinessRules();
        //    using (var db = new ModelKids())
        //    {
        //        var child = db.Children.FirstOrDefault(a => a.ChildId == id);
        //        return Ok(child);
               
        //    }
        //      //  var points = BL.GetChildForId(id);
        //   // return "value";
        //}

        // POST api/values
        public void Post([FromBody]string value)
     //   public void Post(string value)
        {
            var test = value;
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
