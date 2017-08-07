/// <reference path="typings/knockout/knockout.d.ts" />
/// <reference path="typings/knockout.mapping/knockout.mapping.d.ts" />
/// <reference path="typings/knockout.validation/knockout.validation.d.ts" />
/// <reference path="typings/jquery/jquery.d.ts" />
var PointBehaviour = (function () {
    function PointBehaviour() {
    }
    return PointBehaviour;
}());
var ChildViewModel = (function () {
    function ChildViewModel() {
    }
    return ChildViewModel;
}());
var ChildDetailModel = (function () {
    function ChildDetailModel(values, childvalue) {
        var pb;
        pb = values.ChildVM.slice(0);
        this.points = ko.observableArray(pb);
        this.child = ko.observable(childvalue);
        this.totalPoints = ko.observable(0);
        this.totalMinusPoints = ko.observable(0);
        this.totalPlusPoints = ko.observable(0);
        this.totalBankedPoints = ko.observable(0);
        this.totalRewardPoints = ko.observable(0);
        // this.AutoCreate = ko.observable(false);
        this.totalBankedPoints(childvalue.BankedPoints);
        this.Errors = ko.validation.group(this);
        var totalval = 0;
        var goodval = 0;
        var badval = 0;
        for (var _i = 0, _a = this.points(); _i < _a.length; _i++) {
            var p = _a[_i];
            if (p.Saved == false) {
                totalval = totalval + Number(p.Points);
                p.Points < 0 ? badval = badval + p.Points : goodval = goodval + p.Points;
            }
        }
        this.totalPoints(totalval);
        this.totalPlusPoints(goodval);
        this.totalMinusPoints(badval);
        //debugger;
    }
    ChildDetailModel.prototype.BankPointsClick = function () {
        console.log("banking points");
        var pointscollected = this.totalPoints();
        if (pointscollected > 0) {
            var adjustedpoints = Number(this.totalPoints()) + Number(this.totalBankedPoints());
            this.totalBankedPoints(adjustedpoints);
            this.child().BankedPoints = adjustedpoints;
            this.bankUpdate(this.child(), this);
            this.totalPoints(0);
            this.totalPlusPoints(0);
            this.totalMinusPoints(0);
        }
    };
    ChildDetailModel.prototype.bankUpdate = function (child, root) {
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
    return ChildDetailModel;
}());
//# sourceMappingURL=ChildDetail.js.map