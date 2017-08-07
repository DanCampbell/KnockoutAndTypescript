using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KnockoutAndTypescript.Models;
using System.Data.Entity;
using System.Data.Entity.Migrations;


namespace KnockoutAndTypescript.Business
{
    public class BusinessRules
    {
        private const string CHARACTERS = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    static public long DecodeB36(string value)
        {
            var database = new List<char>(CHARACTERS);
            var tmp = new List<char>(value.ToUpper().TrimStart(new char[] { '0' }).ToCharArray());
            tmp.Reverse();

            long number = 0;
            int index = 0;
            foreach (char character in tmp)
            {
                number += database.IndexOf(character) * (long)Math.Pow(36, index);
                index++;
            }

            return number;
        }

        static private string EncodeB36(long number)
        {
            var database = new List<char>(CHARACTERS);
            var value = new List<char>();
            long tmp = number;

            while (tmp != 0)
            {
                value.Add(database[Convert.ToInt32(tmp % 36)]);
                tmp /= 36;
            }

            value.Reverse();
            return new string(value.ToArray());
        }

        public string CreateUserAuthCode()
        {
            Random rnd = new Random();
            int seed = rnd.Next(1, 1679615);
            var base36 = EncodeB36(seed);
            return base36;
        }

        public IEnumerable<PointAllocation> GetPointsForId(int id)
        {
            using (var db = new ModelKids())
            {
                var allPoints = db.PointAllocation.Where(a => a.ChildId == id  && a.Saved != true).ToList();
                return allPoints;
            }
        }

        public ChildReward GetChildRewardForId(int ChildId, int GoalId)
        {
            using (var db = new ModelKids())
            {
                var rtn = db.ChildReward.Where(a => a.ChildId == ChildId && a.GoalId == GoalId).Include(i => i.Goal).FirstOrDefault();
                return rtn;
            }
        }

        public List<ChildReward> GetAllChildRewardForId(int ChildId)
        {
            using (var db = new ModelKids())
            {
                var rtn = db.ChildReward.Where(a => a.ChildId == ChildId).ToList();
                return rtn;
            }
        }

        public void UpdatePointsToSavedStatusForId(Child child)
        {
            using (var db = new ModelKids())
            {
                var allPoints = db.PointAllocation.Where(a => a.ChildId == child.ChildId && a.Saved != true);
                foreach (var p in allPoints)
                {
                    p.Saved = true;
                }
                var curchild = db.Children.Where(a => a.ChildId == child.ChildId).First();
                curchild.BankedPoints = child.BankedPoints;

                db.SaveChanges();
            }
            }

        public IEnumerable<Points2> GetPoints2ForId(int id)
        {
            using (var db = new ModelKids())
            {
                var test = db.Points2.Where(a => a.ChildId == id).Include(a=> a.Behaviour2).ToList();
                var allPoints = db.Points2.Where(a => a.ChildId == id).ToList();
                return allPoints;
            }
        }

        public IEnumerable<PointAllocation> GetPointsAndDescriptionForId(int id)
        {
            using (var db = new ModelKids())
            {
                var allPoints = db.PointAllocation.Where(a => a.ChildId == id && a.Saved != true).Include(i => i.Behaviour).ToList();
                return allPoints;
            }
        }

        public IEnumerable<PointAllocation> GetPointsAndDescriptionForIdNotFiltered(int id)
        {
            using (var db = new ModelKids())
            {
                var allPoints = db.PointAllocation.Where(a => a.ChildId == id).Include(i => i.Behaviour).ToList();
                return allPoints;
            }
        }

        public IEnumerable<PointAllocation> GetPointsAndDescriptionForContributorId(int id)
        {
            using (var db = new ModelKids())
            {
                var allPoints = db.PointAllocation.Where(a => a.ContributorId == id).Include(i => i.Behaviour).ToList();
                return allPoints;
            }
        }

        public IEnumerable<Child> GetChildren()
        {
            using (var db = new ModelKids())
            {
                var allKids = db.Children.ToList();
                return allKids;
            }
        }

        public IEnumerable<Contributor> GetContributors(string parent)
        {
            using (var db = new ModelKids())
            {
                var allContributors = db.Contributor.Where(a => a.ParentUser == parent).ToList();
                return allContributors;
            }
        }

        public Child GetChildForId(int id)
        {
            using (var db = new ModelKids())
            {
                var kid = db.Children.Where(a => a.ChildId == id).FirstOrDefault();
                return kid;
            }
        }

        public Contributor GetContributorForId(int id)
        {
            using (var db = new ModelKids())
            {
                var rtn = db.Contributor.Where(a => a.ContributorId == id).FirstOrDefault();
                return rtn;
            }
        }

        public Contributor GetContributorForCode(string code, string parent)
        {
            using (var db = new ModelKids())
            {
                var rtn = db.Contributor.Where(a => a.ContributorAuthCode == code && a.ParentUser == parent).FirstOrDefault();
                return rtn;
            }
        }

        public Child GetChildAndRewardsForId(int id)
        {
            using (var db = new ModelKids())
            {
                var kid = db.Children.Where(a => a.ChildId == id).Include(a => a.ChildRewards).FirstOrDefault();
                
                return kid;
            }
        }

