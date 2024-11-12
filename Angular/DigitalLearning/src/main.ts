import { bootstrapApplication } from '@angular/platform-browser';
import { provideRouter } from '@angular/router';
import { provideHttpClient } from '@angular/common/http';
import { appRoutes } from './app/app.routes';
import { SessionComponent } from './app/pages/session.component';

bootstrapApplication(SessionComponent, {
  providers: [provideRouter(appRoutes), provideHttpClient()],
}).catch(err => console.error(err));
