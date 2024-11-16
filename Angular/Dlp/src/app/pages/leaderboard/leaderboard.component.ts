import { Component } from '@angular/core';
import { LeaderboardService } from '../../services/leaderboard.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-leaderboard',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './leaderboard.component.html',
  styleUrl: './leaderboard.component.css'
})
export class LeaderboardComponent {
  leaderboard: any[] = []; 

  constructor(private leaderboardService: LeaderboardService) {}

  ngOnInit(): void {
    this.fetchLeaderboard();
  }

  fetchLeaderboard(): void {
    this.leaderboardService.getLeaderboard().subscribe(
      (data) => {
        this.leaderboard = data; // Store the leaderboard data
      },
      (error) => {
        console.error('Error fetching leaderboard:', error);
      }
    );
  }
}
