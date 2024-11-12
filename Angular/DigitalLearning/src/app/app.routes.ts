import { Routes } from '@angular/router';
import { SessionComponent } from './pages/session.component';

export const appRoutes: Routes = [
  { path: 'sessions', component: SessionComponent },
  { path: '', redirectTo: '/sessions', pathMatch: 'full' }
];
