import { Component, OnInit } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { SessionService } from '../services/session.service';

@Component({
  selector: 'app-session',
  templateUrl: './session.component.html',
  styleUrls: ['./session.component.css'],
  standalone: true,
  imports: [HttpClientModule],
})
export class SessionComponent implements OnInit {
  sessions: any[] = [];
  selectedSession: any;

  constructor(private sessionService: SessionService) {}

  ngOnInit(): void {
    this.sessionService.getSessionsWithAssignments().subscribe((data) => {
      this.sessions = data;
    });
  }

  openPopup(session: any): void {
    this.selectedSession = session;
  }

  closePopup(): void {
    this.selectedSession = null;
  }
}
