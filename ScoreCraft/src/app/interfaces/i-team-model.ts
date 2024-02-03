import { IMatchModel } from "./i-match-model";
import { IUserModel } from "./i-user-model";

export interface ITeamModel {
    refTeam?: number;
    teamName?: string;
    members?: Array<IUserModel>;
    matches?: Array<IMatchModel>;
}
