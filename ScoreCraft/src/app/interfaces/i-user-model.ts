import { IMatchModel } from "./i-match-model";
import { ITeamModel } from "./i-team-model";
import { IUserTeamModel } from "./iuser-team-model";

export interface IUserModel {
    refUser?: string;
    isTeamCaptain?: boolean;
    name?: string;
    surname?: string;
    email?: string;
    userAssignedTeams?: string;
    matches?: Array<IMatchModel>;
    userTeams?: Array<IUserTeamModel>;
}
