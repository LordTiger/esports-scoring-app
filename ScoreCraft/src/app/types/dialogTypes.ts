import { IUserModel } from "../interfaces/i-user-model";

export type userDialogType = {
    title: string;
    isEdit: boolean;
    data: IUserModel;
};