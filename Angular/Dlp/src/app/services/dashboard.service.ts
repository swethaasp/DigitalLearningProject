import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {
  loginUrl:string="https://quotes-api-self.vercel.app/quote";
  today = new Date().toISOString().split('T')[0];

  constructor(private http:HttpClient) { }

  getquote(): Observable<any> {
    return this.http.get<any>(this.loginUrl);
  }
  private apiUrl = 'https://localhost:5000/assignment/bydeadline/'+this.today; 

  getAssignment(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl); 
  }
  

  private Url = 'https://localhost:5000/user/ById'; 

  getuser(): Observable<any[]> {
    return this.http.get<any[]>(this.Url); 
  }
}
