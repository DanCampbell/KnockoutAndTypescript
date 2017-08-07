/// <reference path="typings/knockout/knockout.d.ts" />
/// <reference path="typings/knockout.mapping/knockout.mapping.d.ts" />
/// <reference path="typings/knockout.validation/knockout.validation.d.ts" />
/// <reference path="typings/jquery/jquery.d.ts" />

class Goal{
    //ChildId: KnockoutObservable<number>;
    //ChildName: KnockoutObservable<string>;
    //ChildAge: KnockoutObservable<number>;
    //ChildSex: KnockoutObservable<boolean>;    
    GoalId: number;
    GoalName: string;
    GoalPointsRequired: number;
    AutoCreate: boolean;    
}

//class Selected {
//    optionText: string;
//    optionValue: boolean;
//}

class GoalListModel {
    //values: string[];
    goals: KnockoutObservableArray<Goal>;
    GoalName: KnockoutObservable<string>;
    GoalPointsRequired: KnockoutObservable<number>;
    AutoCreate: KnockoutObservable<boolean>;   
    Errors: KnockoutValidationErrors;
   
    constructor(values: Goal[]) {        
        this.goals = ko.observableArray(values);
        this.GoalName = ko.observable("").extend({required:true });
        this.GoalPointsRequired = ko.observable(0).extend({required:true});
        this.AutoCreate = ko.observable(false);     
        
        this.Errors = ko.validation.group(this);     
    }

    deleteItem(index, root, data) {
    let curItem = <Goal>data;
    //console.log(data);
    root.goals.remove((item: Goal) => { return item.GoalName == curItem.GoalName && item.GoalId == curItem.GoalId });
    root.removeItems(curItem);    
    }
        
    addItem(root) {
       
        if (root.GoalName() != "") {
            
            let nextIndex: number = root.goals().length;
            let t1 = new Goal();
            t1.GoalId = nextIndex;
            t1.GoalPointsRequired = <number>root.GoalPointsRequired();
            t1.GoalName = root.GoalName().toString();
            t1.AutoCreate = <boolean>root.AutoCreate();          
            root.goals.push(t1);
            root.saveItems(t1, root);            
        }
        else {
            //ko.validatedObservable(this.ChildName);
            this.Errors = ko.validation.group(this.GoalPointsRequired, this.GoalName);
        }
    }

    saveItems(data, root) {
        let curItem = <Goal>data;
        
       
        var promise =
            $.ajax({
                url: "/Home/AddGoal/",
                cache: false,
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(curItem),
                success: function (result) {
                    
                    let goal: Goal = <Goal>result;
                    console.log(root);
                    let match: Goal = ko.utils.arrayFirst(root.goals(), (item: Goal) =>  {
                        return data.GoalId === item.GoalId;
                    });
                    
                    match.GoalId = goal.GoalId; // set Id to that returned from Server
                   
                },
            });
        promise.done(function (res) {
          
        });
    }   

    removeItems(data) {
        let curItem = <Goal>data;

        var promise =
            $.ajax({
                url: "/Home/DeleteGoal/",
                cache: false,
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(curItem)
            });
        promise.done(function (res) {

        });
    }

}
