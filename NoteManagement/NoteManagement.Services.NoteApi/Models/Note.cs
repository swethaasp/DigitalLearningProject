namespace NoteManagement.Services.NoteApi.Models
{
    public class Note
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Resources { get; set; }
        public int UserId { get; set; } // Foreign Key for User
    }
}
