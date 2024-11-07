// File: Models/Assignment.cs
using NoteManagement.Service.AssignmentApi;
namespace AssignmentApi.Models
{
    public class Assignment
    {
        public int Id { get; set; }
        public DateTime DateAssigned { get; set; }
        public DateTime Deadline { get; set; }
        public string Status { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; } // Foreign key to User
    }
}
