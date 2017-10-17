using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KnockoutAndTypescript.Models;
using KnockoutAndTypescript.Business;
using KnockoutAndTypescript;
using System.Data.Entity;
using System.Web.Http.Results;
using System.Web.Http.Description;
using System.Resources;
using System.Reflection;

namespace KidService1.Controllers
{
    public class UserDetailsController : ApiController
    {
        public BusinessRules BL;
        public UserDetailsController()
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
        [ResponseType(typeof(KnockoutAndTypescript.Models.Child))]
       // public IEnumerable<string> Get()
        public IHttpActionResult Get()
        {
            using (var db = new ModelKids())
            {
                var allKids = db.Children.ToList();
                return Ok(allKids);

            }
            // return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [ResponseType(typeof(List<KnockoutAndTypescript.Models.Child>))]
        public IHttpActionResult Get(int id)
        {
           // BL = new BusinessRules();
            using (var db = new ModelKids())
            {
                var child = db.Children.FirstOrDefault(a => a.ChildId == id);
                return Ok(child);
               
            }
              //  var points = BL.GetChildForId(id);
           // return "value";
        }

        [ResponseType(typeof(List<KnockoutAndTypescript.Models.Child>))]
        public IHttpActionResult Get(string parent, string code)
        {
            // BL = new BusinessRules();
            using (var db = new ModelKids())
            {
                var child = db.Children.FirstOrDefault(a => a.ParentUser == parent && a.UserAuthCode == code);
                return Ok(child);

            }
            //  var points = BL.GetChildForId(id);
            // return "value";
        }

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
