import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NavbarService {

  private apiUrl = 'https://localhost:5000/streak/ById'; 

  constructor(private http: HttpClient) {}

  getstreak(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl); 
  }
}
