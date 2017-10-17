using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KnockoutAndTypescript.Business;
using KnockoutAndTypescript.Models;
using System.Web.WebSockets;
using Microsoft.AspNet.SignalR;
using KnockoutAndTypescript.ViewModels;
using KnockoutAndTypescript.Hubs;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Concurrent;
using System.Threading;

namespace KnockoutAndTypescript.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
            BL = new BusinessRules();
            TestCurrentUser = "b@b.com";
        }

        protected BusinessRules BL;
        protected string TestCurrentUser;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Chat()
        {
            return View();
        }

        public ActionResult GraphTest()
        {
            return View();
        }
        public ActionResult GuageTest()
        {
            return View();
        }

        public ActionResult GaugeBehaviour(int id =1)
        {
            var points = BL.GetPointsValueForId(id);
            ViewBag.points = points;
            ViewBag.rangestart = -200;
            ViewBag.rangeend = 200;
            return View();
        }

        [System.Web.Mvc.Authorize]
        public ActionResult GaugeBehaviourAll()
        {
            string current_user = User.Identity.Name;
            var model = BL.GetPointsValueForAll(current_user);
           // ViewBag.points = points;
            ViewBag.rangestart = -200;
            ViewBag.rangeend = 200;
            return View(model);
        }

        public ActionResult PointsTicker()
        {
            return View();
        }

        public ActionResult UploadUserImage(int id = 1)
        {
            var model = BL.GetChildForId(id);
            return View(model);
        }

        public ActionResult UploadContributorImage(int id = 1)
        {
            var model = BL.GetContributorForId(id);
            return View(model);
        }

        public ActionResult SaveUserUploadedFile()
        {
            bool isSavedSuccessfully = true;
            string fName = "";
            string useridstr = Request.Files.Keys[0];
            int userid = int.Parse(useridstr);
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    //Save file content goes here
                    fName = file.FileName;

                    if (file != null && file.ContentLength > 0)
                    {
                        //  var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\Content\\UserImages", Server.MapPath(@"\")));
                        var originalDirectory = new DirectoryInfo(string.Format("{0}Content\\UserImages", Server.MapPath(@"\")));

                        string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "imagepath");

                        var fileName1 = Path.GetFileName(file.FileName);

                        bool isExists = System.IO.Directory.Exists(pathString);

                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);

                        //  var path = string.Format("{0}\\{1}", pathString, fName);
                        var path = string.Format("{0}\\{1}", pathString, file.FileName);

                        file.SaveAs(path);
                        string uripath = "/Content/UserImages/imagepath/" + file.FileName;
                        BL.UpdateChildImage(userid, uripath);
                    }

                }

            }
            catch (Exception ex)
            {
                isSavedSuccessfully = false;
            }


            if (isSavedSuccessfully)
            {
                return Json(new { Message = fName });
            }
            else
            {
                return Json(new { Message = "Error in saving file" });
            }
        }

        public ActionResult SaveContributorUploadedFile()
        {
            bool isSavedSuccessfully = true;
            string fName = "";
            string useridstr = Request.Files.Keys[0];
            int userid = int.Parse(useridstr);
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    //Save file content goes here
                    fName = file.FileName;

                    if (file != null && file.ContentLength > 0)
                    {
                        var originalDirectory = new DirectoryInfo(string.Format("{0}Content\\ContributorImages", Server.MapPath(@"\")));

                        string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "imagepath");

                        var fileName1 = Path.GetFileName(file.FileName);

                        bool isExists = System.IO.Directory.Exists(pathString);

                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);

                        var path = string.Format("{0}\\{1}", pathString, file.FileName);

                        file.SaveAs(path);
                        string uripath = "/Content/ContributorImages/imagepath/" + file.FileName;
                        BL.UpdateContributorImage(userid, uripath);
                    }

                }

            }
            catch (Exception ex)
            {
                isSavedSuccessfully = false;
            }


            if (isSavedSuccessfully)
            {
                return Json(new { Message = fName });
            }
            else
            {
                return Json(new { Message = "Error in saving file" });
            }
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            //var hubConnection = new Microsoft.AspNet.SignalR.HubConnection("http://your_end_point:port/");

            return View();
        }
        // public class testservice{

        public async Task<AllChildrenGraphViewModel> GetComparisonGraphAsync(int ChildId)
        {
            var model = new ChildGraphViewModel();
            var rtn = new AllChildrenGraphViewModel();
            //var p1 = BL.GetGroupedPointsforDayGraphAsync(ChildId);
            //var p2 = BL.GetPlusNegativePointsforLastMonthAsync(ChildId);
            //var p3 = BL.GetGroupedPointsforWeekGraphAsync(ChildId);
            //var p4 = BL.GetPointsforLastSevenDaysAsync(ChildId);
            var p5 = await BL.GetPointsforDayGraphAsync(ChildId);
            //var p6 = BL.GetPointsforLastMonthAsync(ChildId);
            //var p7 = BL.GetPlusNegativePointsforLastWeekAsync(ChildId);


            //  Task.WhenAll( p5);
            //model.groupPointsForDay = p1.Result;
            //model.plusnegativeForMonth = p2.Result;
            //model.groupPointsForWeek = p3.Result;
            //model.sevenDays = p4.Result;
            model.currentDay = p5;// p5.Result;
                //model.lastMonth = p6.Result;
                //model.plusnegativeForWeek = p7.Result;
            rtn.ChildGraph = model;
           
            return rtn;
        }

        public async Task<AllChildrenGraphViewModel> GetGraphAsync(int ChildId, BusinessRules BL, bool bGetAll)
        {
            var model = new ChildGraphViewModel();
            var rtn = new AllChildrenGraphViewModel();
            var p1 = BL.GetGroupedPointsforDayGraphAsync(ChildId);
            var p2 = BL.GetPlusNegativePointsforLastMonthAsync(ChildId);
            var p3 = BL.GetGroupedPointsforWeekGraphAsync(ChildId);
            var p4 = BL.GetPointsforLastSevenDaysAsync(ChildId);
            var p5 = BL.GetPointsforDayGraphAsync(ChildId);
            var p6 = BL.GetPointsforLastMonthAsync(ChildId);
            var p7 = BL.GetPlusNegativePointsforLastWeekAsync(ChildId);
            if (bGetAll == true)
            {
                var p8 = GetAllChildrenGraphDataAsync();
                Task.WhenAll(p1, p2, p3, p4, p5, p6, p7,p8);
                // var allchildren = await GetAllChildrenGraphDataAsync();
                //  rtn.ChildGraphList = allchildren;
                model.groupPointsForDay = p1.Result;
                model.plusnegativeForMonth = p2.Result;
                model.groupPointsForWeek = p3.Result;
                model.sevenDays = p4.Result;
                model.currentDay = p5.Result;
                model.lastMonth = p6.Result;
                model.plusnegativeForWeek = p7.Result;
                var allchildren = p8.Result;
                rtn.ChildGraphList = allchildren;
            }
            else
            {
                Task.WhenAll(p1, p2, p3, p4, p5, p6, p7);
                model.groupPointsForDay = p1.Result;
                model.plusnegativeForMonth = p2.Result;
                model.groupPointsForWeek = p3.Result;
                model.sevenDays = p4.Result;
                model.currentDay = p5.Result;
                model.lastMonth = p6.Result;
                model.plusnegativeForWeek = p7.Result;
            }

          //  Task.WhenAll(p1, p2, p3, p4, p5, p6, p7);

            //model.groupPointsForDay = p1.Result;
            //model.plusnegativeForMonth = p2.Result;
            //model.groupPointsForWeek = p3.Result;
            //model.sevenDays = p4.Result;
            //model.currentDay = p5.Result;
            //model.lastMonth = p6.Result;
            //model.plusnegativeForWeek = p7.Result;

             ////model.groupPointsForDay = await BL.GetGroupedPointsforDayGraphAsync(ChildId);
             ////model.plusnegativeForMonth = await BL.GetPlusNegativePointsforLastMonthAsync(ChildId);
             ////model.groupPointsForWeek = await BL.GetGroupedPointsforWeekGraphAsync(ChildId);
             ////model.sevenDays = await BL.GetPointsforLastSevenDaysAsync(ChildId);
             ////model.currentDay = await BL.GetPointsforDayGraphAsync(ChildId);
             ////model.lastMonth = await BL.GetPointsforLastMonthAsync(ChildId);
             ////model.plusnegativeForWeek = await BL.GetPlusNegativePointsforLastWeekAsync(ChildId);
             rtn.ChildGraph = model;
            //if (bGetAll == true) {
            //   // var allchildren = await GetAllChildrenGraphDataAsync();
            //   var allchildren = p8.
            //    rtn.ChildGraphList = allchildren;
            //}
            return rtn;
        }
        //  }
       // public class tservice{
        public async Task<int> GetNumber()
        {

            //Thread.Sleep(1000);
            var p2 = BL.GetPlusNegativePointsforLastMonthAsync(1);
            //var r = p2.Result;
            return 1;
        }

        public async Task<int> GetNumber2()
        {
            //Thread.Sleep(1000);
            var p2 = BL.GetPlusNegativePointsforLastMonthAsync(1);
            //var r = p2.Result;
            //await Task.Delay(3000);
            return 2;
        }
        public async Task<int> GetNumber3()
        {
            // Thread.Sleep(1000);
            // await Task.Delay(3000);
            var p2 =  BL.GetPlusNegativePointsforLastMonthAsync(1);
            //var r = p2.Result;
            return 3;

        }
   // }

        public async Task<int> GetSum2()
        {
//          var ts = new tservice();
            var s1 =   GetNumber();
            var s2 =  GetNumber2();
            var s3 =   GetNumber3();
            await Task.WhenAll(s1, s2, s3);

            var a =  s1.Result;
            var b =  s2.Result;
            var c =  s3.Result;

            return a + b + c;
        }

        public async Task<int> GetSum()
        {
            //          var ts = new tservice();
            var a = await GetNumber();
            var b = await GetNumber2();
            var c = await GetNumber3();
            // Task.WhenAll(s1, s2, s3);

            //var a = s1.Result;
            //var b = s2.Result;
            //var c = s3.Result;

            return a + b + c;
        }

        [OutputCache(Duration = 0)]
        public ActionResult Statistics(int Id)
        {
            var ChildId = Id;
            //Stopwatch s3 = new Stopwatch();
            //s3.Start();
            //Task<int> t = GetSum();
            //t.Wait();
            //var r = t.Result;

            //s3.Stop();
            //var dur = s3.ElapsedMilliseconds;

            //Stopwatch s4 = new Stopwatch();
            //s4.Start();
            //Task<int> t2 = GetSum2();
            //t.Wait();
            //var r2 = t.Result;

            //s4.Stop();
            //var dur2 = s4.ElapsedMilliseconds;


            //Task<List<PointsPlusNegative>> task1= await BL.GetPlusNegativePointsforLastMonth(ChildId);
            //Stopwatch s0 = new Stopwatch();
            //s0.Start();
            //var graphDataDayGroup = BL.GetGroupedPointsforDayGraph(ChildId);
            //var graphDataWeekGroup = BL.GetGroupedPointsforWeekGraph(ChildId);
            //var graphData = BL.GetPointsforLastSevenDays(ChildId);
            //var graphDataDay = BL.GetPointsforDayGraph(ChildId);
            //var graphDataMonth = BL.GetPointsforLastMonth(ChildId);
            //var pointsPlusNegativeForWeek = BL.GetPlusNegativePointsforLastWeek(ChildId);
            //List<PointsPlusNegative> pointsPlusNegativeForMonth = BL.GetPlusNegativePointsforLastMonth(ChildId);
            //var temp = GetAllChildrenGraphData();
            //s0.Stop();
            //var el0 = s0.ElapsedMilliseconds;
            //var testservice = new testservice();
            //var test2 = testservice.TestReturn(ChildId,BL);
            Stopwatch s1 = new Stopwatch();
            s1.Start();
            var modelasync = GetGraphAsync(ChildId, BL,true);
            s1.Stop();
            var el = s1.ElapsedMilliseconds;
            
            var model = new ChildGraphViewModel();
            model.ChildId = ChildId;
            model.ChildName = BL.GetChildForId(ChildId).ChildName;
            model.sevenDays = modelasync.Result.ChildGraph.sevenDays;// graphData;
            model.currentDay = modelasync.Result.ChildGraph.currentDay;// graphDataDay;
            model.lastMonth = modelasync.Result.ChildGraph.lastMonth;//graphDataMonth;
            model.groupPointsForDay = modelasync.Result.ChildGraph.groupPointsForDay;//graphDataDayGroup;
            model.groupPointsForWeek = modelasync.Result.ChildGraph.groupPointsForWeek;//graphDataWeekGroup;
            model.plusnegativeForWeek = modelasync.Result.ChildGraph.plusnegativeForWeek;
            model.plusnegativeForMonth = modelasync.Result.ChildGraph.plusnegativeForMonth;
            //var hubConnection = new Microsoft.AspNet.SignalR.HubConnection("http://your_end_point:port/");
            // ViewBag.AllChildren = GetAllChildrenGraphData();
            ViewBag.AllChildren = modelasync.Result.ChildGraphList;
            return View(model);
        }

        private async Task<List<ChildGraphViewModel>> GetAllChildrenGraphDataAsync()
        {
            var gcvm = new ConcurrentQueue<ChildGraphViewModel>();
            string current_user = User.Identity.Name;
            var model = BL.GetChildren().Where(a => a.ParentUser == current_user).ToList();
            //foreach (var child in model)
            Parallel.ForEach(model, (child) =>
            {                
                    var viewmodel = new ChildGraphViewModel();
                    viewmodel.ChildId = child.ChildId;
                    viewmodel.ChildName = child.ChildName;
                // var modelasync = await GetGraphAsync(child.ChildId, BL, false);
                //var modelasync = GetGraphAsync(child.ChildId, BL, false);
                var modelasync = GetComparisonGraphAsync(child.ChildId);
                //viewmodel.sevenDays = modelasync.ChildGraph.sevenDays;// graphData;
                //viewmodel.currentDay = modelasync.ChildGraph.currentDay;// graphDataDay;
                //viewmodel.lastMonth = modelasync.ChildGraph.lastMonth;//graphDataMonth;
                //viewmodel.groupPointsForDay = modelasync.ChildGraph.groupPointsForDay;//graphDataDayGroup;
                //viewmodel.groupPointsForWeek = modelasync.ChildGraph.groupPointsForWeek;//graphDataWeekGroup;
                //viewmodel.plusnegativeForWeek = modelasync.ChildGraph.plusnegativeForWeek;
                //viewmodel.plusnegativeForMonth = modelasync.ChildGraph.plusnegativeForMonth;

                //viewmodel.sevenDays = modelasync.Result.ChildGraph.sevenDays;// graphData;
                viewmodel.currentDay = modelasync.Result.ChildGraph.currentDay;// graphDataDay;
                //viewmodel.lastMonth = modelasync.Result.ChildGraph.lastMonth;//graphDataMonth;
                //viewmodel.groupPointsForDay = modelasync.Result.ChildGraph.groupPointsForDay;//graphDataDayGroup;
                //viewmodel.groupPointsForWeek = modelasync.Result.ChildGraph.groupPointsForWeek;//graphDataWeekGroup;
                //viewmodel.plusnegativeForWeek = modelasync.Result.ChildGraph.plusnegativeForWeek;
                //viewmodel.plusnegativeForMonth = modelasync.Result.ChildGraph.plusnegativeForMonth;

                gcvm.Enqueue(viewmodel);
                });

            //return GetAllChildrenGraphData();
            return gcvm.ToList();
            }

        

        private List<ChildGraphViewModel> GetAllChildrenGraphData()
        {
            string current_user = User.Identity.Name;
            var model = BL.GetChildren().Where(a => a.ParentUser == current_user).ToList();

            var gcvm = new List<ChildGraphViewModel>(); ;
            //foreach(var child in model)
            Parallel.ForEach(model, (child) =>
             {
                //var graphDataDayGroup = BL.GetGroupedPointsforDayGraph(child.ChildId);
                //var graphDataWeekGroup = BL.GetGroupedPointsforWeekGraph(child.ChildId);
                //var graphData = BL.GetPointsforLastSevenDays(child.ChildId);
                //var graphDataDay = BL.GetPointsforDayGraph(child.ChildId);
                //var graphDataMonth = BL.GetPointsforLastMonth(child.ChildId);
                //var pointsPlusNegativeForWeek = BL.GetPlusNegativePointsforLastWeek(child.ChildId);
                //var pointsPlusNegativeForMonth = BL.GetPlusNegativePointsforLastMonth(child.ChildId);
                var viewmodel = new ChildGraphViewModel();
                 viewmodel.ChildId = child.ChildId;
                 viewmodel.ChildName = child.ChildName;
                //viewmodel.sevenDays = graphData;
                //viewmodel.currentDay = graphDataDay;
                //viewmodel.lastMonth = graphDataMonth;
                //viewmodel.groupPointsForDay = graphDataDayGroup;
                //viewmodel.groupPointsForWeek = graphDataWeekGroup;
                //viewmodel.plusnegativeForWeek = pointsPlusNegativeForWeek;
                //viewmodel.plusnegativeForMonth = pointsPlusNegativeForMonth;
                var modelasync = GetGraphAsync(child.ChildId, BL, false);

                 viewmodel.sevenDays = modelasync.Result.ChildGraph.sevenDays;// graphData;
                viewmodel.currentDay = modelasync.Result.ChildGraph.currentDay;// graphDataDay;
                viewmodel.lastMonth = modelasync.Result.ChildGraph.lastMonth;//graphDataMonth;
                viewmodel.groupPointsForDay = modelasync.Result.ChildGraph.groupPointsForDay;//graphDataDayGroup;
                viewmodel.groupPointsForWeek = modelasync.Result.ChildGraph.groupPointsForWeek;//graphDataWeekGroup;
                viewmodel.plusnegativeForWeek = modelasync.Result.ChildGraph.plusnegativeForWeek;
                 viewmodel.plusnegativeForMonth = modelasync.Result.ChildGraph.plusnegativeForMonth;

                 gcvm.Add(viewmodel);
             });
            return gcvm;
        }

        [System.Web.Mvc.Authorize]
        public ActionResult Points(int id=-1)
        {
            ViewBag.Message = "Points page.";
            //  var test1 = BL.GetPoints2ForId(1);
            //  var test2 = Json(test1);
            ViewBag.UserSelected = id;
            Response.CacheControl = "no-cache";
            return View();
        }

        //System.Web.Mvc.Authorize]
        public ActionResult Contribute(int id = 1, string Parent= "a@a.com", string Contributor="1")
        {
            ViewBag.Message = "Contribute page.";
            var contributor = BL.GetContributorForCode(Contributor, Parent);
            if (contributor == null) return RedirectToAction("Index");
            ViewBag.UserSelected = id;
            ViewBag.Parent = Parent;
            ViewBag.Contributor = contributor.ContributorId;
            Response.CacheControl = "no-cache";
            return View();
        }

        public ActionResult Knockout()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult KnockoutTypescript()
        {
            ViewBag.Message = "Your Typescript page.";

            return View();
        }

        [System.Web.Mvc.Authorize]
        public ActionResult EditChild(int id=1)
        {
            string uri = Request.Url.AbsoluteUri;
            var idx = uri.Replace("EditChild", "ChildrenList");
            ViewBag.RtnUri = idx;
            var model = BL.GetChildForId(id);
            return View(model);
        }

        [System.Web.Mvc.Authorize]
        public ActionResult ChildrenList()
        {
            string uri = Request.Url.AbsoluteUri;
            var idx = uri.Replace("ChildrenList", "ChildDetail");
            ViewBag.ChildDetailUri = idx;
            
            string current_user = User.Identity.Name;
            ViewBag.ParentUser = current_user;
            var model = BL.GetChildren().Where( a=> a.ParentUser == current_user).ToList();
            return View(model);
        }

        [System.Web.Mvc.Authorize]
        public ActionResult ContributorList()
        {
            string uri = Request.Url.AbsoluteUri;
            var idx = uri.Replace("ContributorList", "Contribute");
            ViewBag.ContributorDetailUri = idx;
            string current_user = User.Identity.Name;
            ViewBag.Parent = current_user;
            var model = BL.GetContributors(current_user).ToList();
            return View(model);
        }

        [System.Web.Mvc.Authorize]
        public ActionResult ReviewPoints(int id = 1)
        {
            var data = BL.GetPointsAndDescriptionForIdNotFiltered(id).Where(a => a.Saved == false).ToList().OrderByDescending(o => o.AllocationDate);
            var model = new List<PointReviewVM>();
            ViewBag.ChildDetails = BL.GetChildForId(id);
            ViewBag.ReviewMode = "Child";

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

            return View(model);
        }

        [System.Web.Mvc.Authorize]
        public ActionResult ReviewContributorPoints( string parent, string contributor)
        {
            var cval = BL.GetContributorForCode(contributor, parent);
            var data = BL.GetPointsAndDescriptionForContributorId(cval.ContributorId).Where(a => a.Saved == false).ToList().OrderByDescending(o => o.AllocationDate);
            var model = new List<PointReviewVM>();
            //ViewBag.ChildDetails = BL.GetChildForId(id);
            Child tempc = new Child();
            tempc.ChildName = "Given By : "+ cval.ContributorName; // bodge
            ViewBag.ChildDetails = tempc;
            ViewBag.ReviewMode = "Contributor";

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
                p.Approved = x.Approved;
                p.ChildName = BL.GetChildForId(p.ChildId).ChildName;
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

            return View("ReviewPoints", model);
        }

        //[Route("ChildDetail /{ParentUser ?}/{Test ?")]
        public ActionResult ChildDetail(int? id, string Parent, string Code)
        {
           if(id == null)
            {
                return RedirectToAction("Index");
            }
           
            ViewBag.ChildDetails = BL.GetChildForId(id.GetValueOrDefault());
            //   var testreward = BL.GetChildRewardForId(1, 2);
            var testchild = BL.GetChildAndRewardsForId(id.GetValueOrDefault());
            if(testchild == null)
            {
                return RedirectToAction("Index");
            }
            if(testchild.UserAuthCode != Code || testchild.ParentUser != Parent)
            {
                return RedirectToAction("Index");
            }

            var s = BL.GetPointsAndDescriptionForIdNotFiltered(id.GetValueOrDefault()).ToList().OrderByDescending(o => o.AllocationDate);
           // var bank = BL.GetBankForId(id);
            ViewBag.ChildDetails = BL.GetChildForId(id.GetValueOrDefault());
            var vmlist = new ChildDetailsViewModelList();
            foreach ( var t in s) {
                ChildDetailsViewModel vm = new ChildDetailsViewModel();
                vm.AllocationDate = t.AllocationDate.ToString("D");
                vm.BehaviourId = t.BehaviourId;
                vm.ChildId = t.ChildId;
                vm.PointId = t.PointId;
                vm.Points = t.Points;
                vm.Saved = t.Saved;
                vm.BehaviourName = t.Behaviour.BehaviourName;
                //vm.BehaviourId = t.BehaviourId;
                vm.BehaviourPoints = t.Behaviour.BehaviourPoints;
                vmlist.ChildVM.Add(vm);
            }
            return View(vmlist);
        }

        
        public ActionResult RewardHistory(int id = 1)
        {
            RewardsViewModel model = BuildRewardView(id);
            model.ShowAllocation = false;
            model.ShowRewards = true;
            return View("Reward", model);
        }

        public ActionResult RewardAcceptance(int id = 1)
        {
            RewardsViewModel model = BuildRewardView(id);
            model.ShowAllocation = true;
            model.ShowRewards = false;
            return View("Reward", model);
        }

        private RewardsViewModel BuildRewardView(int id)
        {
            string current_user = User.Identity.Name;
            var testchild = BL.GetChildAndRewardsForId(id);

            var model = new RewardsViewModel();
            var rewardsAwarded = BL.GetRewardAwardedForId(id).OrderByDescending(a => a.RewardReceivedDate).ToList();
            List<RewardAwardedVM> rewardsAwardedVM = BuildRewardsAwardedVM(rewardsAwarded);
            model.RewardsAwardedVM = rewardsAwardedVM;

            var goals = BL.GetGoals().Where(a => a.ParentUser == current_user).ToList();

            var vmlist = new List<ChildRewardVM>();
            foreach (var x in goals)
            {
                var rewardallocated = testchild.ChildRewards.Where(a => a.GoalId == x.GoalId).FirstOrDefault();
                int pointsallocated = 0;
                var cr = new ChildRewardVM();
                if (rewardallocated != null)
                {
                    cr.RewardComplete = rewardallocated.RewardComplete;
                    cr.WantReward = rewardallocated.RewardRequested;
                    cr.GiveReward = rewardallocated.RewardComplete;
                    pointsallocated = rewardallocated.PointsAllocated;
                }

                cr.ChildId = id;
                cr.Administrator = true; // temporary as will do another way
                cr.GoalCompletePoints = x.GoalPointsRequired;
                cr.GoalName = x.GoalName;
                cr.GoalId = x.GoalId;
                cr.GoalPoints = pointsallocated;  // fetch from rewards
                cr.GoalPointsOriginal = pointsallocated;  // fetch from rewards


                if (pointsallocated == x.GoalPointsRequired)
                {
                    cr.SliderVisible = false;
                }
                else
                {
                    cr.SliderVisible = true;
                }
                vmlist.Add(cr);
            }
            model.ChildRewardsVM = vmlist;
            ViewBag.Child = BL.GetChildForId(id);
            ViewBag.Administrator = true;
            model.Administrator = true;
            model.UserLevel = false;
            model.Child = BL.GetChildForId(id);
            return model;
        }

        private static List<RewardAwardedVM> BuildRewardsAwardedVM(List<RewardAwarded> rewardsAwarded)
        {
            var rewardsAwardedVM = new List<RewardAwardedVM>();
            foreach (var r in rewardsAwarded)
            {
                var v = new RewardAwardedVM();
                v.GoalDescription = r.GoalDescription;
                v.PointsAllocated = r.PointsAllocated;
                v.RewardReceivedDate = r.RewardReceivedDate.ToShortDateString();
                rewardsAwardedVM.Add(v);
            }

            return rewardsAwardedVM;
        }

        public ActionResult Reward(int id=1)
        {
            string current_user = User.Identity.Name; // note, might need to change when not a logged in User
            // var testreward = BL.GetChildRewardForId(1, 2);
            var testchild = BL.GetChildAndRewardsForId(id);
            var model = new RewardsViewModel();
            model.ShowAllocation = true;
            model.ShowRewards = false;
            var rewardsAwarded = BL.GetRewardAwardedForId(id);
            List<RewardAwardedVM> rewardsAwardedVM = BuildRewardsAwardedVM(rewardsAwarded);
            model.RewardsAwardedVM = rewardsAwardedVM;

            var goals = BL.GetGoals().Where(a => a.ParentUser ==current_user).ToList();
            var vmlist = new List<ChildRewardVM>();
            foreach( var x  in goals)
            {
                var rewardallocated = testchild.ChildRewards.Where(a => a.GoalId == x.GoalId && a.RewardComplete == false).FirstOrDefault();
                int pointsallocated = 0;                         

                var cr = new ChildRewardVM();
                if (rewardallocated != null)
                {
                    pointsallocated = rewardallocated.PointsAllocated;
                    cr.WantReward = rewardallocated.RewardRequested;
                    cr.RewardComplete = rewardallocated.RewardComplete;
                }

                cr.ChildId = id;
                cr.GoalCompletePoints = x.GoalPointsRequired;
                cr.GoalName = x.GoalName;
                cr.GoalId = x.GoalId;
                cr.GoalPoints = pointsallocated ;  // fetch from rewards
                cr.GoalPointsOriginal = pointsallocated;  // fetch from rewards
                
                if (pointsallocated == x.GoalPointsRequired)
                {
                    cr.SliderVisible = false;
                }
                else
                {
                    cr.SliderVisible = true;
                }
                vmlist.Add(cr);
            }
            model.ChildRewardsVM = vmlist;
            ViewBag.Child = BL.GetChildForId(id);
            model.Child = BL.GetChildForId(id);
            model.Administrator = false;
            model.UserLevel = true;
            return View(model);
        }

        [HttpGet]
        [OutputCache(Duration = 0)]
        public JsonResult PointAllocationListById(int id)
        {
            var list = BL.GetPointsForId(id);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(Duration = 0)]
        public JsonResult ChildRewardsListById(int id)
        {
            var list = BL.GetAllChildRewardForId(id);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Duration = 0)]
        [System.Web.Mvc.Authorize]
        public ActionResult BehaviourList()
        {
            string current_user = User.Identity.Name;
            var model = BL.GetBehaviours().Where(a => a.ParentUser == current_user).ToList();
            return View(model);
        }

        [OutputCache(Duration = 0)]
        [System.Web.Mvc.Authorize]
        public ActionResult GoalList()
        {
            string current_user = User.Identity.Name;
            var model = BL.GetGoals().Where(a => a.ParentUser == current_user).ToList();
            return View(model);
        }

        [HttpGet]
        [OutputCache(Duration = 0)]       
        public JsonResult GetGoodBehaviour()
        {
            string current_user = User.Identity.Name;
            var behaviour = BL.GetGoodBehaviour().Where(a => a.ParentUser == current_user).ToList();
            
            return Json(behaviour, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(Duration = 0)]
        public JsonResult GetGoodBehaviourByUserName(string user)
        {
            string current_user = user;
            var behaviour = BL.GetGoodBehaviour().Where(a => a.ParentUser == current_user).ToList();

            return Json(behaviour, JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Duration = 0)]
        public JsonResult GetChildren()
        {
            string current_user = User.Identity.Name;
            var children = BL.GetChildren().Where(a => a.ParentUser == current_user).ToList();
            return Json(children, JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Duration = 0)]
        public JsonResult GetChildrenByUserName(string user)
        {
            string current_user = user;
            var children = BL.GetChildren().Where(a => a.ParentUser == current_user).ToList();
            return Json(children, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(Duration = 0)]
        public JsonResult GetBadBehaviour()
        {
            string current_user = User.Identity.Name;
            var behaviour = BL.GetBadBehaviour().Where(a => a.ParentUser == current_user).ToList();
            return Json(behaviour, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(Duration = 0)]
        public JsonResult GetBadBehaviourByUserName(string user)
        {
            string current_user = user;
            var behaviour = BL.GetBadBehaviour().Where(a => a.ParentUser == current_user).ToList();
            return Json(behaviour, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(Duration = 0)]
        public JsonResult GetNewUserAuthCode()
        {            
            var newcode = BL.CreateUserAuthCode();
            return Json(newcode, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddChild(Child child)
        {
            string current_user = User.Identity.Name;
            child.ParentUser = current_user;
            child.UserImage = "/Content/usernotset.png";
            var addedchild = BL.AddChild(child);
            return Json(addedchild,JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddContributor(Contributor contributor)
        {
            string current_user = User.Identity.Name;
            contributor.ParentUser = current_user;
            contributor.ContributorImage = "/Content/usernotset.png";
            BL.AddContributor(contributor);
            return Json(contributor, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public int AddPointQuick(int id, int points)
        {
            var child = BL.GetChildForId(id);
            var point = new PointAllocation();
            point.ChildId = child.ChildId;
            point.Approved = true;
            //point.Saved = true;
            DateTime curtime = DateTime.Now;
            point.AllocationDate = curtime;
            string contributorStr = child.ParentUser;
            var presetpoint = BL.GetBehaviours().Where(a => a.ParentUser == child.ParentUser && a.Preset == true && a.BehaviourPoints == points).FirstOrDefault();
            point.BehaviourId = presetpoint.BehaviourId;
            point.Points = presetpoint.BehaviourPoints;

            if (point.ContributorId > 0)
            {
                var contributor = BL.GetContributorForId(point.ContributorId);
                contributorStr = contributor.ContributorName;

            }
            // lookup appropriate range value
            BL.AddPoint(point);
            var newpoints = BL.GetPointsValueForId(id);
            return newpoints;
        }

        [HttpPost]
        public JsonResult AddPoint(PointAllocation point, string Contributor)
        {
            DateTime curtime = DateTime.Now;
            point.AllocationDate = curtime;
            BL.AddPoint(point);
            // Broadcast Change using SignalR
            var child = BL.GetChildForId(point.ChildId);
            string contributorStr = child.ParentUser;

            if (point.ContributorId > 0)
            {
                var contributor = BL.GetContributorForId(point.ContributorId);
                contributorStr = contributor.ContributorName;
            }

            var gc = GlobalHost.ConnectionManager.GetHubContext<KidHub>();
            gc.Clients.All.addNewMessageToPage(child.ParentUser, contributorStr + "- Points Allocated:"+ point.Points.ToString());
            return Json(point, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateRewards(ChildRewardVM[] rewards)
        {
            if(rewards.Length > 0)
            {
                var rewardslist = new List<ChildReward>();
                DateTime curtime = DateTime.Now;
                foreach ( var r in rewards){
                    var reward = new ChildReward();
                    reward.GoalId = r.GoalId;
                    reward.PointsAllocated = r.GoalPoints;
                    reward.RewardRequested = r.WantReward;
                    reward.ChildId = r.ChildId;                    
                    reward.RewardReceivedDate = curtime;
                    rewardslist.Add(reward);
                };
                BL.AddorUpdateReward(rewardslist);
            }

            return Json(rewards, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateRewardsAdministrator(ChildRewardVM[] rewards)
        {
            if (rewards.Length > 0)
            {
                var rewardslist = new List<ChildReward>();
                DateTime curtime = DateTime.Now;
                foreach (var r in rewards)
                {
                    var reward = new ChildReward();
                    reward.RewardComplete = r.GiveReward;
                    //if(r.RewardComplete == true) { reward.}
                    reward.GoalId = r.GoalId;
                    reward.PointsAllocated = r.GoalPoints;
                    reward.RewardRequested = r.WantReward;
                    reward.ChildId = r.ChildId;
                    reward.RewardReceivedDate = curtime;
                    reward.RewardReceived = r.GiveReward;
                    rewardslist.Add(reward);
                };
                BL.AddorUpdateReward(rewardslist);
            }

            return Json(rewards, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public Child DeleteChild(Child child)
        {
           // var test = child.ChildId;
            BL.DeleteChild(child);
            return child;
        }

        [HttpPost]
        public Contributor DeleteContributor(Contributor contributor)
        {
            //var test = contributor.ContributorId;
            BL.DeleteContributor(contributor);
            return contributor;
        }

        [HttpPost]
        public Child BankPoints(Child child)
        {
            var id = child.ChildId;
            BL.UpdatePointsToSavedStatusForId(child);
            return child;
        }

        [HttpPost]
        public JsonResult UpdateChild(Child child)
        {
            BL.UpdateChild(child);
            return Json(child, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EditChildValues(Child child)
        {
            var curChild = BL.GetChildForId(child.ChildId);
            curChild.ChildAge = child.ChildAge;
            curChild.ChildName = child.ChildName;
            curChild.ChildSex = child.ChildSex;
            curChild.UserAuthCode = child.UserAuthCode;

            BL.EditChild(curChild);
            return Json(child, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddBehaviour(Behaviour behaviour)
        {
            string current_user = User.Identity.Name; ;
            behaviour.ParentUser = current_user;
            BL.AddBehaviour(behaviour);
            return Json(behaviour, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteBehaviour(Behaviour behaviour)
        {
            BL.DeleteBehaviour(behaviour);
            return Json(behaviour, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddGoal(Goal goal)
        {
            string current_user = User.Identity.Name;
            goal.ParentUser = current_user;
            BL.AddGoal(goal);
            return Json(goal, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteGoal(Goal goal)
        {
            BL.DeleteGoal(goal);
            return Json(goal, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteReviewPoint(PointReviewVM point)
        {
            
            BL.DeletePointReview(point.PointId);
            return Json(point, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ApproveChildPoints(Child child)
        {

            BL.ApprovePointsForChild(child.ChildId);
            return Json(child, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ApproveContributorPoints(PointReviewVM[] points)
        {
            foreach(var p in points)
            {
                BL.ApprovePointForId(p.PointId);
            }
            //BL.ApprovePointsForChild(child.ChildId);
            return Json(points, JsonRequestBehavior.AllowGet);
        }

    }
        }