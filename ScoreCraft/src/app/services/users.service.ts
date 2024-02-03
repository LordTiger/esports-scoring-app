import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from '../../environments/environment';
import { IUserModel } from '../interfaces/i-user-model';
import { lastValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  private httpClient = inject(HttpClient);
  private readonly apiUrl = `${environment.api}/user`;

  /**
   * Retrieves the collection of users from the server.
   * @returns {Observable<Array<IUserModel>>} An observable that emits an array of IUserModel.
   */
  getCollection() {
    return lastValueFrom(this.httpClient.get<Array<IUserModel>>(`${this.apiUrl}/GetCollection`));
  }

  /**
   * Retrieves a user based on the provided reference user.
   * @param RefUser The reference user to retrieve.
   * @returns A promise that resolves to the retrieved user.
   */
  getUser(RefUser: string) {
    return lastValueFrom(this.httpClient.get<IUserModel>(`${this.apiUrl}/GetUser`, { params: { RefUser: RefUser } }));
  }

  /**
   * Creates a new user.
   * @param user The user object to be created.
   * @returns A promise that resolves to the created user.
   */
  createUser(user: IUserModel) {
    return lastValueFrom(this.httpClient.post<IUserModel>(`${this.apiUrl}/insert`, user));
  }

  /**
   * Updates a user.
   * @param user The user object to be updated.
   * @returns A promise that resolves to the updated user.
   */
  updateUser(user: IUserModel) {
    return lastValueFrom(this.httpClient.put<IUserModel>(`${this.apiUrl}/update`, user));
  }

  /**
   * Deletes a user.
   * @param RefUser - The reference ID of the user to be deleted.
   * @returns A boolean indicating whether the user was successfully deleted.
   */
  deleteUser(RefUser: string) {
    return lastValueFrom(this.httpClient.delete<boolean>(`${this.apiUrl}/delete`, { params: { RefUser: RefUser } }));
  }
}
