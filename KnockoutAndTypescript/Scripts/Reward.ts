/// <reference path="typings/knockout/knockout.d.ts" />
/// <reference path="typings/knockout.mapping/knockout.mapping.d.ts" />
/// <reference path="typings/knockout.validation/knockout.validation.d.ts" />
/// <reference path="typings/jquery/jquery.d.ts" />

class ChildRewardsVM {
    index:number
    GoalName: string;
    GoalId: number;
    GoalPoints: number;
    GoalCompletePoints: number;
    GoalPointsOriginal: number;
    WantReward: boolean;
    SliderVisible: boolean;
    Administrator: boolean;
    GiveReward: boolean;
    RewardComplete: boolean;
    ChildId: number;
}

class RewardAwarded {  
    PointsAllocated: number;
    GoalDescription: string;
    RewardReceivedDate: string;
}

class ChildRewardsVMList {
    ChildRewardsList: ChildRewardsVM[];
}

class RewardViewModel {
    Child: Children;
    ChildRewardsVM: ChildRewardsVM[];
    Administrator: boolean;
    UserLevel: boolean;
    RewardsAwardedVM: RewardAwarded[];
    ShowRewards: boolean;
    ShowAllocation: boolean;
}

class RewardsModel {
    RewardsAry: KnockoutObservableArray<ChildRewardsVM>;
    AwardedAry: KnockoutObservableArray<RewardAwarded>;
    TestSlider: KnockoutObservable<number>;
    BankedPoints: KnockoutObservable<number>;
    Child: KnockoutObservable<Children>;
    Errors: KnockoutValidationErrors;
    TestFunction: (val: any) => void;
    Administrator: boolean;
    UserLevel: boolean;
    ShowRewards: boolean;
    ShowAllocation: boolean;

    constructor(values: RewardViewModel) {
        var cr: ChildRewardsVM[];
        //debugger;
        let test = values.ChildRewardsVM;
        cr = test.slice(0);
        var ra: RewardAwarded[];
        let ary2 = values.RewardsAwardedVM;
        ra = ary2.slice(0);

        this.Administrator = values.Administrator;
        this.UserLevel = values.UserLevel;
        this.ShowAllocation = values.ShowAllocation;
        this.ShowRewards = values.ShowRewards;
        this.Child = ko.observable(values.Child);
        this.BankedPoints = ko.observable(this.Child().BankedPoints);
        this.TestFunction = (val) => {
            console.log(val);
        }
                
        this.TestSlider = ko.observable(20);

        let inx: number = 0;

        for (let reward of cr)
        {
            cr[inx].index = inx;
            inx = inx + 1;
            //reward.SliderVisible = true;
            //debugger;
        }
        this.RewardsAry = ko.observableArray(cr);
        this.AwardedAry = ko.observableArray(ra);

        this.RewardsAry.valueHasMutated();
        //this.RewardsAry.notifySubscribers(this.RewardsAry());

        this.RewardsAry.subscribe((data) => {
          //  console.log(data);
        });
    }

