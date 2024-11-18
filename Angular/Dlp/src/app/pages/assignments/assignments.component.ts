import { Component } from '@angular/core';
import { AssignmentService } from '../../services/assignment.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { jwtDecode } from 'jwt-decode';

@Component({
  selector: 'app-assignments',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './assignments.component.html',
  styleUrl: './assignments.component.css',
})
export class AssignmentsComponent {
  assignment: any[] = [];
  userRole: string | null = null;
  isCreatePopupOpen: boolean = false;
  selsectedassignment: any;
  assignmentmodel = {
    id: 0,
    dateAssigned: new Date().toISOString(),
    deadline: '',
    status: '',
    title: '',
    description: '',
    userId: '',
  };
  isDescriptionPopupOpen: boolean = false;
  isUpdatePopupOpen: boolean = false;
  constructor(private AssignmentService: AssignmentService) {}

  ngOnInit(): void {
    this.fetchassignment();

    const token = localStorage.getItem('token');
    if (token) {
      try {
        const decodedToken: any = jwtDecode(token);
        this.userRole = decodedToken.RoleS || null; // Replace "role" with the correct key from your token assignmentmodel
      } catch (error) {
        console.error('Error decoding token:', error);
        this.userRole = null;
      }
    }
  }

  submitCreateSession() {
    if (
      this.assignmentmodel.title != '' &&
      this.assignmentmodel.deadline != '' &&
      this.assignmentmodel.description != ''
    ) {
      this.AssignmentService.createAssignment(this.assignmentmodel).subscribe(
        (response) => {
          this.resetCreateForm();
          this.fetchassignment();
          console.log(response);
        }
      );
      // Close the popup and reset the form
      console.log(
        this.assignmentmodel.dateAssigned,
        this.assignmentmodel.title
      );
      this.closeCreatePopup();
    } else {
      // Show an error or validation message
      alert('Please fill in all fields.');
    }
  }
  



  openDescriptionPopup(asnmt: any) {
    this.assignmentmodel = asnmt;
    this.isDescriptionPopupOpen = true;
  }
  
  // Close the description popup
  closeDescriptionPopup() {
    this.isDescriptionPopupOpen = false;
  }

  



  resetCreateForm() {
    this.assignmentmodel = {
      id: 0,
      dateAssigned: new Date().toISOString(),
      deadline: '',
      status: '',
      title: '',
      description: '',
      userId: '',
    };
  }

  submitUpdate() {
    this.AssignmentService.updateAssignment(
      this.selsectedassignment.id,
      this.selsectedassignment
    ).subscribe({
      next: (response) => {
        console.log('Session updated successfully:', response);

        // Manually update the session in the sessionList

        const index = this.assignment.findIndex(
          (assignment) => assignment.id === this.selsectedassignment.id
        );

        if (index !== -1) {
          this.assignment[index] = { ...this.selsectedassignment }; // Update the session in the list
        }

        this.closeUpdatePopup(); // Close the popup
      },

      error: (error) => {
        console.error('Error updating session:', error);
      },
    });
  }

  openCreatePopup(): void {
    this.isCreatePopupOpen = true;
  }
  
  openUpdatePopup(assignment: any): void {
    this.selsectedassignment = { ...assignment }; // Clone session to avoid mutating the list
    this.isUpdatePopupOpen = true;
  }

  closeCreatePopup() {
    this.isCreatePopupOpen = false;
  }
  closeUpdatePopup(): void {
    this.isUpdatePopupOpen = false;
    this.selsectedassignment = null;
  }


  fetchassignment(): void {
    this.AssignmentService.getAssignments().subscribe(
      (data) => {
        this.assignment = data;
      },
      (error) => {
        console.error('Error fetching Assidnment:', error);
      }
    );
  }

  

  confirmDelete(sessionId: number): void {
    if (confirm('Are you sure you want to delete this session?')) {
      this.AssignmentService.deleteAssignment(sessionId).subscribe(
        () => {
          // Remove the deleted session from the sessionList

          this.assignment = this.assignment.filter(
            (assignment) => assignment.id !== sessionId
          );
        },

        (error) => console.error('Error deleting session:', error)
      );
    }
  }
}
