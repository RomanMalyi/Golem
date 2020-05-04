import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { ProjectModel } from '../models/projectModel';
import { HttpParams, HttpClient } from '@angular/common/http';
import { GetInTouch } from '../models/getInTouchModel';
import { UserModel } from '../models/userModel';
import { QueryModel } from '../models/queryModel';

@Injectable({
  providedIn: 'root',
})
export class HttpService {
  private readonly apiUrl: string = environment.apiUrl;

  constructor(private http: HttpClient) {}

  public searchProject(searchTerm: string): Observable<ProjectModel[]> {
    const params = new HttpParams().append('searchTerm', searchTerm);

    return this.http.get<any>(`${this.apiUrl}elasticsearch`, { params });
  }

  public sendEmail(loginViewModel: GetInTouch): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}emails`, loginViewModel);
  }

  public getUsers(skip: number, take: number): Observable<UserModel[]> {
    const params = new HttpParams()
      .append('skip', skip.toString())
      .append('take', take.toString());

    return this.http.get<any>(`${this.apiUrl}analytics/users`, { params });
  }

  public getQueries(
    userId: string,
    skip: number,
    take: number
  ): Observable<QueryModel[]> {
    const params = new HttpParams()
      .append('skip', skip.toString())
      .append('take', take.toString());

    return this.http.get<any>(
      `${this.apiUrl}analytics/users/${userId}/queries`,
      { params }
    );
  }
}
