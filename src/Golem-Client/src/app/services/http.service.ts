import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { ProjectModel } from '../models/projectModel';
import { HttpParams, HttpClient, HttpHeaders } from '@angular/common/http';
import { GetInTouch } from '../models/getInTouchModel';
import { DashboardOverview } from '../models/dashboardOverview';

@Injectable({
  providedIn: 'root',
})
export class HttpService {
  private readonly apiUrl: string = environment.apiUrl;

  constructor(private http: HttpClient) {}

  public searchProject(searchTerm: string): Observable<ProjectModel[]> {
    const params = new HttpParams().append('searchTerm', searchTerm);

    return this.http.get<any>(`${this.apiUrl}elasticsearch`, {
      params,
      withCredentials: true,
    });
  }

  public sendEmail(loginViewModel: GetInTouch): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}emails`, loginViewModel, {
      withCredentials: true,
    });
  }

  public getUsers(skip: number, take: number): Observable<any> {
    const params = new HttpParams()
      .append('skip', skip.toString())
      .append('take', take.toString());

    return this.http.get<any>(`${this.apiUrl}analytics/users`, {
      params,
      withCredentials: true,
    });
  }

  public getQueries(
    userId: string,
    skip: number,
    take: number
  ): Observable<any> {
    const params = new HttpParams()
      .append('skip', skip.toString())
      .append('take', take.toString());

    return this.http.get<any>(
      `${this.apiUrl}analytics/users/${userId}/queries`,
      { params, withCredentials: true }
    );
  }

  public getSessions(
    userId: string,
    skip: number,
    take: number
  ): Observable<any> {
    const params = new HttpParams()
      .append('skip', skip.toString())
      .append('take', take.toString());

    return this.http.get<any>(
      `${this.apiUrl}analytics/users/${userId}/sessions`,
      { params, withCredentials: true }
    );
  }

  public getDashboardOverview(): Observable<DashboardOverview> {
    return this.http.get<any>(`${this.apiUrl}analytics/dashboard-overview`, {
      withCredentials: true,
    });
  }
}
