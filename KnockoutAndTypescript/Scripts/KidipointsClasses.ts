
     export class Child {

        ChildId: number;
        ChildName: string;
        ChildAge: number;
        ChildSex: boolean;
        BankedPoints: number;
        UserImage: string;
        ParentUser: string;
        UserAuthCode: string;
        CanBank: boolean;
        constructor(data) {
            this.BankedPoints = data.BankedPoints;
            this.CanBank = data.CanBank;
            this.ChildAge = data.ChildAge;
            this.ChildId = data.ChildId;
            this.ChildName = data.ChildName;
            this.ParentUser = data.ParentUser;
            this.UserAuthCode = data.UserAuthCode;
            this.UserImage = data.UserImage;
        };
    }


   

