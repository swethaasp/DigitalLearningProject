// login.component.ts
import { Component, NgModule } from '@angular/core';
import { AuthServiceService } from '../../services/auth-service.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import {  HttpClientModule } from '@angular/common/http';
import { Router } from '@angular/router';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  imports: [FormsModule,CommonModule,HttpClientModule],
  providers: [AuthServiceService],
  standalone:true
})
export class LoginComponent {
  credentials = { userName: '', password: '' };

  constructor(private authService: AuthServiceService,private router:Router) {}
  onSubmit() {
    this.authService.login(this.credentials).subscribe(
      response => {
        console.log(response);
        if (response.result.token) {
          localStorage.setItem("token", response.result.token);
          alert('Login successful!');
          this.router.navigate(['/home']);
        } else {
          alert('Login successful, but no token found in response.');
        }
      },
      error => {
        alert('Login failed: Check Credentials');
      }
    );
  }
}
