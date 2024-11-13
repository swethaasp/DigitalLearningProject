// File: Models/Session.cs
using System;

namespace NoteManagement.Services.SessionApi.Models
{
    public class Session
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }  // Date of the session
        public string Title { get; set; }   // Title of the session
        public string Description { get; set; } // Description of the session
        public string Resources { get; set; }   // Resources or materials associated with the session
        public int AssignmentId { get; set; }   // Foreign key to Assignment
        public string UserId { get; set; }         // Foreign key to User
    }
}
