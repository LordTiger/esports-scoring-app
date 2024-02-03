import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from '../../environments/environment';
import { IUserModel } from '../interfaces/i-user-model';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  private readonly httpClient = inject(HttpClient);
  private readonly apiUrl = environment.api;

  getCollection() {
    return this.httpClient.get<Array<IUserModel>>(`${this.apiUrl}/GetUserCollection`);
  }

  getUser(RefUser: string) {
    return this.httpClient.get<IUserModel>(`${this.apiUrl}/GetUser`, { params: { RefUser: RefUser } });
  }

  createUser(user: IUserModel) {
    return this.httpClient.post<IUserModel>(`${this.apiUrl}/insert`, user);
  }

  updateUser(user: IUserModel) {
    return this.httpClient.put<IUserModel>(`${this.apiUrl}/update`, user);
  }

  deleteUser(RefUser: string) {
    return this.httpClient.delete<boolean>(`${this.apiUrl}/delete`, { params: { RefUser: RefUser } });
  }
}
