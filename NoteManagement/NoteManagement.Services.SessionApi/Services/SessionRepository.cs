using NoteManagement.Services.SessionApi.Models;
using NoteManagement.Services.SessionApi.Data;
using Microsoft.EntityFrameworkCore;

public class SessionRepository : ISessionRepository
{
    private readonly AppDbContext _context;

    public SessionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Session>> GetAllSessions()
    {
        return await _context.Sessions.ToListAsync();
    }

    public async Task<IEnumerable<Session>> GetAllSessionsByUser(int userId)
    {
        return await _context.Sessions.Where(s => s.UserId == userId).ToListAsync();
    }

    public async Task<IEnumerable<Session>> GetAllSessionsByDate(DateTime date)
    {
        return await _context.Sessions.Where(s => s.Date.Date == date.Date).ToListAsync();
    }

    public async Task<IEnumerable<Session>> GetAllSessionsByTitle(string title)
    {
        return await _context.Sessions.Where(s => s.Title.Contains(title)).ToListAsync();
    }

    public async Task<Session> CreateSession(Session session)
    {
        _context.Sessions.Add(session);
        await _context.SaveChangesAsync();
        return session;
    }

    public async Task UpdateSession(Session session)
    {
        _context.Sessions.Update(session);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteSession(int id)
    {
        var session = await _context.Sessions.FindAsync(id);
        if (session == null) return false;

        _context.Sessions.Remove(session);
        await _context.SaveChangesAsync();
        return true;
    }
}
