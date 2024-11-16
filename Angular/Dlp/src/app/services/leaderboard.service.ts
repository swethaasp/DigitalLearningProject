import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LeaderboardService {

  private apiUrl = 'https://localhost:5000/leaderboard'; 

  constructor(private http: HttpClient) {}

  getLeaderboard(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl); 
  }
}
