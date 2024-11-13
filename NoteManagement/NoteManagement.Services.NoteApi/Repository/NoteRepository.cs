using Microsoft.EntityFrameworkCore;
using NoteManagement.Services.NoteApi.Data;
using NoteManagement.Services.NoteApi.Models;
using System.Runtime.InteropServices;

namespace NoteManagement.Services.NoteApi.Repository
{
    public class NoteRepository : INoteRepository
    {
        private readonly AppDbContext _context;

        public NoteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Note>> GetNotesByUser(string userId)
        {
            return await _context.Notes.Where(n => n.UserId == userId).ToListAsync();
        }

        public async Task<List<Note>> GetNoteByCreatedDate(DateTime dateCreated) // Updated method name
        {
            return await _context.Notes.Where(n => n.DateCreated.Date == dateCreated.Date).ToListAsync();
        }


        public async Task<List<Note>> GetNoteByModifiedDate(DateTime dateModified)
        {
            return await _context.Notes.Where(n => n.DateModified.Date == dateModified.Date).ToListAsync()  ;
        }

        public async Task<Note> CreateNote(Note note)
        {
            _context.Notes.Add(note);
            await _context.SaveChangesAsync();
            return note;
        }

        public async Task<bool> UpdateNote(int id,Note note)
        {
            var n = _context.Notes.FirstOrDefault(n => n.Id==id);
            if (n==null)
            {
                return false;
            }
            n.Title=note.Title;
            n.Description=note.Description;
            n.DateCreated= note.DateCreated;
            n.DateModified=note.DateModified;
            n.Resources = note.Resources;
            
            await _context.SaveChangesAsync();
            return true;
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
