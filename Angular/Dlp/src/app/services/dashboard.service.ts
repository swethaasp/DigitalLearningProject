import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {
  loginUrl:string="https://quotes-api-self.vercel.app/quote"

  constructor(private http:HttpClient) { }

  getquote(): Observable<any> {
    return this.http.get<any>(this.loginUrl);
  }
}
