import { IMatchModel } from "./i-match-model";
import { ITeamModel } from "./i-team-model";
import { IUserTeamModel } from "./i-user-team-model";

export interface IUserModel {
    refUser?: string;
    isTeamCaptain?: boolean;
    name?: string;
    surname?: string;
    email?: string;
    teamNames?: string;
    refTeams?: Array<number>;
    matches?: Array<IMatchModel>;
    userTeams?: Array<IUserTeamModel>;
}
