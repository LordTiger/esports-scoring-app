import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from '../../environments/environment';
import { ITeamModel } from '../interfaces/i-team-model';
import { lastValueFrom } from 'rxjs';
import { IMatchResultModel } from '../interfaces/i-match-result-model';

@Injectable({
  providedIn: 'root'
})
export class TeamsService {

  private httpClient = inject(HttpClient);
  private readonly apiUrl = `${environment.api}/team`;

  getCollection() {
    return lastValueFrom(this.httpClient.get<Array<ITeamModel>>(`${this.apiUrl}/GetCollection`));
  }

  getTeam(RefTeam: number) {
    return lastValueFrom(this.httpClient.get<Array<ITeamModel>>(`${this.apiUrl}/GetTeam`, { params: { RefTeam: RefTeam } }));
  }

  insert(team: ITeamModel) {
    return lastValueFrom(this.httpClient.post<ITeamModel>(`${this.apiUrl}/Insert`, team));
  }

  update(team: ITeamModel) {
    return lastValueFrom(this.httpClient.put<ITeamModel>(`${this.apiUrl}/Update`, team));
  }

  delete(RefTeam: number) {
    return lastValueFrom(this.httpClient.delete<boolean>(`${this.apiUrl}/Delete`, { params: { RefTeam: RefTeam } }));
  }

}
