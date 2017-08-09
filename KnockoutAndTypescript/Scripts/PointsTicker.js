/// <reference path="typings/knockout/knockout.d.ts" />
/// <reference path="typings/knockout.mapping/knockout.mapping.d.ts" />
/// <reference path="typings/knockout.validation/knockout.validation.d.ts" />
/// <reference path="typings/jquery/jquery.d.ts" />
/// <reference path="typings/requirejs/require.d.ts" />
//import { Points } from './points';
var TestBroadcast = (function () {
    function TestBroadcast() {
    }
    return TestBroadcast;
}());
var PointTickerViewModel = (function () {
    function PointTickerViewModel() {
        this.ChatVar = null;
        this.Points = ko.observableArray([]);
        //let test1 = new TestBroadcast();
        //test1.Name = "Test";
        //test1.Message = "fist ";
        //this.Points.push(test1);
    }
    PointTickerViewModel.prototype.updateTicker = function (name, message) {
        var test1 = new TestBroadcast();
        test1.Name = name;
        test1.Message = message;
        this.Points.push(test1);
    };
    return PointTickerViewModel;
}());
//# sourceMappingURL=PointsTicker.js.map