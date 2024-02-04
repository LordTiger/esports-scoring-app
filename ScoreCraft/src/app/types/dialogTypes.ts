import { ITeamModel } from "../interfaces/i-team-model";
import { IUserModel } from "../interfaces/i-user-model";

export type userDialogType = {
    title: string;
    isEdit: boolean;
    data: IUserModel;
};

export type teamDialogType = {
    title: string;
    isEdit: boolean;
    data: ITeamModel;
};