/// <reference path="typings/knockout/knockout.d.ts" />
/// <reference path="typings/knockout.mapping/knockout.mapping.d.ts" />
/// <reference path="typings/knockout.validation/knockout.validation.d.ts" />
/// <reference path="typings/jquery/jquery.d.ts" />

 class Children{
    //ChildId: KnockoutObservable<number>;
    //ChildName: KnockoutObservable<string>;
    //ChildAge: KnockoutObservable<number>;
    //ChildSex: KnockoutObservable<boolean>;    
    ChildId: number;
    ChildName: string;
    ChildAge: number;
    ChildSex: boolean;
    BankedPoints: number;
    UserImage: string;
    ParentUser: string;
    UserAuthCode: string;
    CanBank: boolean;
}

 class Selected {
    optionText: string;
    optionValue: boolean;
}

class ChildrenListModel {
    //values: string[];
    children: KnockoutObservableArray<Children>;
    ChildName: KnockoutObservable<string>;
    ChildAge: KnockoutObservable<number>;
    ChildSex: KnockoutObservable<boolean>;
    UserImage: KnockoutObservable<string>;
    SexOptions: KnockoutObservableArray<Selected>;
    SexSelected: KnockoutObservable<boolean>;
    ParentUser: string;
    Errors: KnockoutValidationErrors;
   
    constructor(values: Children[], parentuser:string) {
        //var test = ko.mapping.fromJS(values);
        //this.items = ko.mapping.fromJS(values);
        this.ParentUser = parentuser;
        this.children = ko.observableArray(values);
        this.ChildName = ko.observable("").extend({required:true });
        this.ChildAge = ko.observable(0).extend({required:true});
        this.ChildSex = ko.observable(true);
        this.SexOptions = ko.observableArray([]);
        //Selected test = new Selected("male", true);
        //this.SexOptions.push(new Selected('male', true));
        var arr: Selected[] = [{ "optionText": "female", "optionValue": false }, { "optionText": "male", "optionValue": true }];
        this.SexOptions = ko.observableArray(arr);
        this.SexSelected = ko.observable(false);       
        this.Errors = ko.validation.group(this);     
    }

    deleteItem(index, root, data) {
    let curChild = <Children>data;
    //console.log(data);
    root.children.remove((item: Children) => { return item.ChildName == curChild.ChildName && item.ChildId == curChild.ChildId });
    root.removeItems(curChild);
    root.children.valueHasMutated();
    root.testFunction();
    }

    editImage(index, root, data) {
        let chrChild = <Children>data;
        let urlstring = "/Home/UploadUserImage/" + data.ChildId;
        window.location.href = urlstring;
    }
        
    addItem(root) {
        debugger;
       // var test1 = this.ChildName().toString();
       // console.log(test1);
        if (root.ChildName() != "") {
            var test = root.ChildName().toString();
            var selected = root.SexSelected();
            let nextIndex: number = root.children().length;
            let t1 = new Children();
            t1.ChildId = nextIndex;
            t1.ChildAge = <number>root.ChildAge();
            t1.ChildName = root.ChildName().toString();
            t1.ChildSex = <boolean>selected;
            t1.BankedPoints = 0;
            t1.UserAuthCode = "";
            t1.UserImage = "/Content/usernotset.png";
            t1.ParentUser = root.ParentUser;
           // console.log(t1);
            root.saveItems(t1, root);
           // root.children.push(t1);
            
            root.testFunction();
        }
        else {
            //ko.validatedObservable(this.ChildName);
            this.Errors = ko.validation.group(this.ChildAge, this.ChildName);
        }
    }

    saveItems(data, root) {
        let curChild = <Children>data;
        debugger;
       
        var promise =
            $.ajax({
                url: "/Home/AddChild/",
                cache: false,
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(curChild),
                success: function (result) {
                    
                    let child: Children = <Children>result;
                    console.log(root);
                    //let match: Children = ko.utils.arrayFirst(root.children(), (item: Children) =>  {
                    //    return data.ChildId === item.ChildId;
                    //});
                    
                    //match.ChildId = child.ChildId;
                    //match.CanBank = child.CanBank;
                    //match.ParentUser = child.ParentUser;
                    //match.UserAuthCode = child.UserAuthCode;
                    root.children.push(child);
                    root.children.valueHasMutated();
                   
                },
            });
        promise.done(function (res) {
          
        });
    }

    resFunction(res) {
        var x = res;
        //var y = Json.Encode(res);
        console.log('ok');
    }
    testFunction() {
        console.log('ok');
    }

    removeItems(data) {
        let curChild = <Children>data;

        var promise =
            $.ajax({
                url: "/Home/DeleteChild/",
                cache: false,
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(curChild)
            });
        promise.done(function (res) {

        });
    }

}
