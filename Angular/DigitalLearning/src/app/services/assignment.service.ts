import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AssignmentService {
  private apiUrl = 'https://localhost:7159/api/Assignment';  // Replace with your actual API URL

  constructor(private http: HttpClient) {}

  // Get all assignments
  getAllAssignments(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}`);
  }

  // Create a new assignment
  createAssignment(assignment: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}`, assignment);
  }

  // Update an existing assignment (expects both id and assignment data)
  updateAssignment(id: number, assignment: any): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/${id}`, assignment);
  }

  // Delete an assignment
  deleteAssignment(id: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/${id}`);
  }
}
