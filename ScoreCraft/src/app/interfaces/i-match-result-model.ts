import { IMatchModel } from "./i-match-model";

export interface IMatchResultModel {
    refMatchResult?: number;
    refMatch?: number;
    match?: IMatchModel;
    homeResult?: number;
    guestResult?: number;
}
