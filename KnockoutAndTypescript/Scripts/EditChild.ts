/// <reference path="typings/knockout/knockout.d.ts" />
/// <reference path="typings/knockout.mapping/knockout.mapping.d.ts" />
/// <reference path="typings/knockout.validation/knockout.validation.d.ts" />
/// <reference path="typings/jquery/jquery.d.ts" />
/// <reference path="kidipointsclasses.ts" />
/// <reference path="childrenlist.ts" />
/// <reference path="typings/requirejs/require.d.ts" />

//import { Child } from "./KidipointsClasses";
import child = require("./KidipointsClasses");

class test123 {
    optionText: string;
    optionValue: boolean;
}


class ChildEditModel {
    ChildId: KnockoutObservable<number>;
    ChildName: KnockoutObservable<string>;
    ChildAge: KnockoutObservable<number>;
    ChildSex: KnockoutObservable<boolean>;
    UserAuthCode: KnockoutObservable<string>;
    UserImage: KnockoutObservable<string>;
    SexOptions: KnockoutObservableArray<Selected>;
    SexSelected: KnockoutObservable<boolean>;
    Errors: KnockoutValidationErrors;
    ChildUpdate: () => void;
    GetNewCode: () => void;
    RtnUri: string;
    

    constructor(value: Children, rtnvalue: string) {
        // debugger;
        this.ChildName = ko.observable(value.ChildName).extend({ required: true });
        this.ChildAge = ko.observable(value.ChildAge).extend({ required: true });
        this.ChildSex = ko.observable(value.ChildSex);
        this.ChildId = ko.observable(value.ChildId);
        this.UserAuthCode = ko.observable(value.UserAuthCode);
        this.UserImage = ko.observable(value.UserImage);
        this.SexOptions = ko.observableArray([]);
        this.RtnUri = rtnvalue;

        var arr: Selected[] = [{ "optionText": "female", "optionValue": false }, { "optionText": "male", "optionValue": true }];
        this.SexOptions = ko.observableArray(arr);
        this.SexSelected = ko.observable(value.ChildSex);
        this.Errors = ko.validation.group(this);
        //var testc = new test123();
        //testc.optionText = "test";
        //import moo = module('kidipoints');
        //var testk = new kidipoints.Child();
       
        //let test: child.Child;
       
        //var test2 = <child.Child>{};
        //test2.ChildName = "test2"
        //var test3 = <Children>{};
        //test3.ChildName = "test3";

       // debugger;
        this.ChildUpdate = () => {
          //  debugger;
            var self = this;
            var test = this.ChildName().valueOf();
            var updateVar = <Children>{};
            updateVar.ChildName = this.ChildName();
            updateVar.ChildSex = this.SexSelected();
            updateVar.ChildAge = this.ChildAge();
            updateVar.UserAuthCode = this.UserAuthCode();
            updateVar.ChildId = this.ChildId();

            var promise =
                $.ajax({
                    url: "/Home/EditChildValues/",
                    cache: false,
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify(updateVar),
                    success: function (result) {
                    },
                });
            promise.done(function (res) {
                window.location.href = self.RtnUri;
            });
        }

        this.GetNewCode = () => {            
            var self = this;
            var promise =
                $.ajax({
                    url: "/Home/GetNewUserAuthCode/",
                    cache: false,
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    //dataType: 'json',
                    // data: { "id": childid, "name": this.TabPanelName() },
                    // data: { "id": childid },
                    success: function (result) {
                        self.UserAuthCode(result);
                    },
                });
            promise.done(function (res) {

            });
        }
    }

    UpdateChildClick() {
        //console.log("update Child");
        this.ChildUpdate();
    }

    CodeClick() {
        //console.log("update Child");
     //   debugger;
        this.GetNewCode();
    }

    

}

    