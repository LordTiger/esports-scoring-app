import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from '../../environments/environment';
import { ITeamModel } from '../interfaces/i-team-model';
import { lastValueFrom } from 'rxjs';
import { IMatchResultModel } from '../interfaces/i-match-result-model';
import { IUserTeamModel } from '../interfaces/i-user-team-model';
import { IUserModel } from '../interfaces/i-user-model';

@Injectable({
  providedIn: 'root'
})
export class TeamsService {

  private httpClient = inject(HttpClient);
  private readonly apiUrl = `${environment.api}/team`;

  /**
   * Retrieves the collection of teams from the server.
   * @returns {Observable<Array<ITeamModel>>} The collection of teams.
   */
  getCollection() {
    return lastValueFrom(this.httpClient.get<Array<ITeamModel>>(`${this.apiUrl}/GetCollection`));
  }

  /**
 * Retrieves the collection of teams from the server.
 * @returns {Observable<Array<ITeamModel>>} The collection of teams.
 */
  getTeamsForLookup() {
    return lastValueFrom(this.httpClient.get<Array<ITeamModel>>(`${this.apiUrl}/getTeamsForLookup`));
  }

  /**
   * Retrieves a team based on the provided reference team number.
   * @param RefTeam The reference team number.
   * @returns A promise that resolves to an array of ITeamModel.
   */
  getTeam(RefTeam: number) {
    return lastValueFrom(this.httpClient.get<Array<ITeamModel>>(`${this.apiUrl}/GetTeam`, { params: { RefTeam: RefTeam } }));
  }

  /**
   * Inserts a team into the database.
   * @param team The team to be inserted.
   * @returns A promise that resolves to the inserted team.
   */
  insert(team: ITeamModel) {
    return lastValueFrom(this.httpClient.post<ITeamModel>(`${this.apiUrl}/Insert`, team));
  }

  /**
   * Adds a user to a team.
   * @param model The user team model.
   * @returns A promise that resolves to the added user model.
   */
  addUserToTeam(model: IUserTeamModel) {
    return lastValueFrom(this.httpClient.post<IUserModel>(`${this.apiUrl}/AddUserToTeam`, model));
  }

  /**
   * Updates a team.
   * @param team The team to be updated.
   * @returns A promise that resolves to the updated team.
   */
  update(team: ITeamModel) {
    return lastValueFrom(this.httpClient.put<ITeamModel>(`${this.apiUrl}/Update`, team));
  }

  /**
   * Deletes a team.
   * @param RefTeam - The reference number of the team to delete.
   * @returns A boolean indicating whether the deletion was successful.
   */
  delete(RefTeam: number) {
    return lastValueFrom(this.httpClient.delete<boolean>(`${this.apiUrl}/Delete`, { params: { RefTeam: RefTeam } }));
  }

  /**
   * Removes a user from a team.
   * @param {string} RefUser - The reference ID of the user.
   * @param {number} RefTeam - The reference ID of the team.
   * @returns {Observable<boolean>} - An Observable that emits a boolean indicating whether the user was successfully removed from the team.
   */
  removeUserFromTeam(RefUser: string ,RefTeam: number) {
    return lastValueFrom(this.httpClient.delete<boolean>(`${this.apiUrl}/RemoveUserFromTeam`, { params: { RefUser: RefUser, RefTeam: RefTeam } }));
  }

}
