/// <reference path="typings/knockout/knockout.d.ts" />
/// <reference path="typings/knockout.mapping/knockout.mapping.d.ts" />
/// <reference path="typings/knockout.validation/knockout.validation.d.ts" />
/// <reference path="typings/jquery/jquery.d.ts" />

class Behaviour{     
    BehaviourId: number;
    BehaviourName: string;
    BehaviourPoints: number;      
}

//class Selected {
//    optionText: string;
//    optionValue: boolean;
//}

class BehaviourListModel {
    //values: string[];
    behaviours: KnockoutObservableArray<Behaviour>;
    BehaviourName: KnockoutObservable<string>;
    BehaviourPoints: KnockoutObservable<number>;    
    Errors: KnockoutValidationErrors;

    constructor(values: Behaviour[]) {        
        this.behaviours = ko.observableArray(values);
        this.BehaviourName = ko.observable("").extend({required:true });
        this.BehaviourPoints = ko.observable(0).extend({required:true,number:true});      
        this.Errors = ko.validation.group(this);     
    }

    deleteItem(index, root, data) {
    let curItem = <Behaviour>data;    
    root.behaviours.remove((item: Behaviour) => { return item.BehaviourName == curItem.BehaviourName && item.BehaviourId == curItem.BehaviourId });
    root.removeItems(curItem);   
    }
        
    addItem(root) {      
        if (root.BehaviourName() != "") {  
            let match: Behaviour = ko.utils.arrayFirst(root.behaviours(), (item: Behaviour) => {
                return this.BehaviourName() === item.BehaviourName;
            });
            if (match == null) {

                let nextIndex: number = root.behaviours().length;
                let b1 = new Behaviour();
                b1.BehaviourId = nextIndex;
                b1.BehaviourPoints = <number>root.BehaviourPoints();
                b1.BehaviourName = root.BehaviourName().toString();
                root.behaviours.push(b1);
                root.saveItems(b1, root);
            }
                else{
                    this.BehaviourName.isValid(false);
                }
        }
        else {           
            this.Errors = ko.validation.group(this.BehaviourName, this.BehaviourPoints);
        }
    }

    saveItems(data, root) {
        let curItem = <Behaviour>data;        
       
        var promise =
            $.ajax({
                url: "/Home/AddBehaviour/",
                cache: false,
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(curItem),
                success: function (result) {
                    
                    let behaviour: Behaviour = <Behaviour>result;
                    //console.log(root);
                    let match: Behaviour = ko.utils.arrayFirst(root.behaviours(), (item: Behaviour) =>  {
                        return data.BehaviourId === item.BehaviourId;
                    });
                    
                    match.BehaviourId = curItem.BehaviourId;
                   
                },
            });
        promise.done(function (res) {
          
        });
    }

    removeItems(data) {
        let curItem = <Behaviour>data;

        var promise =
            $.ajax({
                url: "/Home/DeleteBehaviour/",
                cache: false,
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(curItem)
            });
        promise.done(function (res) {

        });
    }

}
