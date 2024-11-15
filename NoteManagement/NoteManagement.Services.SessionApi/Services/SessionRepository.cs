using NoteManagement.Services.SessionApi.Models;
using NoteManagement.Services.SessionApi.Data;
using Microsoft.EntityFrameworkCore;
using NoteManagement.Services.SessionApi.Services;

public class SessionRepository : ISessionRepository
{
    private readonly AppDbContext _context;

    public SessionRepository(AppDbContext context)
    {
        _context = context;
    }

        public async Task<IEnumerable<Session>> GetAllSessionsByUser(string userId)
        {
            return await _context.Sessions.Where(s => s.UserId == userId).ToListAsync();
        }

    public async Task<IEnumerable<Session>> GetAllSessionsByDate(DateTime date,string userid)
    {
        return await _context.Sessions.Where(s => s.Date.Date == date.Date && s.UserId==userid).ToListAsync();
    }

    public async Task<IEnumerable<Session>> GetAllSessionsByTitle(string title,string userid)
    {
        return await _context.Sessions.Where(s => s.Title.Contains(title) && s.UserId==userid).ToListAsync();
    }

        public async Task<bool> CreateSession(Session session,string id)
        {
        Session s = new Session()
        {
            AssignmentId = session.AssignmentId,
            Title = session.Title,
            Date = session.Date,
            Description = session.Description,
            Resources = session.Resources,
            UserId = id
        };
            _context.Sessions.Add(s);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateSession(int id,Session session)
        {
            var ses =_context.Sessions.FirstOrDefault(s => s.Id == id);
            if (ses == null)
            {
                return false;
            }
            ses.Title=session.Title;
            ses.Date=session.Date;
            ses.Description=session.Description;
            ses.Resources=session.Resources;
            ses.AssignmentId=session.AssignmentId;
            
            await _context.SaveChangesAsync();
            return true;
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
