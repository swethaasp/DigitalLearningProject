import { Injectable } from '@angular/core';

import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

@Injectable({

 providedIn: 'root'

})

export class NoteService {

 addNote(note: any) {

  return this.http.post<any>('https://localhost:5000/notes',note);

 }

 private apiUrl = 'https://localhost:5000/notes'; // Base URL for notes API

 constructor(private http: HttpClient) {}

 getNotesByUser(): Observable<any[]> {

  return this.http.get<any[]>(`${this.apiUrl}/byuser`);

 }

 // Update note

 updateNote(noteId: number, noteData: any): Observable<any> {

  return this.http.put<any>(`${this.apiUrl}/${noteId}`, noteData);

 }

 // Delete note

 deleteNote(noteId: number): Observable<any> {

  return this.http.delete<any>(`${this.apiUrl}/${noteId}`);

 }

}

// // Create a new note

// createNote(note: any): Observable<any> {

// return this.http.post<any>(this.apiUrl, note);

// }



