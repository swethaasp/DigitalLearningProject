﻿namespace NoteManagement.Services.AssignmentApi.Models
{
    public class Assignment
    {
        public int Id { get; set; }
        public int AssignmentId { get; set; }
        public DateTime DateAssigned { get; set; }
        public DateTime Deadline { get; set; }
        public string Status { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; } 
    }
}
