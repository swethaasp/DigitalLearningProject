using NoteManagement.Services.NoteApi.Models;

namespace NoteManagement.Services.NoteApi.Repository
{
    public interface INoteRepository
    {
        Task<IEnumerable<Note>> GetNotesByUser(string userId);
        Task<List<Note>> GetNoteByCreatedDate(DateTime dateCreated);
        Task<List<Note>> GetNoteByModifiedDate(DateTime dateModified);
        Task<Note> CreateNote(Note note);
        Task<bool> UpdateNote(int userid,Note note);
        Task<bool> DeleteNote(int id);
    }
}
