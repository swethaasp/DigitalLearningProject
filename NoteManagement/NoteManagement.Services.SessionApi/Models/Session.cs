// File: Models/Session.cs
using System;

namespace NoteManagement.Services.SessionApi.Models
{
    public class Session
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } 
        public string Title { get; set; }   
        public string Description { get; set; } 
        public string Resources { get; set; }   
        public int AssignmentId { get; set; }  
        public string UserId { get; set; }        
    }
}
