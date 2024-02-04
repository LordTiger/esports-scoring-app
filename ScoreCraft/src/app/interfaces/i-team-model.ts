import { IMatchModel } from "./i-match-model";
import { IUserModel } from "./i-user-model";
import { IUserTeamModel } from "./i-user-team-model";

export interface ITeamModel {
    refTeam?: number;
    teamName?: string;
    teamSize?: number;
    winningRatio?: number;
    isArchived?: boolean;
    members?: Array<IUserModel>;
    matches?: Array<IMatchModel>;
    userTeams?: Array<IUserTeamModel>;
}
