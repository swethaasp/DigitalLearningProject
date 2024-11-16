// import { Component } from '@angular/core';

// @Component({
//   selector: 'app-register',
//   standalone: true,
//   imports: [],
//   templateUrl: './register.component.html',
//   styleUrl: './register.component.css'
// })
// export class RegisterComponent {

// }


import { Component } from '@angular/core';

import { Router } from '@angular/router';
import { AuthServiceService } from '../../services/auth-service.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule,CommonModule],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  user = { email: '', name: '',phoneNumber:'',password:'' };
  constructor(private authService: AuthServiceService, private router: Router) {}

 
  onSubmit() {
    this.authService.register(this.user).subscribe(
      response => {
        console.log('Registration successful:', response);
        // Redirect to login page
        this.router.navigate(['/login']); // Update the path if needed
      },
      error => {
        console.error('Registration failed:', error);
      }
    );
  }
}
