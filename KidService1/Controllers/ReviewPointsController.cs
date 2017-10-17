using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using KnockoutAndTypescript.Models;
using System.Data.Entity;
using KnockoutAndTypescript.Business;
using KnockoutAndTypescript;
using System.Web.Http.Description;
using System.Resources;
using System.Reflection;
using KnockoutAndTypescript.ViewModels;

namespace KidService1.Controllers
{
    public class ReviewPointsController : ApiController
    {
        public BusinessRules BL;
        public ReviewPointsController()
        {
            BL = new BusinessRules();           
        }

        [ResponseType(typeof(KnockoutAndTypescript.ViewModels.PointReviewVM))]
        // public IEnumerable<string> Get()
        public IHttpActionResult Get()
        {           
                var allPoints = GetPointsAndDescriptionForAllNotFiltered();
                var unfiltered = GetReviewPointData(allPoints);
                var filtered = unfiltered.OrderBy(a => a.PointId).ToList();
                return Ok(filtered);
        }

        // GET: api/Points/5
        [ResponseType(typeof(KnockoutAndTypescript.ViewModels.PointReviewVM))]       
        public IHttpActionResult Get(int id)
        {
            var allPoints = GetPointsAndDescriptionForIdNotFiltered(id);
            var unfiltered = GetReviewPointData(allPoints);
            var filtered = unfiltered.OrderBy(a => a.PointId).ToList();
            return Ok(filtered);
        }

        

        [ResponseType(typeof(KnockoutAndTypescript.ViewModels.PointReviewVM))]
        public IHttpActionResult Get(int id, int lastpointid)
        {           
                var allPoints = GetPointsAndDescriptionForIdNotFiltered(id);
                var unfiltered = GetReviewPointData(allPoints);
                var filtered = unfiltered.Where(a => a.PointId > lastpointid).OrderBy(a=> a.PointId).ToList();

               // var allPoints = db.PointAllocation.Where(a => a.ChildId == id && a.Approved == true && a.Saved == false && a.PointId > lastpointid).ToList();
                return Ok(filtered);           
        }

        // POST: api/Points
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Points/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Points/5
        public void Delete(int id)
        {
        }

        private IEnumerable<PointAllocation> GetPointsAndDescriptionForIdNotFiltered(int id)
        {
            using (var db = new ModelKids())
            {
                var allPoints = db.PointAllocation.Where(a => a.ChildId == id).Include(i => i.Behaviour).ToList();
                return allPoints;
            }
        }

        private IEnumerable<PointAllocation> GetPointsAndDescriptionForAllNotFiltered()
        {
            using (var db = new ModelKids())
            {
                var allPoints = db.PointAllocation.Include(i => i.Behaviour).ToList();
                return allPoints;
            }
        }

        private IEnumerable<PointReviewVM> GetReviewPointData(IEnumerable<PointAllocation> data)
        {
            //var data = BL.GetPointsAndDescriptionForIdNotFiltered(id).Where(a => a.Saved == false).ToList().OrderByDescending(o => o.AllocationDate);
            var model = new List<PointReviewVM>();
           
            foreach (var x in data)
            {
                var p = new PointReviewVM();
                p.AllocationDate = x.AllocationDate.ToShortDateString();
                p.BehaviourName = x.Behaviour.BehaviourName;
                p.BehaviourId = x.BehaviourId;
                p.BehaviourPoints = x.Points;
                p.ChildId = x.ChildId;
                p.PointId = x.PointId;
                p.Saved = x.Saved;
                p.ChildName = BL.GetChildForId(p.ChildId).ChildName;
                p.Approved = x.Approved;
                if (x.ContributorId != 0)
                {
                    var c = BL.GetContributorForId(x.ContributorId);
                    p.Contributor = c.ContributorName;
                }
                else
                {
                    p.Contributor = User.Identity.Name;
                }
                model.Add(p);
            }
            return model;
        }
    }
}
