/// <reference path="typings/knockout/knockout.d.ts" />
/// <reference path="typings/knockout.mapping/knockout.mapping.d.ts" />
/// <reference path="typings/knockout.validation/knockout.validation.d.ts" />
/// <reference path="typings/jquery/jquery.d.ts" />
/// <reference path="typings/requirejs/require.d.ts" />
/// <reference path="typings/chartjs/index.d.ts" />
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
        var self = this;
        //let test1 = new TestBroadcast();
        //test1.Name = "Test";
        //test1.Message = "fist ";
        //this.Points.push(test1);
        self.lineChartData = ko.observable({
            labels: ["January", "February", "March", "April", "May", "June", "July"],
            datasets: [
                {
                    fillColor: "rgba(151,187,205,0.5)",
                    strokeColor: "rgba(151,187,205,1)",
                    pointColor: "rgba(151,187,205,1)",
                    pointStrokeColor: "#fff",
                    data: [65, 59, 90, 81, 56, 55, 40]
                }
            ]
        });
    }
    PointTickerViewModel.prototype.initLine = function () {
        var options = {
            animation: false,
            scaleOverride: true,
            scaleSteps: 10,
            scaleStepWidth: 10,
            scaleStartValue: 10 //Number - The scale starting value
        };
        //var canvas = document.getElementById("canvas");
        //var context = canvas.getContext('2d');
        //var ctx = $("#canvas").get(0).getContext("2d");
        var myLine = new Chart(this.context).Line(this.lineChartData(), options);
    };
    PointTickerViewModel.prototype.updateTicker = function (name, message) {
        var test1 = new TestBroadcast();
        test1.Name = name;
        test1.Message = message;
        this.Points.push(test1);
    };
    return PointTickerViewModel;
}());
//# sourceMappingURL=PointsTicker.js.map