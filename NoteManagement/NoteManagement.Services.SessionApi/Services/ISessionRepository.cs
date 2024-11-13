using NoteManagement.Services.SessionApi.Models;

public interface ISessionRepository
{
    Task<IEnumerable<Session>> GetAllSessions(); // This should fetch all sessions
    Task<IEnumerable<Session>> GetAllSessionsByUser(int userId);
    Task<IEnumerable<Session>> GetAllSessionsByDate(DateTime date);
    Task<IEnumerable<Session>> GetAllSessionsByTitle(string title);
    Task<Session> CreateSession(Session session);
    Task UpdateSession(Session session);
    Task<bool> DeleteSession(int id);
}
