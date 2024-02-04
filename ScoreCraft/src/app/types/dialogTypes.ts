import { IMatchModel } from "../interfaces/i-match-model";
import { IMatchResultModel } from "../interfaces/i-match-result-model";
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

export type matchDialogType = {
    title: string;
    isEdit: boolean;
    data: IMatchModel;
};

export type matchResultDialogType = {
    title: string;
    data: IMatchResultModel;
};