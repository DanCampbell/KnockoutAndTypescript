/// <reference path="typings/knockout/knockout.d.ts" />
/// <reference path="typings/knockout.mapping/knockout.mapping.d.ts" />
/// <reference path="typings/knockout.validation/knockout.validation.d.ts" />
/// <reference path="typings/jquery/jquery.d.ts" />

class PointReview {
    PointId: number;
    ChildId: number;
    AllocationDate: string;
    Points: number;
    BehaviourId: number;
    BehaviourName: string;
    BehaviourPoints: number;
    Saved: boolean;
    ChildName: string;
    Approved: boolean;
}

class ReviewPointsModel {
    //values: string[];
    // goals: KnockoutObservableArray<Goal>;

    points: KnockoutObservableArray<PointReview>;
    totalPoints: KnockoutObservable<Number>;
    totalMinusPoints: KnockoutObservable<Number>;
    totalPlusPoints: KnockoutObservable<Number>;
    totalBankedPoints: KnockoutObservable<Number>;
    totalRewardPoints: KnockoutObservable<Number>;
    child: KnockoutObservable<Children>;
    Errors: KnockoutValidationErrors;
    mode: KnockoutObservable<string>;

    constructor(values: PointReview[], childvalue: Children, mode:string) {
        var pb: PointReview[];
        console.log(values);
        pb = values.slice(0);
        this.points = ko.observableArray(pb);
        this.child = ko.observable(childvalue)
        this.totalPoints = ko.observable(0);
        this.totalMinusPoints = ko.observable(0);
        this.totalPlusPoints = ko.observable(0);
        this.totalBankedPoints = ko.observable(0);
        this.totalRewardPoints = ko.observable(0);
        // this.AutoCreate = ko.observable(false);

        this.totalBankedPoints(childvalue.BankedPoints);
        this.Errors = ko.validation.group(this);
        let totalval = 0;
        let goodval = 0;
        let badval = 0;
        this.mode = ko.observable(mode);
       
        for (let p of pb) {
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
        

        this.points.subscribe((data) => {
            console.log("points subscribe");
            let totalval = 0;
            let goodval = 0;
            let badval = 0;
            for (let p of this.points()) {
                totalval = totalval + p.BehaviourPoints;
                p.BehaviourPoints < 0 ? badval = badval + p.BehaviourPoints : goodval = goodval + p.BehaviourPoints;
            }
            this.totalPoints(totalval);
            this.totalPlusPoints(goodval);
            this.totalMinusPoints(badval);
            //this.ChatVar.server.send("test", totalval.toString());
        });
    }

    approvePoints(data) {
        if (this.mode() == 'Child') {
            var promise =
                $.ajax({
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
            var promise =
                $.ajax({
                    url: "/Home/ApproveContributorPoints/",
                    cache: false,
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify(this.points())
                });
            promise.done(function (res) {

            });
        }
    }

    deleteItem(index, root, data) {
        let curItem = <PointReview>data;
        //console.log(data);
        root.points.remove((item: PointReview) => { return item.PointId == curItem.PointId });
        
        root.removeItems(curItem);
    }

    removeItems(data) {
        let curItem = <PointReview>data;

        var promise =
            $.ajax({
                url: "/Home/DeleteReviewPoint/",
                cache: false,
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(curItem)
            });
        promise.done(function (res) {

        });
    }

     
}