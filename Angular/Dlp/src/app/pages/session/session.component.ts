import { Component, OnInit } from '@angular/core';

import { CommonModule } from '@angular/common';

import { FormsModule } from '@angular/forms';

import { Router } from '@angular/router';
import { SessionService } from '../../services/session.service';

@Component({
  selector: 'app-session',

  standalone: true,

  imports: [CommonModule, FormsModule],

  templateUrl: './session.component.html',

  styleUrls: ['./session.component.css'],
})
export class SessionComponent implements OnInit {
  sessionList: any[] = [];

  isUpdatePopupOpen: boolean = false;
  isCreatePopupOpen: boolean = false;
  isDescriptionPopupOpen: boolean = false;

  selectedSession: any = null;
  // Flags for managing popups

  payload = {
    id: 0,
    date: new Date().toISOString(),
    title: '',
    description: '',
    resources: '',
    assignmentId: 0,
    userId: '',
  };

  selectedSessionDescription: any = null;
  isModalOpen = false;

  constructor(private sessionService: SessionService, private router: Router) {}

  ngOnInit(): void {
    this.fetchSessions();
  }

  fetchSessions(): void {
    this.sessionService.getSessions().subscribe(
      (data) => (this.sessionList = data),

      (error) => console.error('Error fetching sessions:', error)
    );
  }
  resetCreateForm() {
    this.payload = {
      title: '',
      date: '',
      resources: '',
      description: '',
      assignmentId: 0,
      id: 0,
      userId: '',
    };
  }

  submitCreateSession() {
    if (
      this.payload.title != '' &&
      this.payload.date != '' &&
      this.payload.resources != ''
    ) {
      this.sessionService.createSession(this.payload).subscribe((response) => {
        this.resetCreateForm();
        this.fetchSessions();
        console.log(response);
      });
      // Close the popup and reset the form
      console.log(this.payload.date, this.payload.title);
      this.closeCreatePopup();
    } else {
      // Show an error or validation message
      alert('Please fill in all fields.');
    }
  }
  openCreatePopup(): void {
    // this.selectedSession = { ...session }; // Clone session to avoid mutating the list

    this.isCreatePopupOpen = true;
  }

  openUpdatePopup(session: any): void {
    this.selectedSession = { ...session }; // Clone session to avoid mutating the list

    this.isUpdatePopupOpen = true;
  }

  closeUpdatePopup(): void {
    this.isUpdatePopupOpen = false;

    this.selectedSession = null;
  }

  toggleModal() {
    this.isModalOpen = !this.isModalOpen;
  }

  onSubmit(formData: any) {
    console.log('New Member Data:', formData);
    this.toggleModal(); // Close the modal after submitting
  }

  // Close the create session popup
  closeCreatePopup() {
    this.isCreatePopupOpen = false;
    this.resetCreateForm();
  }

  openDescriptionPopup(description: any) {
    this.selectedSessionDescription = description;
    this.isDescriptionPopupOpen = true;
  }

  // Close the description popup
  closeDescriptionPopup() {
    this.isDescriptionPopupOpen = false;
  }

  submitUpdate() {
    this.sessionService
      .updateSession(this.selectedSession.id, this.selectedSession)
      .subscribe({
        next: (response) => {
          console.log('Session updated successfully:', response);

          // Manually update the session in the sessionList

          const index = this.sessionList.findIndex(
            (session) => session.id === this.selectedSession.id
          );

          if (index !== -1) {
            this.sessionList[index] = { ...this.selectedSession }; // Update the session in the list
          }

          this.closeUpdatePopup(); // Close the popup
        },

        error: (error) => {
          console.error('Error updating session:', error);
        },
      });
  }

  // confirmDelete(sessionId: number): void {

  // if (confirm('Are you sure you want to delete this session?')) {

  // this.sessionService.deleteSession(sessionId).subscribe(

  // () => this.fetchSessions(),

  // (error) => console.error('Error deleting session:', error)

  // );

  // }

  // }

  // }

  confirmDelete(sessionId: number): void {
    if (confirm('Are you sure you want to delete this session?')) {
      this.sessionService.deleteSession(sessionId).subscribe(
        () => {
          // Remove the deleted session from the sessionList

          this.sessionList = this.sessionList.filter(
            (session) => session.id !== sessionId
          );
        },

        (error) => console.error('Error deleting session:', error)
      );
    }
  }
}
