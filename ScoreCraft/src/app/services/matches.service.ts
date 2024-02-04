import { Injectable, inject } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { IMatchModel } from '../interfaces/i-match-model';
import { lastValueFrom } from 'rxjs';
import { IMatchResultModel } from '../interfaces/i-match-result-model';

@Injectable({
  providedIn: 'root'
})
export class MatchesService {

  private httpClient = inject(HttpClient);
  private readonly apiUrl = `${environment.api}/match`;

  getCollection() {
    return lastValueFrom(this.httpClient.get<Array<IMatchModel>>(`${this.apiUrl}/GetCollection`));
  }

  getMatch(RefMatch: number) {
    return lastValueFrom(this.httpClient.get<IMatchModel>(`${this.apiUrl}/GetMatch`, { params: { RefMatch: RefMatch } }));
  }

  getMatchResult(RefMatch: number) {
    return lastValueFrom(this.httpClient.get<IMatchResultModel>(`${this.apiUrl}/GetMatchResult`, { params: { RefMatch: RefMatch } }));
  }


  insert(model: IMatchModel) {
    return lastValueFrom(this.httpClient.post<IMatchModel>(`${this.apiUrl}/Insert`, model));
  }

  update(model: IMatchModel) {
    return lastValueFrom(this.httpClient.put<IMatchModel>(`${this.apiUrl}/Update`, model));
  }

  delete(RefMatch: number) {
    return lastValueFrom(this.httpClient.delete<boolean>(`${this.apiUrl}/Delete`, { params: { RefMatch: RefMatch } }));
  }
}
