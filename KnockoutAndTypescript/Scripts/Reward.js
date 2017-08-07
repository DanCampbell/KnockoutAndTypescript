/// <reference path="typings/knockout/knockout.d.ts" />
/// <reference path="typings/knockout.mapping/knockout.mapping.d.ts" />
/// <reference path="typings/knockout.validation/knockout.validation.d.ts" />
/// <reference path="typings/jquery/jquery.d.ts" />
var ChildRewardsVM = (function () {
    function ChildRewardsVM() {
    }
    return ChildRewardsVM;
}());
var RewardAwarded = (function () {
    function RewardAwarded() {
    }
    return RewardAwarded;
}());
var ChildRewardsVMList = (function () {
    function ChildRewardsVMList() {
    }
    return ChildRewardsVMList;
}());
var RewardViewModel = (function () {
    function RewardViewModel() {
    }
    return RewardViewModel;
}());
var RewardsModel = (function () {
    function RewardsModel(values) {
        var cr;
        //debugger;
        var test = values.ChildRewardsVM;
        cr = test.slice(0);
        var ra;
        var ary2 = values.RewardsAwardedVM;
        ra = ary2.slice(0);
        this.Administrator = values.Administrator;
        this.UserLevel = values.UserLevel;
        this.ShowAllocation = values.ShowAllocation;
        this.ShowRewards = values.ShowRewards;
        this.Child = ko.observable(values.Child);
        this.BankedPoints = ko.observable(this.Child().BankedPoints);
        this.TestFunction = function (val) {
            console.log(val);
        };
        this.TestSlider = ko.observable(20);
        var inx = 0;
        for (var _i = 0, cr_1 = cr; _i < cr_1.length; _i++) {
            var reward = cr_1[_i];
            cr[inx].index = inx;
            inx = inx + 1;
        }
        this.RewardsAry = ko.observableArray(cr);
        this.AwardedAry = ko.observableArray(ra);
        this.RewardsAry.valueHasMutated();
        //this.RewardsAry.notifySubscribers(this.RewardsAry());
        this.RewardsAry.subscribe(function (data) {
            //  console.log(data);
        });
    }
    RewardsModel.prototype.SaveChanges = function () {
        var child = this.Child();
        child.BankedPoints = this.BankedPoints();
        var rtnary = this.RewardsAry();
        var rtnstring = "http://localhost:61908/Home/Points?childid=" + this.Child().ChildId;
        var rtnuser = "http://localhost:61908/Home/childdetail?childid=" + this.Child().ChildId;
        // console.log(rtnary);
        //for (let reward of this.RewardsAry()) {
        //}
        var promise = $.ajax({
            url: "/Home/UpdateChild/",
            cache: false,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(child),
            success: function (result) {
            },
        });
        promise.done(function (res) {
        });
        if (this.UserLevel == true) {
            var promise = $.ajax({
                url: "/Home/UpdateRewards/",
                cache: false,
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(rtnary),
                success: function (result) {
                },
            });
            promise.done(function (res) {
                window.location.href = rtnuser;
            });
        }
        if (this.Administrator == true) {
            var promise = $.ajax({
                url: "/Home/UpdateRewardsAdministrator/",
                cache: false,
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(rtnary),
                success: function (result) {
                },
            });
            promise.done(function (res) {
                window.location.href = rtnstring;
            });
        }
    };
    RewardsModel.prototype.CheckWantReward = function (val) {
        if (val.WantReward == true) {
            val.SliderVisible = false;
        }
        else {
            val.SliderVisible = true;
        }
        var changedIdx = this.RewardsAry.indexOf(val);
        this.RewardsAry.splice(changedIdx, 1); // removes the item from the array
        this.RewardsAry.splice(changedIdx, 0, val); // adds it back
        this.RewardsAry.valueHasMutated();
        // return false;
    };
    RewardsModel.prototype.CheckGiveReward = function (val) {
        if (val.GiveReward == true) {
            val.SliderVisible = false;
        }
        else {
            val.SliderVisible = true;
        }
        var changedIdx = this.RewardsAry.indexOf(val);
        this.RewardsAry.splice(changedIdx, 1); // removes the item from the array
        this.RewardsAry.splice(changedIdx, 0, val); // adds it back
        this.RewardsAry.valueHasMutated();
        // return false;
    };
    RewardsModel.prototype.ChangeSlider = function (val) {
        //if (val.GoalPoints > 75) {
        //    val.GoalPoints = 75;
        //}
        var bankpoints = this.BankedPoints();
        // console.log(val.GoalPoints);
        // console.log(val.GoalPointsOriginal);
        var difference = 0;
        //if (val.WantReward == true) {
        // //   val.GoalPoints = val.GoalCompletePoints;
        // //   val.GoalPointsOriginal = val.GoalCompletePoints;
        //    val.SliderVisible = false;
        //}
        console.log(this.RewardsAry);
        //   if (val.WantReward == false) {
        if (val.GoalPoints > val.GoalPointsOriginal) {
            difference = val.GoalPoints - val.GoalPointsOriginal;
            bankpoints = bankpoints - difference;
            if (bankpoints < 0) {
                val.GoalPoints = val.GoalPointsOriginal;
            }
            else {
                this.BankedPoints(bankpoints);
                val.GoalPointsOriginal = val.GoalPoints;
            }
        }
        if (val.GoalPoints < val.GoalPointsOriginal) {
            difference = val.GoalPointsOriginal - val.GoalPoints;
            console.log(difference);
            bankpoints = bankpoints + difference;
            if (bankpoints < 0) {
                val.GoalPoints = val.GoalPointsOriginal;
            }
            else {
                this.BankedPoints(bankpoints);
                val.GoalPointsOriginal = val.GoalPoints;
            }
        }
        //let match: ChildRewardsVM = ko.utils.arrayFirst(this.RewardsAry(), (item: ChildRewardsVM) => {
        //    return val.GoalId === item.GoalId;
        //});
        // debugger;
        //match.GoalPoints = val.GoalPoints;
        //console.log(this.TestSlider());
        //console.log()
        //}
        var changedIdx = this.RewardsAry.indexOf(val);
        this.RewardsAry.splice(changedIdx, 1); // removes the item from the array
        this.RewardsAry.splice(changedIdx, 0, val); // adds it back
        this.RewardsAry.valueHasMutated();
        //this.RewardsAry.notifySubscribers(this.RewardsAry());
        //$("#testtext").text("changed");
    };
    return RewardsModel;
}());
//# sourceMappingURL=Reward.js.map