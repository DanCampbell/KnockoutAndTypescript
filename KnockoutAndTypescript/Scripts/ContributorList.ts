/// <reference path="typings/knockout/knockout.d.ts" />
/// <reference path="typings/knockout.mapping/knockout.mapping.d.ts" />
/// <reference path="typings/knockout.validation/knockout.validation.d.ts" />
/// <reference path="typings/jquery/jquery.d.ts" />

 class Contributor{
    
    ContributorId: number;
    ContributorName: string;
    ContributorImage: string;
    ParentUser: string;
    ContributorAuthCode: string;
    
}

// class Selected {
//    optionText: string;
//    optionValue: boolean;
//}

class ContributorListModel {
    
    contributors: KnockoutObservableArray<Contributor>;
    ContributorName: KnockoutObservable<string>;    
    ContributorImage: KnockoutObservable<string>;
    Parent: string;
   
    Errors: KnockoutValidationErrors;
   
    constructor(values: Contributor[], parent: string) {
        //var test = ko.mapping.fromJS(values);
        //this.items = ko.mapping.fromJS(values);
        this.contributors = ko.observableArray(values);
        this.ContributorName = ko.observable("").extend({ required: true });
        this.Parent = parent;
         
        this.Errors = ko.validation.group(this);     
    }

    deleteItem(index, root, data) {
    let curContributor = <Contributor>data;
    
    root.contributors.remove((item: Contributor) => { return item.ContributorName == curContributor.ContributorName && item.ContributorId == curContributor.ContributorId });
    root.removeItems(curContributor);
    root.contributors.valueHasMutated();
    root.testFunction();
    }

    editImage(index, root, data) {
      //  let chrChild = <Contributor>data;
        let urlstring = "/Home/UploadContributorImage/" + data.ContributorId;
        window.location.href = urlstring;
    }
        
    addItem(root) {
        debugger;
        if (root.ContributorName() != "") {
            var test = root.ContributorName().toString();
            //var selected = root.SexSelected();
            let nextIndex: number = root.contributors().length;
            let t1 = new Contributor();
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
            this.Errors = ko.validation.group( this.ContributorName);
        }
    }

    saveItems(data, root) {
        let curContributor = <Contributor>data;
        
       
        var promise =
            $.ajax({
                url: "/Home/AddContributor/",
                cache: false,
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(curContributor),
                success: function (result) {
                    
                    let contributor: Contributor = <Contributor>result;
                    console.log(root);
                    let match: Contributor = ko.utils.arrayFirst(root.contributors(), (item: Contributor) =>  {
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
        let curContributor = <Contributor>data;

        var promise =
            $.ajax({
                url: "/Home/DeleteContributor/",
                cache: false,
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(curContributor)
            });
        promise.done(function (res) {

        });
    }

}
