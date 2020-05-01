import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Project } from '../models/project';
import { HttpParams, HttpClient } from '@angular/common/http';
import { GetInTouch } from '../models/getInTouchModel';

@Injectable({
  providedIn: 'root'
})
export class HttpService {
  private readonly apiUrl: string = environment.apiUrl;

  constructor(private http: HttpClient) { }

  public searchProject(searchTerm: string): Observable<Project[]> {
    const params = new HttpParams().append('searchTerm', searchTerm);

    return this.http.get<any>(`${this.apiUrl}elasticsearch`, { params });
  }

  public sendEmail(loginViewModel: GetInTouch): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}emails`, loginViewModel);
  }
}
