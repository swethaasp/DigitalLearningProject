import { AssignmentService } from '../../services/assignment.service';  // Ensure correct import path
import { CommonModule } from '@angular/common';  // Import for ngFor
import { Observable } from 'rxjs';
import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-assignment',
  templateUrl: './assignment.component.html',
  styleUrls: ['./assignment.component.css'],
  standalone: true,
  imports: [CommonModule,FormsModule],  // Import CommonModule for *ngFor to work
  providers: [AssignmentService]  // Add AssignmentService provider
})
export class AssignmentComponent {
  assignmentList: any[] = [];
  newAssignment: any = {
    "id": 0,
    "dateAssigned": "",
    "deadline": "",
    "status": "",
    "title": "",
    "description": "",
    "userId": 0
  };
  
  // Injecting the service
  assignmentService = inject(AssignmentService);

  constructor() {
    this.getAssignments();  
  }

  
  getAssignments(): void {
    this.assignmentService.getAllAssignments().subscribe(
      (data: any[]) => { // Define the data type here
        this.assignmentList = data;
      },
      (error: any) => { // Handle error
        console.error('Error fetching assignments', error);
      }
    );
  }

  // Create a new assignment
  createAssignment(): void {
    this.assignmentService.createAssignment(this.newAssignment).subscribe(
      (data: any) => { // Define the data type here
        this.assignmentList.push(data);  // Add the new assignment to the list
        this.resetForm();  // Reset form
      },
      (error: any) => { // Handle error
        console.error('Error creating assignment', error);
      }
    );
  }

  // Edit an assignment
  editAssignment(assignment: any): void {
    this.newAssignment = { ...assignment };  // Set the form with selected assignment data
  }

  // Update an existing assignment (pass both id and assignment data)
  updateAssignment(): void {
    const id = this.newAssignment.id;  // Extract the id from the newAssignment
    this.assignmentService.updateAssignment(id, this.newAssignment).subscribe(
      (data: any) => {  // Define the data type here
        const index = this.assignmentList.findIndex(item => item.id === data.id);
        if (index !== -1) {
          this.assignmentList[index] = data;  // Update the assignment in the list
        }
        this.resetForm();  // Reset form after update
      },
      (error: any) => {  // Handle error
        console.error('Error updating assignment', error);
      }
    );
  }

  // Delete an assignment
  deleteAssignment(id: number): void {
    this.assignmentService.deleteAssignment(id).subscribe(
      () => {
        this.assignmentList = this.assignmentList.filter(item => item.id !== id);  // Remove from list
      },
      (error: any) => { // Handle error
        console.error('Error deleting assignment', error);
      }
    );
  }

  // Reset form fields
  resetForm(): void {
    this.newAssignment = { id: 0, title: '', description: '', dueDate: '' };  // Reset the form to default state
  }
}
