import { Component, OnInit } from '@angular/core';



import { CommonModule } from '@angular/common';

import { FormsModule } from '@angular/forms';
import { NoteService } from '../../services/notes.service';

@Component({
  selector: 'app-notes',

  standalone: true,

  imports: [CommonModule, FormsModule,],

  templateUrl: './notes.component.html',

  styleUrls: ['./notes.component.css'],
})
export class NotesComponent implements OnInit {
  noteList: any[] = [];

  isDescriptionPopupOpen = false; // Track description popup state

  isUpdatePopupOpen = false; // To track the update popup state

  selectedNote: any = null;

  selectedDescription: string = ''; // For the description popup

  isCreatePopup: boolean = false;

  note: any = {
    id: 0,

    dateCreated: new Date().toISOString(),

    dateModified: new Date().toISOString(),

    title: '',

    description: '',

    resources: '',

    userId: '',
  };

  constructor(private noteService: NoteService) {}

  ngOnInit() {
    this.getNotes();
  }

  // Fetch notes from backend

  getNotes() {
    this.noteService.getNotesByUser().subscribe((notes: any[]) => {
      this.noteList = notes;
    });
  }

  openCreatePopup() {
    this.isCreatePopup = true;
  }

  closeCreatePopup() {
    this.isCreatePopup = false;

    this.clearnote();
  }

  clearnote() {
    this.note = {
      id: 0,

      dateCreated: '',

      dateModified: '',

      title: '',

      description: '',

      resources: '',

      userId: '',
    };
  }

  submitCreate() {
    this.noteService.addNote(this.note).subscribe((note: any) => {
      alert('note created successfully');

      this.closeCreatePopup();

      this.clearnote();

      this.getNotes();
    });
  }

  // Open the popup with the note description

  openDescriptionPopup(note: any) {
    this.selectedNote = note;

    this.selectedDescription = note.description;

    this.isDescriptionPopupOpen = true;
  }

  // Close the description popup

  closeDescriptionPopup() {
    this.isDescriptionPopupOpen = false;

    this.selectedDescription = '';
  }

  // Open the update popup with the selected note

  openUpdatePopup(note: any) {
    this.selectedNote = { ...note }; // Clone the note to avoid mutating the list

    this.isUpdatePopupOpen = true;
  }

  // Close the update popup

  closeUpdatePopup() {
    this.isUpdatePopupOpen = false;

    this.selectedNote = null;
  }

  // Submit the updated note

  submitUpdate() {
    if (this.selectedNote) {
      // Set the current date and time as the modified date

      this.selectedNote.dateModified = new Date(); // Set the modified date to today's date

      this.noteService
        .updateNote(this.selectedNote.id, this.selectedNote)
        .subscribe({
          next: () => {
            const index = this.noteList.findIndex(
              (note) => note.id === this.selectedNote.id
            );

            if (index !== -1) {
              this.noteList[index] = { ...this.selectedNote }; // Update the note in the list
            }

            this.closeUpdatePopup(); // Close the popup
          },

          error: (error) => console.error('Error updating note:', error),
        });
    }
  }

  // Confirm the deletion of a note

  confirmDelete(noteId: number): void {
    if (confirm('Are you sure you want to delete this note?')) {
      this.noteService.deleteNote(noteId).subscribe(
        () => {
          this.noteList = this.noteList.filter((note) => note.id !== noteId);
        },

        (error) => console.error('Error deleting note:', error)
      );
    }
  }
}
