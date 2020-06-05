import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { LoginUserModel } from '../models/loginUserModel';
import { Observable } from 'rxjs';
import { AuthorizationModel } from '../models/authorizationResponse';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly accessTokenPropertyName = 'access_token';
  private readonly emailPropertyName = 'email';
  private apiUrl: string = environment.apiUrl;

  constructor(private http: HttpClient) { }

  public login(loginModel: LoginUserModel): Observable<AuthorizationModel> {
    return this.http.post<any>(`${this.apiUrl}account/login`, loginModel, {
      withCredentials: true,
    });
  }

  public getToken() {
    return localStorage.getItem(this.accessTokenPropertyName);
  }

  public logout() {
    localStorage.removeItem(this.accessTokenPropertyName);
  }

  public setToken(result: any) {
    localStorage.setItem(this.accessTokenPropertyName, result.accessToken);
  }

  public isAuthenticated() {
    const accessToken = localStorage.getItem(this.accessTokenPropertyName);
    return accessToken !== 'undefined' && accessToken !== undefined && accessToken !== null;
}
}
