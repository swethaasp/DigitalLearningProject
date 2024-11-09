using NoteManagement.Services.NoteApi.Models;

namespace NoteManagement.Services.NoteApi.Repository
{
    public interface INoteRepository
    {
        Task<IEnumerable<Note>> GetNotesByUser(int userId);
        Task<Note> GetNoteByCreatedDate(DateTime dateCreated);
        Task<Note> GetNoteByModifiedDate(DateTime dateModified);
        Task<Note> CreateNote(Note note);
        Task<Note> UpdateNote(Note note);
        Task<bool> DeleteNote(int id);
    }
}
