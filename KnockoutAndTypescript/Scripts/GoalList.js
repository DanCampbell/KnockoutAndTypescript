/// <reference path="typings/knockout/knockout.d.ts" />
/// <reference path="typings/knockout.mapping/knockout.mapping.d.ts" />
/// <reference path="typings/knockout.validation/knockout.validation.d.ts" />
/// <reference path="typings/jquery/jquery.d.ts" />
var Goal = (function () {
    function Goal() {
    }
    return Goal;
}());
//class Selected {
//    optionText: string;
//    optionValue: boolean;
//}
var GoalListModel = (function () {
    function GoalListModel(values) {
        this.goals = ko.observableArray(values);
        this.GoalName = ko.observable("").extend({ required: true });
        this.GoalPointsRequired = ko.observable(0).extend({ required: true });
        this.AutoCreate = ko.observable(false);
        this.Errors = ko.validation.group(this);
    }
    GoalListModel.prototype.deleteItem = function (index, root, data) {
        var curItem = data;
        //console.log(data);
        root.goals.remove(function (item) { return item.GoalName == curItem.GoalName && item.GoalId == curItem.GoalId; });
        root.removeItems(curItem);
    };
    GoalListModel.prototype.addItem = function (root) {
        if (root.GoalName() != "") {
            var nextIndex = root.goals().length;
            var t1 = new Goal();
            t1.GoalId = nextIndex;
            t1.GoalPointsRequired = root.GoalPointsRequired();
            t1.GoalName = root.GoalName().toString();
            t1.AutoCreate = root.AutoCreate();
            root.goals.push(t1);
            root.saveItems(t1, root);
        }
        else {
            //ko.validatedObservable(this.ChildName);
            this.Errors = ko.validation.group(this.GoalPointsRequired, this.GoalName);
        }
    };
    GoalListModel.prototype.saveItems = function (data, root) {
        var curItem = data;
        var promise = $.ajax({
            url: "/Home/AddGoal/",
            cache: false,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(curItem),
            success: function (result) {
                var goal = result;
                console.log(root);
                var match = ko.utils.arrayFirst(root.goals(), function (item) {
                    return data.GoalId === item.GoalId;
                });
                match.GoalId = goal.GoalId; // set Id to that returned from Server
            },
        });
        promise.done(function (res) {
        });
    };
    GoalListModel.prototype.removeItems = function (data) {
        var curItem = data;
        var promise = $.ajax({
            url: "/Home/DeleteGoal/",
            cache: false,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(curItem)
        });
        promise.done(function (res) {
        });
    };
    return GoalListModel;
}());
//# sourceMappingURL=GoalList.js.map