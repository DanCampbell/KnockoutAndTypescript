/// <reference path="typings/knockout/knockout.d.ts" />
/// <reference path="typings/knockout.mapping/knockout.mapping.d.ts" />
/// <reference path="typings/knockout.validation/knockout.validation.d.ts" />
/// <reference path="typings/jquery/jquery.d.ts" />
var Points = (function () {
    function Points() {
    }
    return Points;
}());
var PointSignal = (function () {
    function PointSignal() {
    }
    return PointSignal;
}());
var PointViewModel = (function () {
    //getGood: (any) => void; 
    function PointViewModel(UserSelected, Contributor) {
        var _this = this;
        this.urlParam = function (name) {
            var results = new RegExp('[\?&]' + name + '=([^]*)').exec(window.location.href);
            if (results == null) {
                return null;
            }
            else {
                return results[1] || 0;
            }
        };
        this.listItemSelected = function (behaviour) {
            // console.log(behaviour);
            //  console.log(this.children());
            var currentChild = _this.SelectedChild();
            if (currentChild != null) {
                var p1 = new Points();
                p1.ChildId = currentChild.ChildId;
                p1.Points = behaviour.BehaviourPoints;
                p1.PointsId = 0;
                p1.AllocationDate = "";
                p1.BehaviourId = behaviour.BehaviourId;
                p1.Approved = true;
                _this.saveItems(p1, _this);
                _this.Points.push(p1);
            }
        };
        this.getPoints = function (points, child) {
            var childid = child.ChildId;
            console.log("about to getting points");
            var promise = $.ajax({
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
                    for (var _i = 0, result_1 = result; _i < result_1.length; _i++) {
                        var r = result_1[_i];
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
        };
        this.getChildRewards = function (TotalPointsNum, child) {
            var childid = child.ChildId;
            var promise = $.ajax({
                url: "/Home/ChildRewardsListById/",
                cache: false,
                type: "GET",
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: { "id": childid },
                success: function (result) {
                    //  points.removeAll();
                    var totalRewardsAllocation = 0;
                    for (var _i = 0, result_2 = result; _i < result_2.length; _i++) {
                        var r = result_2[_i];
                        totalRewardsAllocation = totalRewardsAllocation + r.PointsAllocated;
                    }
                    // TotalPointsNum(TotalPointsNum() - totalRewardsAllocation);
                    TotalPointsNum(totalRewardsAllocation);
                    // console.log(result);
                },
            });
            promise.done(function (res) {
            });
        };
        this.getPointsToBeReviewed = function (TotalPointsNum, child) {
            var childid = child.ChildId;
            var promise = $.ajax({
                url: "/Home/GetPointsToBeReviewedNum/",
                cache: false,
                type: "GET",
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: { "id": childid },
                success: function (result) {
                    TotalPointsNum(result);
                    // console.log(result);
                },
            });
            promise.done(function (res) {
            });
        };
        this.getGood = function (behavior) {
            var promise = $.ajax({
                url: "/Home/GetGoodBehaviour/",
                cache: false,
                type: "GET",
                contentType: "application/json; charset=utf-8",
                success: function (result) {
                    //var data = result.json()[result];
                    //let good: Behaviour = <Behaviour>result;
                    for (var _i = 0, result_3 = result; _i < result_3.length; _i++) {
                        var r = result_3[_i];
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
        };
        this.getChildren = function (root, children) {
            console.log("Get Children");
            var promise = $.ajax({
                url: "/Home/GetChildren/",
                cache: false,
                type: "GET",
                contentType: "application/json; charset=utf-8",
                success: function (result) {
                    //var data = result.json()[result];
                    //let good: Behaviour = <Behaviour>result;
                    for (var _i = 0, result_4 = result; _i < result_4.length; _i++) {
                        var r = result_4[_i];
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
                        for (var _a = 0, _b = children(); _a < _b.length; _a++) {
                            var c = _b[_a];
                            if (c.ChildId == qrychild) {
                                var ctest = children()[idx];
                                root.SelectedChild(ctest);
                                root.ChildSelectedBool(true);
                                root.NothingSelectedBool(false);
                            }
                            idx = idx + 1;
                        }
                        console.log(root.SelectedChild());
                    }
                    if (root.ChildParmSelected() > -1) {
                        var match = ko.utils.arrayFirst(result, function (item) {
                            return root.ChildParmSelected() === item.ChildId;
                        });
                        console.log("Got Children");
                        root.SelectedChild(match);
                        console.log("After seletedChild");
                        root.ChildSelectedBool(true);
                        root.NothingSelectedBool(false);
                    }
                },
            });
            promise.done(function (res) {
            });
        };
        this.getBad = function (behavior) {
            var promise = $.ajax({
                url: "/Home/GetBadBehaviour/",
                cache: false,
                type: "GET",
                contentType: "application/json; charset=utf-8",
                success: function (result) {
                    //var data = result.json()[result];
                    //let good: Behaviour = <Behaviour>result;
                    for (var _i = 0, result_5 = result; _i < result_5.length; _i++) {
                        var r = result_5[_i];
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
        };
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
        this.PointsAllocatedNum = ko.observable(0);
        this.PointsToBeReviewedNum = ko.observable(0);
        this.ContributorVal = Contributor;
        this.ContributorVal == -1 ? this.DisplayAdmin = ko.observable(true) : this.DisplayAdmin = ko.observable(false);
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
        this.Points.subscribe(function (data) {
            console.log("points subscribe");
            var totalval = 0;
            var goodval = 0;
            var badval = 0;
            for (var _i = 0, _a = _this.Points(); _i < _a.length; _i++) {
                var p = _a[_i];
                if (_this.ContributorVal == -1) {
                    // debugger;
                    if (p.Approved == true) {
                        totalval = totalval + Number(p.Points);
                        p.Points < 0 ? badval = badval + p.Points : goodval = goodval + p.Points;
                    }
                }
                if (_this.ContributorVal != -1) {
                    // debugger;
                    if (p.Approved == false && p.ContributorId == _this.ContributorVal) {
                        totalval = totalval + Number(p.Points);
                        p.Points < 0 ? badval = badval + p.Points : goodval = goodval + p.Points;
                    }
                }
            }
            var testj = 99;
            _this.TotalPointsNum(totalval);
            _this.GoodPointsNum(goodval);
            _this.BadPointsNum(badval);
            //this.ChatVar.server.send("test", totalval.toString());
        });
        this.TotalPointsNum.subscribe(function (data) {
            var totalval = 0;
            var goodval = 0;
            var badval = 0;
            for (var _i = 0, _a = _this.Points(); _i < _a.length; _i++) {
                var p = _a[_i];
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
            read: function () {
                //let totalPoints: number = 0;
                //for (let i = 0; i < this.Points.length; i++) {
                //    totalPoints = totalPoints + this.Points()[i].Points;
                //}
                //return totalPoints;        
                return 0;
            }
        }).extend({ notify: 'always' });
        this.ComputeTotalPoints = ko.pureComputed(function () {
            var totalval = 0;
            console.log("triggered");
            for (var _i = 0, _a = _this.Points(); _i < _a.length; _i++) {
                var points = _a[_i];
                totalval = totalval + Number(points.Points);
            }
            return totalval.toFixed(2);
        }).extend({ notify: 'always' });
        //console.log(this.children);
        this.SelectedChild.subscribe(function (data) {
            //console.log(data
            if (_this.SelectedChild != null) {
                console.log("in subscribe");
                var test = _this.Points;
                _this.getPoints(test, _this.SelectedChild());
                var testnum = _this.SelectedChild().BankedPoints;
                console.log(testnum);
                _this.BankedPointsNum(testnum);
                _this.getChildRewards(_this.PointsAllocatedNum, _this.SelectedChild());
                _this.getPointsToBeReviewed(_this.PointsToBeReviewedNum, _this.SelectedChild());
            }
        });
    }
    // method and variables required to stop error with link on the rewards failing when no child selected
    PointViewModel.prototype.SelectChange = function (data) {
        console.log("listItemSelected");
        console.log(data);
        var currentChild = this.SelectedChild();
        if (currentChild != null) {
            this.NothingSelectedBool(false);
            this.ChildSelectedBool(true);
        }
        else {
            this.NothingSelectedBool(true);
            this.ChildSelectedBool(false);
        }
    };
    PointViewModel.prototype.BankPointsClick = function () {
        console.log("banking points");
        var pointscollected = this.TotalPointsNum();
        if (pointscollected > 0) {
            var adjustedpoints = this.BankedPointsNum() + pointscollected;
            this.BankedPointsNum(adjustedpoints);
            this.SelectedChild().BankedPoints = adjustedpoints;
            this.bankUpdate(this.SelectedChild(), this);
            this.Points.removeAll();
        }
    };
    PointViewModel.prototype.broadcastPointsChange = function (points, root) {
        var totalval = 0;
        var goodval = 0;
        var badval = 0;
        for (var _i = 0, _a = this.Points(); _i < _a.length; _i++) {
            var p = _a[_i];
            totalval = totalval + Number(p.Points);
            p.Points < 0 ? badval = badval + p.Points : goodval = goodval + p.Points;
        }
        //this.TotalPointsNum(totalval);
        //this.GoodPointsNum(goodval);
        //this.BadPointsNum(badval);
        //root.ChatVar.server.send("test", totalval.toString());
    };
    PointViewModel.prototype.saveItems = function (points, root) {
        var promise = $.ajax({
            url: "/Home/AddPoint/",
            cache: false,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(points),
            success: function (result) {
                var pointrtn = result;
                console.log(pointrtn);
                console.log(root.Points());
                var match = ko.utils.arrayFirst(root.Points(), function (item) {
                    return points.PointsId === item.PointsId;
                });
                match.PointsId = pointrtn.ChildId;
                match.AllocationDate = pointrtn.AllocationDate;
                //  root.broadcastPointsChange(root.points, root);
            },
        });
        promise.done(function (res) {
        });
    };
    PointViewModel.prototype.bankUpdate = function (child, root) {
        var promise = $.ajax({
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
    };
    return PointViewModel;
}());
//# sourceMappingURL=Points.js.map