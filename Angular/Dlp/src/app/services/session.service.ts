import { Injectable } from '@angular/core';

import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

@Injectable({

 providedIn: 'root',

})

export class SessionService {

 private apiUrl = 'https://localhost:5000/sessions/user'; 

 constructor(private http: HttpClient) {}

 // Get all sessions

 getSessions(): Observable<any[]> {

  return this.http.get<any[]>(this.apiUrl);

 }

createSession(payload:any){
  const url='https://localhost:5000/sessions';
  return this.http.post(url,payload)
}

 updateSession(sessionId: number, sessionData: any): Observable<any> {

  const url = `https://localhost:5000/sessions/${sessionId}`;

  //https://localhost:5005/api/Session/7

  return this.http.put(url, sessionData);

 }



 // Delete session

deleteSession(id: number): Observable<any> {

 const url = `https://localhost:5000/sessions/${id}`;

 return this.http.delete(url);

}

}