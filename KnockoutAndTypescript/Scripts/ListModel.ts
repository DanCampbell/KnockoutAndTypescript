/// <reference path="typings/knockout/knockout.d.ts" />
/// <reference path="typings/knockout.mapping/knockout.mapping.d.ts" />

class ListModel {
    values: string[];
    items: KnockoutObservableArray<string>;
    item: KnockoutObservable<string>;
    itemToAdd: KnockoutObservable<string>;

    constructor(values: string[]) {
        //var test = ko.mapping.fromJS(values);
        //this.items = ko.mapping.fromJS(values);
        this.items = ko.observableArray(values);
        this.itemToAdd = ko.observable("");
    }

    addRemoteItem(value:string){
        this.values.push(value);
    }

    addItem(value: string) {
        if (this.itemToAdd() != "") {
            var test = this.itemToAdd().toString();
            this.items.push(this.itemToAdd());
            this.itemToAdd("");
        }
    }

}