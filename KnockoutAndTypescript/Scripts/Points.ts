/// <reference path="typings/knockout/knockout.d.ts" />
/// <reference path="typings/knockout.mapping/knockout.mapping.d.ts" />
/// <reference path="typings/knockout.validation/knockout.validation.d.ts" />
/// <reference path="typings/jquery/jquery.d.ts" />

class Points{      
    PointsId: number;
    ChildId: number;
    AllocationDate: string;
    Points: number;
    BehaviourId: number;
    Saved: boolean;
    ContributorId: number;
    Approved: boolean;
}

class PointSignal {
    ChildId: number;
    ChildName: string;
    Points: number;
    GoodPoints: number;
    BadPoints: number;
}


class PointViewModel {
    //values: string[];
    goodBehaviour: KnockoutObservableArray<Behaviour>;
    naughtyBehaviour: KnockoutObservableArray<Behaviour>;
    children: KnockoutObservableArray<Children>;
    AutoCreate: KnockoutObservable<boolean>;   
    Errors: KnockoutValidationErrors;
    SelectedChild: KnockoutObservable<any>;
    Points: KnockoutObservableArray<Points>;
    TotalPoints: KnockoutComputed<number>;
    ComputeTotalPoints: () => void;
    TotalPointsNum: KnockoutObservable<number>;
    GoodPointsNum: KnockoutObservable<number>;
    BadPointsNum: KnockoutObservable<number>;
    BankedPointsNum: KnockoutObservable<number>;
    RewardsWaitingNum: KnockoutObservable<number>;
    ChatVar: any;
    ChildSelectedBool: KnockoutObservable<boolean>;
    NothingSelectedBool: KnockoutObservable<boolean>;
    ChildParmSelected: KnockoutObservable<number>;
   
    //getGood: (any) => void; 
   
    constructor(UserSelected: any) {
        //this.goals = ko.observableArray(values);
        //this.GoalName = ko.observable("").extend({required:true });
        //this.GoalPointsRequired = ko.observable(0).extend({required:true});
        //this.AutoCreate = ko.observable(false);     
        // if (UserSelected > -1) {
        this.ChildParmSelected = ko.observable(UserSelected);
            //let match: Children = ko.utils.arrayFirst(this.children(), (item: Children) => {
            //    return UserSelected === item.ChildId;
            //});
            //console.log(match.ChildName);
       // }
        this.Errors = ko.validation.group(this);
        this.goodBehaviour = ko.observableArray([]);
        this.naughtyBehaviour = ko.observableArray([]);
        this.Points = ko.observableArray([]);
        this.children = ko.observableArray([]);
        this.getGood(this.goodBehaviour);
        this.getBad(this.naughtyBehaviour);
        this.getChildren(this, this.children);
        this.SelectedChild = ko.observable();
        this.TotalPointsNum = ko.observable(0);
        this.GoodPointsNum = ko.observable(0);
        this.BadPointsNum = ko.observable(0);
        this.BankedPointsNum = ko.observable(0);
        this.RewardsWaitingNum = ko.observable(0);
        this.ChatVar = null;
        //let initialchild = this.children()[0];
        //this.SelectedChild(initialchild);   
        this.ChildSelectedBool = ko.observable(false);
        this.NothingSelectedBool = ko.observable(true);

        //debugger;
        //console.log(UserSelected);
        
        //var qrychild = this.urlParam("childid");
        //if (qrychild != null)
        //{
        //    // this.SelectedChild()
        //    let ctest = this.children()[0];
        //    this.SelectedChild(ctest);
        //    console.log(this.SelectedChild());
        //}

        this.Points.subscribe((data) => {
            console.log("points subscribe");   
            let totalval = 0;
            let goodval = 0;
            let badval = 0;
            for (let p of this.Points()) {
                totalval = totalval + Number(p.Points);
                p.Points < 0 ? badval = badval + p.Points : goodval = goodval + p.Points;
            }
            this.TotalPointsNum(totalval);
            this.GoodPointsNum(goodval);
            this.BadPointsNum(badval);
            //this.ChatVar.server.send("test", totalval.toString());
        });

        this.TotalPointsNum.subscribe((data) => {
            let totalval = 0;
            let goodval = 0;
            let badval = 0;
            for (let p of this.Points()) {
                totalval = totalval + Number(p.Points);
                p.Points < 0 ? badval = badval + p.Points : goodval = goodval + p.Points;
            }
            
            //this.ChatVar.server.send("test", totalval.toString());
            //let pointcast = new PointSignal();
            //pointcast.ChildId = this.SelectedChild().ChildId;
            //pointcast.ChildName = this.SelectedChild().ChildName;
            //pointcast.BadPoints = badval;
            //pointcast.GoodPoints = goodval;
            //pointcast.Points = totalval;
            //this.ChatVar.server.updatePoints(pointcast);
        });

        this.TotalPoints = ko.computed({
            owner: this,
            read: () => {                           
                //let totalPoints: number = 0;

                //for (let i = 0; i < this.Points.length; i++) {
                //    totalPoints = totalPoints + this.Points()[i].Points;
                //}
                //return totalPoints;        
                return 0;
            }
        }).extend({ notify: 'always' });

        this.ComputeTotalPoints = ko.pureComputed(() => {
            let totalval = 0;
            console.log("triggered");
            for (let points of this.Points()) {                
                    totalval = totalval + Number(points.Points);                
            }
            return totalval.toFixed(2);
        }).extend({ notify: 'always' });

        //console.log(this.children);
        this.SelectedChild.subscribe((data) => {
            //console.log(data
            if (this.SelectedChild != null) {
                console.log("in subscribe");
                let test = this.Points;
                this.getPoints(test, this.SelectedChild());
                var testnum = this.SelectedChild().BankedPoints;
                console.log(testnum);
                this.BankedPointsNum(testnum);
                //this.getChildRewards(this.BankedPointsNum, this.SelectedChild());
            }
        });
    }

