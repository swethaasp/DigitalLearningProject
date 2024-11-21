# Student Content Organizer  

An innovative platform designed to help students stay organized and on top of their assignments, notes, and progress tracking. The project focuses on backend security, scalability, and seamless integration with APIs.  

## Features  
- **Secure Microservices Architecture**: Implements JWT-based authentication, with claims extraction and custom headers for secure, role-based routing.  
- **Assignment Management**: Admins can create assignments, and each user's completion status is dynamically tracked.  
- **Notes Management**: Allows students to create, edit, and delete notes efficiently.  
- **Progress Tracking**: Enables users to view assignment completion stats and streaks.  
- **Dynamic Image Generation**: Generates personalized streak or leaderboard images using a custom API.  
- **Google Calendar Integration** *(Planned)*: Automatically adds assignment deadlines to users' calendars.  

## Technical Highlights  
- **Backend Security**:  
  - JWT token validation at the gateway level.  
  - Claims extraction for dynamic routing and headers.  
- **API Gateway (Ocelot)**:  
  - Manages routes for various microservices.  
  - Role-based access restrictions for sensitive endpoints.  
- **Microservices Architecture**:  
  - Modular and scalable services for Authentication, Assignments, Notes, Leaderboards, and Sessions.  
- **Frontend**: Built using Angular for a smooth user experience.  

## Tech Stack  
- **Backend**: ASP.NET Core, EF Core, Ocelot API Gateway  
- **Frontend**: Angular 8  
- **Database**: SQL Server  
- **Authentication**: JWT with claims-based security  
- **Hosting**: Local server with Docker for containerization  
