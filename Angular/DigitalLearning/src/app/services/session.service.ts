import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, forkJoin } from 'rxjs';
import { map } from 'rxjs/operators';

interface Session {
  id: number;
  date: string;
  title: string;
  resources: string;
  description: string;
  assignmentid: number;
  userid: number;
}

interface Assignment {
  id: number;
  title: string;
}

@Injectable({
  providedIn: 'root',
})
export class SessionService {
  private sessionApiUrl = 'https://localhost:7149/api/sessions';
  private assignmentApiUrl = 'https://localhost:7149/api/assignments'; // Replace with actual API URL for assignments

  constructor(private http: HttpClient) {}

  getSessionsWithAssignments(): Observable<any[]> {
    return this.http.get<Session[]>(this.sessionApiUrl).pipe(
      map((sessions) => {
        const assignmentRequests = sessions.map((session) =>
          this.http.get<Assignment>(`${this.assignmentApiUrl}/${session.assignmentid}`).pipe(
            map((assignment) => {
              return { ...session, assignmentTitle: assignment.title }; // Add the assignment title to the session data
            })
          )
        );

        return forkJoin(assignmentRequests);
      })
    );
  }
}
