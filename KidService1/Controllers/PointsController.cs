using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using KnockoutAndTypescript.Models;
using KnockoutAndTypescript.Business;
using KnockoutAndTypescript;
using System.Web.Http.Description;
using System.Resources;
using System.Reflection;


namespace KidService1.Controllers
{
    public class PointsController : ApiController
    {
        public BusinessRules BL;
        public PointsController()
        {
            BL = new BusinessRules();

            try
            {
                //var resman = new ResourceManager("KidService1.Resource1", Assembly.GetExecutingAssembly());

                //// Load the value of string value for Client
                //var strTest = resman.GetString("Test1");
                //List<PointAllocation> p = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PointAllocation>>(strTest);
            }
            catch (Exception ex)
            {

            }
        }

        [ResponseType(typeof(KnockoutAndTypescript.Models.PointAllocation))]
        // public IEnumerable<string> Get()
        public IHttpActionResult Get()
        {
            using (var db = new ModelKids())
            {
                var allPoints = db.PointAllocation.ToList();
                return Ok(allPoints);

            }
            // return new string[] { "value1", "value2" };
        }

        // GET: api/Points/5
        [ResponseType(typeof(KnockoutAndTypescript.Models.PointAllocation))]       
        public IHttpActionResult Get(int id)
        {
            using (var db = new ModelKids())
            {
                var allPoints = db.PointAllocation.Where(a => a.ChildId == id && a.Approved == true && a.Saved == false).ToList();
                return Ok(allPoints);

            }
        }

        [ResponseType(typeof(KnockoutAndTypescript.Models.PointAllocation))]
        public IHttpActionResult Get(int id, int lastpointid)
        {
            using (var db = new ModelKids())
            {
                var allPoints = db.PointAllocation.Where(a => a.ChildId == id && a.Approved == true && a.Saved == false && a.PointId > lastpointid).ToList();
                return Ok(allPoints);

            }
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
    }
}
