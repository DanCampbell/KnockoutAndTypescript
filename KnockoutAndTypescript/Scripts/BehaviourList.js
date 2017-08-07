/// <reference path="typings/knockout/knockout.d.ts" />
/// <reference path="typings/knockout.mapping/knockout.mapping.d.ts" />
/// <reference path="typings/knockout.validation/knockout.validation.d.ts" />
/// <reference path="typings/jquery/jquery.d.ts" />
var Behaviour = (function () {
    function Behaviour() {
    }
    return Behaviour;
}());
//class Selected {
//    optionText: string;
//    optionValue: boolean;
//}
var BehaviourListModel = (function () {
    function BehaviourListModel(values) {
        this.behaviours = ko.observableArray(values);
        this.BehaviourName = ko.observable("").extend({ required: true });
        this.BehaviourPoints = ko.observable(0).extend({ required: true, number: true });
        this.Errors = ko.validation.group(this);
    }
    BehaviourListModel.prototype.deleteItem = function (index, root, data) {
        var curItem = data;
        root.behaviours.remove(function (item) { return item.BehaviourName == curItem.BehaviourName && item.BehaviourId == curItem.BehaviourId; });
        root.removeItems(curItem);
    };
    BehaviourListModel.prototype.addItem = function (root) {
        var _this = this;
        if (root.BehaviourName() != "") {
            var match = ko.utils.arrayFirst(root.behaviours(), function (item) {
                return _this.BehaviourName() === item.BehaviourName;
            });
            if (match == null) {
                var nextIndex = root.behaviours().length;
                var b1 = new Behaviour();
                b1.BehaviourId = nextIndex;
                b1.BehaviourPoints = root.BehaviourPoints();
                b1.BehaviourName = root.BehaviourName().toString();
                root.behaviours.push(b1);
                root.saveItems(b1, root);
            }
            else {
                this.BehaviourName.isValid(false);
            }
        }
        else {
            this.Errors = ko.validation.group(this.BehaviourName, this.BehaviourPoints);
        }
    };
    BehaviourListModel.prototype.saveItems = function (data, root) {
        var curItem = data;
        var promise = $.ajax({
            url: "/Home/AddBehaviour/",
            cache: false,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(curItem),
            success: function (result) {
                var behaviour = result;
                //console.log(root);
                var match = ko.utils.arrayFirst(root.behaviours(), function (item) {
                    return data.BehaviourId === item.BehaviourId;
                });
                match.BehaviourId = curItem.BehaviourId;
            },
        });
        promise.done(function (res) {
        });
    };
    BehaviourListModel.prototype.removeItems = function (data) {
        var curItem = data;
        var promise = $.ajax({
            url: "/Home/DeleteBehaviour/",
            cache: false,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(curItem)
        });
        promise.done(function (res) {
        });
    };
    return BehaviourListModel;
}());
//# sourceMappingURL=BehaviourList.js.map