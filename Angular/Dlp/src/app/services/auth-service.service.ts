import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthServiceService {
  private loginUrl = 'https://localhost:5000/auth/login'; // Update with your gateway's login URL
  private registerUrl = 'https://localhost:5000/auth/register'; // Update with your gateway's register URL

  constructor(private http: HttpClient) {}

  // Login Service
  login(credentials: { userName: string; password: string }): Observable<any> {
    return this.http.post<{
      result: { token: string };
      issuccess: boolean;
      message: string;
    }>(this.loginUrl, credentials);
  }

  // Registration Service
  register(user: { email: string; name: string; phoneNumber: string; password: string }): Observable<any> {
    return this.http.post<any>(this.registerUrl, user);
  }
}