    urlParam = (name: string) => {
        var results = new RegExp('[\?&]' + name + '=([^]*)').exec(window.location.href);
        if (results == null) {
            return null;
        }
        else {
            return results[1] || 0;
        }
    }

    // method and variables required to stop error with link on the rewards failing when no child selected
    SelectChange(data) { 
        console.log("listItemSelected")
        console.log(data);
        var currentChild = <Children>this.SelectedChild();
        if (currentChild != null) {
            this.NothingSelectedBool(false);
            this.ChildSelectedBool(true);
        } else {
            this.NothingSelectedBool(true);
            this.ChildSelectedBool(false);
        }
    }

    listItemSelected = (behaviour: Behaviour) => {
       // console.log(behaviour);
      //  console.log(this.children());
        var currentChild = <Children>this.SelectedChild();
        if (currentChild != null) {
            let p1 = new Points();
            p1.ChildId = currentChild.ChildId;
            p1.Points = behaviour.BehaviourPoints;
            p1.PointsId = 0;
            p1.AllocationDate = "";
            p1.BehaviourId = behaviour.BehaviourId;
            p1.Approved = true;
            this.saveItems(p1,this);
            this.Points.push(p1);
            
            
        }
        
    }

    BankPointsClick() {
        console.log("banking points");
        var pointscollected = this.TotalPointsNum();
        if (pointscollected > 0)
        {
            var adjustedpoints = this.BankedPointsNum() + pointscollected;
            this.BankedPointsNum(adjustedpoints);
            this.SelectedChild().BankedPoints = adjustedpoints;
            this.bankUpdate(this.SelectedChild(), this);
            this.Points.removeAll();
        }
}
     
    broadcastPointsChange(points: Points, root) {
        let totalval = 0;
        let goodval = 0;
        let badval = 0;
        for (let p of this.Points()) {
            totalval = totalval + Number(p.Points);
            p.Points < 0 ? badval = badval + p.Points : goodval = goodval + p.Points;
        }
        //this.TotalPointsNum(totalval);
        //this.GoodPointsNum(goodval);
        //this.BadPointsNum(badval);
        //root.ChatVar.server.send("test", totalval.toString());
    }

    saveItems(points:Points, root) {
      
        var promise =
            $.ajax({
                url: "/Home/AddPoint/",
                cache: false,
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(points),
                success: function (result) {

                    let pointrtn: Points = <Points>result;
                    console.log(pointrtn);
                    console.log(root.Points());
                    let match: Points = ko.utils.arrayFirst(root.Points(), (item: Points) => {
                        return points.PointsId === item.PointsId;
                    });

                    match.PointsId = pointrtn.ChildId;
                    match.AllocationDate = pointrtn.AllocationDate;
                  //  root.broadcastPointsChange(root.points, root);
                },
            });
        promise.done(function (res) {

        });
    }

    bankUpdate(child: Children, root) {

        var promise =
            $.ajax({
                url: "/Home/BankPoints/",
                cache: false,
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(child),
                success: function (result) {

                    //let pointrtn: Points = <Points>result;
                   // console.log(pointrtn);
                  //  console.log(root.Points());
                    //let match: Points = ko.utils.arrayFirst(root.Points(), (item: Points) => {
                    //    return points.PointsId === item.PointsId;
                    //});

                    //match.PointsId = pointrtn.ChildId;
                    //match.AllocationDate = pointrtn.AllocationDate;
                    //  root.broadcastPointsChange(root.points, root);
                },
            });
        promise.done(function (res) {

        });
    }

