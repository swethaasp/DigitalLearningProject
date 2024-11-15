using NoteManagement.Services.NoteApi.Models;

namespace NoteManagement.Services.NoteApi.Repository
{
    public interface INoteRepository
    {
        Task<IEnumerable<Note>> GetNotesByUser(string userId);
        Task<List<Note>> GetNoteByCreatedDate(DateTime dateCreated,string userid);
        Task<List<Note>> GetNoteByModifiedDate(DateTime dateModified,string userid);
        Task<Note> CreateNote(Note note,string userid);
        Task<bool> UpdateNote(int userid,Note note);
        Task<bool> DeleteNote(int id);
    }
}
