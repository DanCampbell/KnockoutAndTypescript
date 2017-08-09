/// <reference path="typings/knockout/knockout.d.ts" />
/// <reference path="typings/knockout.mapping/knockout.mapping.d.ts" />
/// <reference path="typings/knockout.validation/knockout.validation.d.ts" />
/// <reference path="typings/jquery/jquery.d.ts" />
/// <reference path="typings/requirejs/require.d.ts" />

//import { Points } from './points';
class TestBroadcast {
    Name:string;
    Message: string;
}

class PointTickerViewModel {
    
    ChatVar: any;
    Points: KnockoutObservableArray<TestBroadcast>;
    constructor() {
        this.ChatVar = null;
        this.Points = ko.observableArray([]);
        //let test1 = new TestBroadcast();
        //test1.Name = "Test";
        //test1.Message = "fist ";
        //this.Points.push(test1);
    }

    updateTicker(name: any, message: any) {
        let test1 = new TestBroadcast();
        test1.Name = name;
        test1.Message = message;
        this.Points.push(test1);
    }
}