    getPoints = (points: KnockoutObservableArray<Points>, child) => {
        var childid = child.ChildId;
        console.log("about to getting points");
        var promise =
            $.ajax({
                url: "/Home/PointAllocationListById/",
                cache: false,
                type: "GET",
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                // data: { "id": childid, "name": this.TabPanelName() },
                data: { "id": childid },
                success: function (result) {
                    points.removeAll();
                    //var data = result.json()[result];
                    //let good: Behaviour = <Behaviour>result;
                    for (let r of result) {
                        points.push(r);
                    }
                    //behavior(result);
                    console.log("getting points");
                    // points.valueHasMutated();
                    //let match: Goal = ko.utils.arrayFirst(root.goals(), (item: Goal) => {
                    //    return data.GoalId === item.GoalId;
                    //});

                    //match.GoalId = goal.GoalId; // set Id to that returned from Server

                },
            });
        promise.done(function (res) {

        });
    }

    getChildRewards = (TotalPointsNum: KnockoutObservable<number>, child) => {
        var childid = child.ChildId;
        var promise =
            $.ajax({
                url: "/Home/ChildRewardsListById/",
                cache: false,
                type: "GET",
                contentType: "application/json; charset=utf-8",
                dataType: 'json',               
                data: { "id": childid},
                success: function (result) {
                  //  points.removeAll();
                    let totalRewardsAllocation = 0;
                    
                    for (let r of result) {
                        totalRewardsAllocation = totalRewardsAllocation + r.PointsAllocated;
                    }
                    TotalPointsNum(TotalPointsNum() - totalRewardsAllocation);
                    console.log(result);
                     

                },
            });
        promise.done(function (res) {

        });
    }

    getGood = (behavior: KnockoutObservableArray<Behaviour>) => {
    var promise =
        $.ajax({
            url: "/Home/GetGoodBehaviour/",
            cache: false,
            type: "GET",
            contentType: "application/json; charset=utf-8",
            
            success: function (result) {
                //var data = result.json()[result];
                //let good: Behaviour = <Behaviour>result;
                for (let r of result) {
                    behavior.push(r);
                }
                //behavior(result);
                console.log(behavior);
                //let match: Goal = ko.utils.arrayFirst(root.goals(), (item: Goal) => {
                //    return data.GoalId === item.GoalId;
                //});

                //match.GoalId = goal.GoalId; // set Id to that returned from Server

            },
        });
    promise.done(function (res) {

    });
    }
    getChildren = (root: any, children: KnockoutObservableArray<Children>) => {
        console.log("Get Children");
        var promise =
            $.ajax({
                url: "/Home/GetChildren/",
                cache: false,
                type: "GET",
                contentType: "application/json; charset=utf-8",

                success: function (result) {
                    //var data = result.json()[result];
                    //let good: Behaviour = <Behaviour>result;
                    for (let r of result) {
                        children.push(r);
                    }
                    //behavior(result);
                   // console.log(children);
                    //let match: Goal = ko.utils.arrayFirst(root.goals(), (item: Goal) => {
                    //    return data.GoalId === item.GoalId;
                    //});
                    //children.valueHasMutated;
                    //match.GoalId = goal.GoalId; // set Id to that returned from Server
                    var qrychild = root.urlParam("childid");
                    var idx = 0;
                    if (qrychild != null) {
                        // this.SelectedChild()
                        for (let c of children())
                        {
                            if (c.ChildId == qrychild)
                            {
                                let ctest = children()[idx];
                                root.SelectedChild(ctest);
                                root.ChildSelectedBool(true);
                                root.NothingSelectedBool(false);
                            }
                            idx = idx + 1;
                        }
                        
                        console.log(root.SelectedChild());
                    }
                    
                    if (root.ChildParmSelected() > -1) {

                        let match: Children = ko.utils.arrayFirst(result, (item: Children) => {
                            return root.ChildParmSelected() === item.ChildId;
                        });
                        console.log("Got Children");
                        root.SelectedChild(match);
                        console.log("After seletedChild");
                        root.ChildSelectedBool(true);
                        root.NothingSelectedBool(false);
                        //root.GetPoints(root.points, match);
                    }
                },
            });
        promise.done(function (res) {

        });
    }  

    getBad = (behavior: KnockoutObservableArray<Behaviour>) => {
        var promise =
            $.ajax({
                url: "/Home/GetBadBehaviour/",
                cache: false,
                type: "GET",
                contentType: "application/json; charset=utf-8",

                success: function (result) {
                    //var data = result.json()[result];
                    //let good: Behaviour = <Behaviour>result;
                    for (let r of result) {
                        behavior.push(r);
                    }
                    //behavior(result);
                    console.log(behavior);
                    //let match: Goal = ko.utils.arrayFirst(root.goals(), (item: Goal) => {
                    //    return data.GoalId === item.GoalId;
                    //});

                    //match.GoalId = goal.GoalId; // set Id to that returned from Server

                },
            });
        promise.done(function (res) {

        });
    }  
    
    }

