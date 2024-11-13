// File: session.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SessionService {
  private baseUrl = "https://localhost:7149/api/Session";

  constructor(private http: HttpClient) {}

  GetAllSessions(): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl);
  }

  CreateSession(session: any): Observable<any> {
    return this.http.post<any>(this.baseUrl, session);
  }

  UpdateSession(id: number, session: any): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/${id}`, session);
  }

  DeleteSession(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}