        //public Bank GetBankForId(int id)
        //{
        //    //using (var db = new ModelKids())
        //    //{
        //    //    var rtn = db.Bank.Where(a => a.PersonId == id).Include(i => i.Child).FirstOrDefault();
        //    //    return rtn;
        //    //}

        //}

        public IEnumerable<Behaviour> GetBehaviours()
        {
            using (var db = new ModelKids())
            {
                var allBehaviour = db.Behaviour.ToList();
                return allBehaviour;
            }
        }
        public IEnumerable<Behaviour2> GetBehaviours2()
        {
            using (var db = new ModelKids())
            {
                var allBehaviour = db.Behaviour2.ToList();
                return allBehaviour;
            }
        }

        public IEnumerable<Behaviour> GetGoodBehaviour()
        {
            using (var db = new ModelKids())
            {
               
               // var allBehaviour = db.Behaviour.Where(a => a.BehaviourPoints > 0).Include(b => b.Points).ToList();
                var allBehaviour = db.Behaviour.Where(a => a.BehaviourPoints > 0).ToList();

                return allBehaviour;
            }
        }

        public IEnumerable<Behaviour> GetBadBehaviour()
        {
            using (var db = new ModelKids())
            {
               // var allBehaviour = db.Behaviour.Where(a => a.BehaviourPoints < 0).Include(b => b.Points).ToList();
                var allBehaviour = db.Behaviour.Where(a => a.BehaviourPoints < 0).ToList();
                return allBehaviour;
            }
        }

        public IEnumerable<Goal> GetGoals()
        {
            using (var db = new ModelKids())
            {
                var allGoals = db.Goals.ToList();
                return allGoals;
            }
        }

        public Child AddChild(Child child)
        {
            using (var db = new ModelKids())
            {
                child.CanBank = true;
                child.UserAuthCode = CreateUserAuthCode();
                db.Children.Add(child);
                db.SaveChanges();
                var newid = child.ChildId;

            }
            return child;
        }

        public Contributor AddContributor(Contributor contributor)
        {
            using (var db = new ModelKids())
            {
                
                contributor.ContributorAuthCode = CreateUserAuthCode();
                db.Contributor.Add(contributor);
                db.SaveChanges();
                var newid = contributor.ContributorId;

            }
            return contributor;
        }

        public PointAllocation AddPoint(PointAllocation point)
        {
            var test = point.ContributorId;

            using (var db = new ModelKids())
            {
                //var behaviour = db.Behaviour.Where(a => a.BehaviourId == point.BehaviourId).First();
                var p1 = new PointAllocation();
                p1.ChildId = point.ChildId; 
                p1.BehaviourId = point.BehaviourId;
                p1.Points = point.Points;
                p1.AllocationDate = DateTime.Now;
                p1.Saved = false;
                p1.ContributorId = point.ContributorId;
                p1.Approved = point.Approved;
                //point.Behaviour = behaviour;
                db.PointAllocation.Add(p1);
                db.SaveChanges();
                var newid = p1.PointId;
                point = p1;
            }
            return point;
        }

        public Points2 AddPoint2(Points2 point)
        {
            using (var db = new ModelKids())
            {
                db.Points2.Add(point);
                db.SaveChanges();
                var newid = point.Point2Id;
            }
            return point;
        }

        public Child UpdateChild(Child child)
        {
            using (var db = new ModelKids())
            {
                var val = db.Children.Where(a => a.ChildId == child.ChildId).First();
                val.BankedPoints = child.BankedPoints;
                db.SaveChanges();
            }
            return child;
        }

        public Child EditChild(Child child)
        {
            using (var db = new ModelKids())
            {
                //var val = db.Children.Where(a => a.ChildId == child.ChildId).First();
                //val.BankedPoints = child.BankedPoints;
               // db.Children.Attach(child);
                db.Children.AddOrUpdate(child);
                db.SaveChanges();
            }
            return child;
        }

        public void UpdateChildImage(int id, string imageuri)
        {
            using (var db = new ModelKids())
            {
                var val = db.Children.Where(a => a.ChildId == id).First();
                val.UserImage = imageuri;
                db.SaveChanges();
            }
        }

        public void UpdateContributorImage(int id, string imageuri)
        {
            using (var db = new ModelKids())
            {
                var val = db.Contributor.Where(a => a.ContributorId == id).First();
                val.ContributorImage = imageuri;
                db.SaveChanges();
            }
        }

        public Child DeleteChild(Child child)
        {
            using (var db = new ModelKids())
            {
                db.Children.Attach(child);
                db.Children.Remove(child);
                
                db.SaveChanges();
            }
            return child;
        }

        public Contributor DeleteContributor(Contributor contributor)
        {
            using (var db = new ModelKids())
            {
                db.Contributor.Attach(contributor);
                db.Contributor.Remove(contributor);

                db.SaveChanges();
            }
            return contributor;
        }

