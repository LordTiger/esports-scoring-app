import { IMatchResultModel } from "./i-match-result-model";
import { ITeamModel } from "./i-team-model";

export interface IMatchModel {
    refMatch?: number;
    refHomeTeam?: number;
    refGuestTeam?: number;
    matchDate?: Date;
    refMatchWinner?: number;
    format?: string;
    bestOf?: number;
    matchResults?: Array<IMatchResultModel>;

    homeTeam?: ITeamModel;
    guestTeam?: ITeamModel;
    winningTeam?: ITeamModel;
}
