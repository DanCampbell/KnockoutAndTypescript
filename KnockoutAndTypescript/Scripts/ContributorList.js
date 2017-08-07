/// <reference path="typings/knockout/knockout.d.ts" />
/// <reference path="typings/knockout.mapping/knockout.mapping.d.ts" />
/// <reference path="typings/knockout.validation/knockout.validation.d.ts" />
/// <reference path="typings/jquery/jquery.d.ts" />
var Contributor = (function () {
    function Contributor() {
    }
    return Contributor;
}());
// class Selected {
//    optionText: string;
//    optionValue: boolean;
//}
var ContributorListModel = (function () {
    function ContributorListModel(values, parent) {
        //var test = ko.mapping.fromJS(values);
        //this.items = ko.mapping.fromJS(values);
        this.contributors = ko.observableArray(values);
        this.ContributorName = ko.observable("").extend({ required: true });
        this.Parent = parent;
        this.Errors = ko.validation.group(this);
    }
    ContributorListModel.prototype.deleteItem = function (index, root, data) {
        var curContributor = data;
        root.contributors.remove(function (item) { return item.ContributorName == curContributor.ContributorName && item.ContributorId == curContributor.ContributorId; });
        root.removeItems(curContributor);
        root.contributors.valueHasMutated();
        root.testFunction();
    };
    ContributorListModel.prototype.editImage = function (index, root, data) {
        //  let chrChild = <Contributor>data;
        var urlstring = "/Home/UploadContributorImage/" + data.ContributorId;
        window.location.href = urlstring;
    };
    ContributorListModel.prototype.addItem = function (root) {
        debugger;
        if (root.ContributorName() != "") {
            var test = root.ContributorName().toString();
            //var selected = root.SexSelected();
            var nextIndex = root.contributors().length;
            var t1 = new Contributor();
            t1.ContributorId = nextIndex;
            t1.ContributorName = root.ContributorName().toString();
            t1.ContributorImage = "/Content/usernotset.png";
            t1.ParentUser = root.Parent;
            t1.ContributorAuthCode = 'FFFF';
            root.contributors.push(t1);
            root.saveItems(t1, root);
            root.testFunction();
        }
        else {
            //ko.validatedObservable(this.ChildName);
            this.Errors = ko.validation.group(this.ContributorName);
        }
    };
    ContributorListModel.prototype.saveItems = function (data, root) {
        var curContributor = data;
        var promise = $.ajax({
            url: "/Home/AddContributor/",
            cache: false,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(curContributor),
            success: function (result) {
                var contributor = result;
                console.log(root);
                var match = ko.utils.arrayFirst(root.contributors(), function (item) {
                    return data.ContributorId === item.ContributorId;
                });
                match.ContributorId = contributor.ContributorId;
                match.ParentUser = contributor.ParentUser;
                match.ContributorAuthCode = contributor.ContributorAuthCode;
                root.contributors.valueHasMutated();
            },
        });
        promise.done(function (res) {
        });
    };
    ContributorListModel.prototype.resFunction = function (res) {
        var x = res;
        //var y = Json.Encode(res);
        console.log('ok');
    };
    ContributorListModel.prototype.testFunction = function () {
        console.log('ok');
    };
    ContributorListModel.prototype.removeItems = function (data) {
        var curContributor = data;
        var promise = $.ajax({
            url: "/Home/DeleteContributor/",
            cache: false,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(curContributor)
        });
        promise.done(function (res) {
        });
    };
    return ContributorListModel;
}());
//# sourceMappingURL=ContributorList.js.map