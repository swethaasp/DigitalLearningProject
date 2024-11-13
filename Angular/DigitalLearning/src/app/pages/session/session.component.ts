import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SessionService } from '../../services/session.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-session',
  standalone: true,
  imports: [CommonModule, FormsModule],
  providers: [SessionService],
  templateUrl: './session.component.html',
  styleUrls: ['./session.component.css']
})
export class SessionComponent implements OnInit {
  sessionService = inject(SessionService);
  sessionList: any[] = [];
  newSession: any = { date: '', title: '', resources: '', description: '' };
  editMode: boolean = false;
  sessionToEdit: any = null;
  sessionFormModel: any = this.newSession;
  showModal: boolean = false; // Track modal visibility

  ngOnInit(): void {
    this.loadSessions();
  }

  loadSessions(): void {
    this.sessionService.GetAllSessions().subscribe(
      (res: any) => {
        this.sessionList = res;
      },
      (error: any) => {
        console.error('Error loading sessions', error);
      }
    );
  }

  createSession(): void {
    this.sessionService.CreateSession(this.newSession).subscribe(
      () => {
        this.loadSessions();
        this.closeModal(); // Close modal after creating session
        this.newSession = { date: '', title: '', resources: '', description: '' };
      },
      (error: any) => {
        console.error('Error creating session', error);
      }
    );
  }

  editSession(session: any): void {
    this.editMode = true;
    this.sessionToEdit = { ...session }; // Clone to avoid changes in list
    this.sessionFormModel = this.sessionToEdit; // Update form model with the session to edit
    this.openModal(); // Open modal on edit
  }

  updateSession(): void {
    if (!this.sessionToEdit || !this.sessionToEdit.id) return;

    this.sessionService.UpdateSession(this.sessionToEdit.id, this.sessionToEdit).subscribe(
      () => {
        this.loadSessions();
        this.closeModal(); // Close modal after updating session
      },
      (error: any) => {
        console.error('Error updating session', error);
      }
    );
  }

  deleteSession(id: number): void {
    this.sessionService.DeleteSession(id).subscribe(
      () => this.loadSessions(),
      (error: any) => {
        console.error('Error deleting session', error);
      }
    );
  }

  cancelEdit(): void {
    this.editMode = false;
    this.sessionToEdit = null;
    this.sessionFormModel = this.newSession;
  }

  openModal(): void {
    this.showModal = true;
  }

  closeModal(): void {
    this.showModal = false;
    this.cancelEdit(); // Reset form when modal is closed
  }
}
