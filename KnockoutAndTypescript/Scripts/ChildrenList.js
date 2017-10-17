/// <reference path="typings/knockout/knockout.d.ts" />
/// <reference path="typings/knockout.mapping/knockout.mapping.d.ts" />
/// <reference path="typings/knockout.validation/knockout.validation.d.ts" />
/// <reference path="typings/jquery/jquery.d.ts" />
var Children = (function () {
    function Children() {
    }
    return Children;
}());
var Selected = (function () {
    function Selected() {
    }
    return Selected;
}());
var ChildrenListModel = (function () {
    function ChildrenListModel(values, parentuser) {
        //var test = ko.mapping.fromJS(values);
        //this.items = ko.mapping.fromJS(values);
        this.ParentUser = parentuser;
        this.children = ko.observableArray(values);
        this.ChildName = ko.observable("").extend({ required: true });
        this.ChildAge = ko.observable(0).extend({ required: true });
        this.ChildSex = ko.observable(true);
        this.SexOptions = ko.observableArray([]);
        //Selected test = new Selected("male", true);
        //this.SexOptions.push(new Selected('male', true));
        var arr = [{ "optionText": "female", "optionValue": false }, { "optionText": "male", "optionValue": true }];
        this.SexOptions = ko.observableArray(arr);
        this.SexSelected = ko.observable(false);
        this.Errors = ko.validation.group(this);
    }
    ChildrenListModel.prototype.deleteItem = function (index, root, data) {
        var curChild = data;
        //console.log(data);
        root.children.remove(function (item) { return item.ChildName == curChild.ChildName && item.ChildId == curChild.ChildId; });
        root.removeItems(curChild);
        root.children.valueHasMutated();
        root.testFunction();
    };
    ChildrenListModel.prototype.editImage = function (index, root, data) {
        var chrChild = data;
        var urlstring = "/Home/UploadUserImage/" + data.ChildId;
        window.location.href = urlstring;
    };
    ChildrenListModel.prototype.addItem = function (root) {
        debugger;
        // var test1 = this.ChildName().toString();
        // console.log(test1);
        if (root.ChildName() != "") {
            var test = root.ChildName().toString();
            var selected = root.SexSelected();
            var nextIndex = root.children().length;
            var t1 = new Children();
            t1.ChildId = nextIndex;
            t1.ChildAge = root.ChildAge();
            t1.ChildName = root.ChildName().toString();
            t1.ChildSex = selected;
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
    };
    ChildrenListModel.prototype.saveItems = function (data, root) {
        var curChild = data;
        debugger;
        var promise = $.ajax({
            url: "/Home/AddChild/",
            cache: false,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(curChild),
            success: function (result) {
                var child = result;
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
    };
    ChildrenListModel.prototype.resFunction = function (res) {
        var x = res;
        //var y = Json.Encode(res);
        console.log('ok');
    };
    ChildrenListModel.prototype.testFunction = function () {
        console.log('ok');
    };
    ChildrenListModel.prototype.removeItems = function (data) {
        var curChild = data;
        var promise = $.ajax({
            url: "/Home/DeleteChild/",
            cache: false,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(curChild)
        });
        promise.done(function (res) {
        });
    };
    return ChildrenListModel;
}());
//# sourceMappingURL=ChildrenList.js.map