import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Project } from '../models/project';

@Injectable({
  providedIn: 'root',
})
export class ElasticsearchService {
  private readonly apiUrl: string = environment.apiUrl;

  constructor(private http: HttpClient) {}

  public search(searchTerm: string): Observable<Project[]> {
    const params = new HttpParams().append('searchTerm', searchTerm);

    return this.http.get<any>(`${this.apiUrl}elasticsearch`, { params });
  }
}
