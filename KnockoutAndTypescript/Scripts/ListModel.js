/// <reference path="typings/knockout/knockout.d.ts" />
/// <reference path="typings/knockout.mapping/knockout.mapping.d.ts" />
var ListModel = (function () {
    function ListModel(values) {
        //var test = ko.mapping.fromJS(values);
        //this.items = ko.mapping.fromJS(values);
        this.items = ko.observableArray(values);
        this.itemToAdd = ko.observable("");
    }
    ListModel.prototype.addRemoteItem = function (value) {
        this.values.push(value);
    };
    ListModel.prototype.addItem = function (value) {
        if (this.itemToAdd() != "") {
            var test = this.itemToAdd().toString();
            this.items.push(this.itemToAdd());
            this.itemToAdd("");
        }
    };
    return ListModel;
}());
//# sourceMappingURL=ListModel.js.map