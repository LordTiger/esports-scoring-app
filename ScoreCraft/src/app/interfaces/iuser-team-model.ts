import { ITeamModel } from "./i-team-model";
import { IUserModel } from "./i-user-model";

export interface IUserTeamModel {
    refUser?: string;
    refTeam?: number; 

    user?: IUserModel;
    team : ITeamModel;
}
