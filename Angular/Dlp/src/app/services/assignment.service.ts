import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AssignmentService {
  updateAssignment(assignmentid: number, assignmentdata: any): Observable<any> {

    const url = `https://localhost:5000/assignment/${assignmentid}`;
  
    //https://localhost:5005/api/Session/7
  
    return this.http.put(url, assignmentdata);
  
   }

 
  private apiUrl = 'https://localhost:5000/assignment'; 

  constructor(private http: HttpClient) {}

  getAssignments(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl); 
  }
  createAssignment(payload:any){
    const url='https://localhost:5000/assignment';
    return this.http.post(url,payload)
  }


  submit(data:any,id:number):Observable<any>{
    const url=`https://localhost:5000/assignment/Submit/${id}`;
    console.log(url);
    console.log(data);
    return this.http.put(url,data);
  }


  deleteAssignment(id: number): Observable<any> {

    const url = `https://localhost:5000/assignment/${id}`;
   
    return this.http.delete(url);
   

  
}}