        public Behaviour AddBehaviour(Behaviour behaviour)
        {
            using (var db = new ModelKids())
            {
                db.Behaviour.Add(behaviour);
                db.SaveChanges();
                var newid = behaviour.BehaviourId;
            }
            return behaviour;
        }

        public Behaviour DeleteBehaviour(Behaviour behaviour)
        {
            using (var db = new ModelKids())
            {
                db.Behaviour.Attach(behaviour);
                db.Behaviour.Remove(behaviour);

                db.SaveChanges();
            }
            return behaviour;
        }

        public Goal AddGoal(Goal goal)
        {
            using (var db = new ModelKids())
            {
                db.Goals.Add(goal);
                db.SaveChanges();
                //var newid = goal.BehaviourId;
            }
            return goal;
        }

        public RewardAwarded AddRewardAwarded(RewardAwarded rewardawarded)
        {
            using (var db = new ModelKids())
            {
                db.RewardAwarded.Add(rewardawarded);
                db.SaveChanges();
                //var newid = goal.BehaviourId;
            }
            return rewardawarded;
        }

        public Goal DeleteGoal(Goal goal)
        {
            using (var db = new ModelKids())
            {
                db.Goals.Attach(goal);
                db.Goals.Remove(goal);

                db.SaveChanges();
            }
            return goal;
        }

        public void DeletePointReview(int id) { 
            using (var db = new ModelKids())
            {
                var point = db.PointAllocation.Where(a => a.PointId == id).First();
                db.PointAllocation.Attach(point);
                db.PointAllocation.Remove(point);

                db.SaveChanges();
            }
            return ;
        }

        public List<RewardAwarded> GetRewardAwardedForId(int ChildId)
        {
            using (var db = new ModelKids())
            {
                var rtn = db.RewardAwarded.Where(a => a.ChildId == ChildId).ToList();
                return rtn;
            }
        }

        private RewardAwarded BuildRewardAwarded (ChildReward childreward)
        {
            var rewardawarded = new RewardAwarded();
            rewardawarded.ChildId = childreward.ChildId;
            rewardawarded.GoalDescription = childreward.Goal.GoalName;
            rewardawarded.GoalId = childreward.GoalId;
            rewardawarded.PointsAllocated = childreward.PointsAllocated;
            rewardawarded.RewardReceivedDate = childreward.RewardReceivedDate;
            return rewardawarded;
        }

        //private void UpdateBankedPointsWithChildReward(ChildReward childreward)
        //{
        //    using (var db = new ModelKids())
        //    {
        //        var child = db.Children.Where(a => a.ChildId == childreward.ChildId).First();
        //        child.BankedPoints = child.BankedPoints + childreward.PointsAllocated;

        //    }
        //}
        public void ApprovePointsForChild(int childId)
        {
            using (var db = new ModelKids())
            {
                var points = db.PointAllocation.Where(a => a.Approved == false && a.ChildId == childId);
                if (points != null)
                {
                    foreach (var p in points)
                    {
                        p.Approved = true;
                    }

                    db.SaveChanges();
                }
            }

            return;
        }

        public void ApprovePointForId(int pointId)
        {
            using (var db = new ModelKids())
            {
                var point = db.PointAllocation.Where(a => a.PointId == pointId).First();
                if (point != null)
                {                    
                    point.Approved = true;
                    db.SaveChanges();
                }
            }

            return;
        }


        private void AddorUpdateRewardAwarded(List<ChildReward> rewards)
        {
            using (var db = new ModelKids())
            {
                foreach (var m in rewards)
                {
                    var childreward = db.ChildReward.Where(a => a.GoalId == m.GoalId && a.ChildId == m.ChildId).Include(i => i.Goal).FirstOrDefault();
                    if (childreward != null)
                    {
                        if (childreward.RewardComplete == true)
                        {
                            var ra = BuildRewardAwarded(childreward);

                            db.RewardAwarded.Add(ra);
                            db.ChildReward.Attach(childreward);
                            db.ChildReward.Remove(childreward);
                            db.SaveChanges();
                        }
                    }
                }
            }
        }

        public void AddorUpdateReward(List<ChildReward> rewards)
        {
            using (var db = new ModelKids())
            {
                foreach (var m in rewards)
                {
                    var r = db.ChildReward.Where(a => a.GoalId == m.GoalId && a.ChildId == m.ChildId).Include(i => i.Goal).FirstOrDefault();
                    if (r != null)
                    {
                        r.PointsAllocated = m.PointsAllocated;
                        r.RewardRequested = m.RewardRequested;
                        r.RewardReceived = m.RewardReceived;
                        r.RewardComplete = m.RewardComplete;
                        r.RewardReceivedDate = m.RewardReceivedDate;

                        db.SaveChanges();
                        // AddorUpdateRewardAwarded(r);
                    }
                    else // if no existing record on rewards
                    {
                        if (m.PointsAllocated > 0) // if points allocated create entry.
                        {
                            db.ChildReward.Add(m);
                            db.SaveChanges();

                            //AddorUpdateRewardAwarded(m);
                        }
                    }
                }
            }

            AddorUpdateRewardAwarded(rewards);
            
            return ;
        }
    }
}