import { ComponentFixture, TestBed } from '@angular/core/testing';
import { SessionComponent } from './session.component';
import { HttpClientModule } from '@angular/common/http';
import { SessionService } from '../services/session.service';
import { of } from 'rxjs';

describe('SessionComponent', () => {
  let component: SessionComponent;
  let fixture: ComponentFixture<SessionComponent>;
  let sessionService: SessionService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [SessionComponent],
      imports: [HttpClientModule],
      providers: [SessionService],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SessionComponent);
    component = fixture.componentInstance;
    sessionService = TestBed.inject(SessionService);

    // Mock data for sessions
    const mockSessions = [
      {
        id: 1,
        date: '2024-01-01',
        title: 'Session 1',
        resources: 'Resource 1',
        description: 'Description 1',
        assignmentid: 1,
        userid: 1,
        assignmentTitle: 'Assignment 1',
      },
      {
        id: 2,
        date: '2024-01-02',
        title: 'Session 2',
        resources: 'Resource 2',
        description: 'Description 2',
        assignmentid: 2,
        userid: 2,
        assignmentTitle: 'Assignment 2',
      },
    ];

    spyOn(sessionService, 'getSessionsWithAssignments').and.returnValue(of(mockSessions));

    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should fetch sessions and assignments', () => {
    expect(component.sessions.length).toBe(2);
  });

  it('should open popup when title is clicked', () => {
    component.openPopup(component.sessions[0]);
    expect(component.selectedSession).toBe(component.sessions[0]);
  });

  it('should close popup when close button is clicked', () => {
    component.openPopup(component.sessions[0]);
    component.closePopup();
    expect(component.selectedSession).toBeNull();
  });
});
s