import { enableProdMode, importProvidersFrom } from '@angular/core';
import { bootstrapApplication } from '@angular/platform-browser';
import { SessionComponent } from './app/pages/session/session.component';
import { provideHttpClient } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AssignmentComponent } from './app/pages/assignment/assignment.component';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';


bootstrapApplication(AssignmentComponent, {
  providers: [
    provideHttpClient(),
    importProvidersFrom(BrowserAnimationsModule),
  ]
}).catch((err) => console.error(err));





