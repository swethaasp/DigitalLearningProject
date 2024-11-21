import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterOutlet } from '@angular/router';
import { NavbarService } from '../../services/navbar.service';
import { jwtDecode } from 'jwt-decode';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [FormsModule,CommonModule],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {
  streak:any=null;
  userRole: string | null = null;
  
  constructor(private router:Router,private navbarservice:NavbarService){}
  logout() {
    // Remove the token from localStorage or sessionStorage
    localStorage.removeItem('token');
    
    // Optionally clear other user-related data
    localStorage.removeItem('userDetails');

    // Redirect to login page
    this.router.navigate(['/login']);
  }

  ngOnInit(): void {
    this.fetchstreak();
    const token = localStorage.getItem('token');
    if (token) {
      try {
        const decodedToken: any = jwtDecode(token);
        this.userRole = decodedToken.RoleS || null; 
      } catch (error) {
        console.error('Error decoding token:', error);
        this.userRole = null;
      }
    }
  }

  fetchstreak(): void {
    this.navbarservice.getstreak().subscribe(
      (data) => {
        this.streak = data; // Store the leaderboard data
      },
      (error) => {
        console.error('Error fetching leaderboard:', error);
      }
    );
  
}}