    SaveChanges() {
        let child = this.Child();
        child.BankedPoints = this.BankedPoints();
        let rtnary: ChildRewardsVM[] = this.RewardsAry();
        var rtnstring = "http://localhost:61908/Home/Points?childid=" + this.Child().ChildId;
        var rtnuser = "http://localhost:61908/Home/childdetail?childid=" + this.Child().ChildId;
       // console.log(rtnary);
        //for (let reward of this.RewardsAry()) {

        //}
        var promise =
            $.ajax({
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
            var promise =
                $.ajax({
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
            var promise =
                $.ajax({
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
    }

    CheckWantReward(val: ChildRewardsVM) {
        if (val.WantReward == true) {           
            val.SliderVisible = false;
        } else {
            val.SliderVisible = true;
        }

        var changedIdx = this.RewardsAry.indexOf(val);
        this.RewardsAry.splice(changedIdx, 1); // removes the item from the array
        this.RewardsAry.splice(changedIdx, 0, val); // adds it back

        this.RewardsAry.valueHasMutated();
       // return false;
    }

    CheckGiveReward(val: ChildRewardsVM) {
        if (val.GiveReward == true) {
            val.SliderVisible = false;
        } else {
            val.SliderVisible = true;
        }

        var changedIdx = this.RewardsAry.indexOf(val);
        this.RewardsAry.splice(changedIdx, 1); // removes the item from the array
        this.RewardsAry.splice(changedIdx, 0, val); // adds it back

        this.RewardsAry.valueHasMutated();
        // return false;
    }

    MaxPointsToGoal(root: RewardsModel, index: number, val: ChildRewardsVM) {
        console.log(val.GoalPoints);
        var bankpoints = root.BankedPoints();
        let difference: number = 0;
        let bankadjust: number = 0;

        if (val.GoalPoints < val.GoalCompletePoints)
        {
            let diftocomplete = val.GoalCompletePoints - val.GoalPoints;
            if (bankpoints > diftocomplete)
            {
                bankadjust = bankpoints - diftocomplete;
                val.GoalPoints = val.GoalCompletePoints;
            } else
            {
                bankadjust = 0;
                let newbalance = val.GoalPoints + bankpoints;
                val.GoalPoints = newbalance;
            }
          //  root.BankedPoints(bankadjust);
        }

            //val.GoalPoints = 0;
            //val.SliderVisible = true;
            //val.GoalPointsOriginal = 0;
            //var newbankpoints = bankpoints + val.GoalPoints;
            //root.BankedPoints(newbankpoints);
       

        if (val.GoalPoints > val.GoalPointsOriginal) {
            difference = val.GoalPoints - val.GoalPointsOriginal;
            bankpoints = bankpoints - difference;
            if (bankpoints < 0) { val.GoalPoints = val.GoalPointsOriginal } else {
                root.BankedPoints(bankpoints);
                val.GoalPointsOriginal = val.GoalPoints;
            }
        }
        if (val.GoalPoints < val.GoalPointsOriginal) {

            difference = val.GoalPointsOriginal - val.GoalPoints;
            //  console.log(difference);
            bankpoints = bankpoints + difference;
            if (bankpoints < 0) { val.GoalPoints = val.GoalPointsOriginal } else {
                root.BankedPoints(bankpoints);
                val.GoalPointsOriginal = val.GoalPoints;
            }
        }

        var changedIdx = root.RewardsAry.indexOf(val);
        root.RewardsAry.splice(changedIdx, 1); // removes the item from the array
        root.RewardsAry.splice(changedIdx, 0, val); // adds it back

        root.RewardsAry.valueHasMutated();
        root.AwardedAry.notifySubscribers();
    }

    PutPointsBack(root: RewardsModel, index: number, val: ChildRewardsVM  ){
        console.log(val.GoalPoints);
        var bankpoints = root.BankedPoints();
        let difference: number = 0;
        if (val.GoalPoints > 0) {
            val.GoalPoints = 0;
            val.SliderVisible = true;
            //val.GoalPointsOriginal = 0;
            //var newbankpoints = bankpoints + val.GoalPoints;
            //root.BankedPoints(newbankpoints);
        }

        if (val.GoalPoints > val.GoalPointsOriginal) {
            difference = val.GoalPoints - val.GoalPointsOriginal;
            bankpoints = bankpoints - difference;
            if (bankpoints < 0) { val.GoalPoints = val.GoalPointsOriginal } else {
                root.BankedPoints(bankpoints);
                val.GoalPointsOriginal = val.GoalPoints;
            }
        }
        if (val.GoalPoints < val.GoalPointsOriginal) {

            difference = val.GoalPointsOriginal - val.GoalPoints;
            //  console.log(difference);
            bankpoints = bankpoints + difference;
            if (bankpoints < 0) { val.GoalPoints = val.GoalPointsOriginal } else {
                root.BankedPoints(bankpoints);
                val.GoalPointsOriginal = val.GoalPoints;
            }
        }

        var changedIdx = root.RewardsAry.indexOf(val);
        root.RewardsAry.splice(changedIdx, 1); // removes the item from the array
        root.RewardsAry.splice(changedIdx, 0, val); // adds it back

        root.RewardsAry.valueHasMutated();
        root.AwardedAry.notifySubscribers();
    }

    ChangeSlider(val: ChildRewardsVM)
    {
        //if (val.GoalPoints > 75) {
        //    val.GoalPoints = 75;
        //}
        var bankpoints = this.BankedPoints();
       // console.log(val.GoalPoints);
       // console.log(val.GoalPointsOriginal);
        let difference: number = 0;
        //if (val.WantReward == true) {
        // //   val.GoalPoints = val.GoalCompletePoints;
        // //   val.GoalPointsOriginal = val.GoalCompletePoints;
        //    val.SliderVisible = false;
        //}
       // console.log(this.RewardsAry);

     //   if (val.WantReward == false) {
            if (val.GoalPoints > val.GoalPointsOriginal) {
                difference = val.GoalPoints - val.GoalPointsOriginal;
                bankpoints = bankpoints - difference;
                if (bankpoints < 0) { val.GoalPoints = val.GoalPointsOriginal } else {
                    this.BankedPoints(bankpoints);
                    val.GoalPointsOriginal = val.GoalPoints;
                }
            }
            if (val.GoalPoints < val.GoalPointsOriginal) {

                difference = val.GoalPointsOriginal - val.GoalPoints;
              //  console.log(difference);
                bankpoints = bankpoints + difference;
                if (bankpoints < 0) { val.GoalPoints = val.GoalPointsOriginal } else {
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
    }
}