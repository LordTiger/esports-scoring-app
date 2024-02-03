import { IMatchModel } from "./i-match-model";
import { ITeamModel } from "./i-team-model";

export interface IUserModel {
    refUser?: string;
    refTeam?: number;
    isTeamCaptain?: boolean;
    name?: string;
    surname?: string;
    email?: string;
    team?: ITeamModel;
    matches: Array<IMatchModel>;
}
