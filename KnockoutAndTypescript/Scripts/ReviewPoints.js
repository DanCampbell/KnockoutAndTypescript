/// <reference path="typings/knockout/knockout.d.ts" />
/// <reference path="typings/knockout.mapping/knockout.mapping.d.ts" />
/// <reference path="typings/knockout.validation/knockout.validation.d.ts" />
/// <reference path="typings/jquery/jquery.d.ts" />
var PointReview = (function () {
    function PointReview() {
    }
    return PointReview;
}());
var ReviewPointsModel = (function () {
    function ReviewPointsModel(values, childvalue, mode) {
        var _this = this;
        var pb;
        console.log(values);
        pb = values.slice(0);
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
        this.mode = ko.observable(mode);
        for (var _i = 0, pb_1 = pb; _i < pb_1.length; _i++) {
            var p = pb_1[_i];
            //let p2: PointReview = p;
            var pval = p.BehaviourPoints;
            if (p.Saved == false) {
                totalval = totalval + pval;
                pval < 0 ? badval = badval + pval : goodval = goodval + pval;
            }
        }
        this.totalPoints(totalval);
        this.totalPlusPoints(goodval);
        this.totalMinusPoints(badval);
        this.points.subscribe(function (data) {
            console.log("points subscribe");
            var totalval = 0;
            var goodval = 0;
            var badval = 0;
            for (var _i = 0, _a = _this.points(); _i < _a.length; _i++) {
                var p = _a[_i];
                totalval = totalval + p.BehaviourPoints;
                p.BehaviourPoints < 0 ? badval = badval + p.BehaviourPoints : goodval = goodval + p.BehaviourPoints;
            }
            _this.totalPoints(totalval);
            _this.totalPlusPoints(goodval);
            _this.totalMinusPoints(badval);
            //this.ChatVar.server.send("test", totalval.toString());
        });
    }
    ReviewPointsModel.prototype.approvePoints = function (data) {
        if (this.mode() == 'Child') {
            var promise = $.ajax({
                url: "/Home/ApproveChildPoints/",
                cache: false,
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(this.child())
            });
            promise.done(function (res) {
            });
        }
        if (this.mode() == 'Contributor') {
            var promise = $.ajax({
                url: "/Home/ApproveContributorPoints/",
                cache: false,
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(this.points())
            });
            promise.done(function (res) {
            });
        }
    };
    ReviewPointsModel.prototype.deleteItem = function (index, root, data) {
        var curItem = data;
        //console.log(data);
        root.points.remove(function (item) { return item.PointId == curItem.PointId; });
        root.removeItems(curItem);
    };
    ReviewPointsModel.prototype.removeItems = function (data) {
        var curItem = data;
        var promise = $.ajax({
            url: "/Home/DeleteReviewPoint/",
            cache: false,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(curItem)
        });
        promise.done(function (res) {
        });
    };
    return ReviewPointsModel;
}());
//# sourceMappingURL=ReviewPoints.js.map