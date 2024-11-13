using Microsoft.EntityFrameworkCore;
using NoteManagement.Services.NoteApi.Data;
using NoteManagement.Services.NoteApi.Models;

namespace NoteManagement.Services.NoteApi.Repository
{
    public class NoteRepository : INoteRepository
    {
        private readonly AppDbContext _context;

        public NoteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Note>> GetNotesByUser(int userId)
        {
            return await _context.Notes.Where(n => n.UserId == userId).ToListAsync();
        }

        public async Task<Note> GetNoteByCreatedDate(DateTime dateCreated) // Updated method name
        {
            return await _context.Notes.FirstOrDefaultAsync(n => n.DateCreated.Date == dateCreated.Date);
        }


        public async Task<Note> GetNoteByModifiedDate(DateTime dateModified)
        {
            return await _context.Notes.FirstOrDefaultAsync(n => n.DateModified.Date == dateModified.Date);
        }

        public async Task<Note> CreateNote(Note note)
        {
            _context.Notes.Add(note);
            await _context.SaveChangesAsync();
            return note;
        }

        public async Task<Note> UpdateNote(Note note)
        {
            _context.Notes.Update(note);
            await _context.SaveChangesAsync();
            return note;
        }

        public async Task<bool> DeleteNote(int id)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note == null)
            {
                return false;
            }

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
