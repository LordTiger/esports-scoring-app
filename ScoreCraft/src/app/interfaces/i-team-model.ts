import { IMatchModel } from "./i-match-model";
import { IUserModel } from "./i-user-model";
import { IUserTeamModel } from "./iuser-team-model";

export interface ITeamModel {
    refTeam?: number;
    teamName?: string;
    members?: Array<IUserModel>;
    matches?: Array<IMatchModel>;
    userTeams?: Array<IUserTeamModel>;
}
