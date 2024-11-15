using NoteManagement.Services.SessionApi.Models;

namespace NoteManagement.Services.SessionApi.Services
{
    public interface ISessionRepository
    {
        Task<IEnumerable<Session>> GetAllSessionsByUser(string userId);
        Task<IEnumerable<Session>> GetAllSessionsByDate(DateTime date,string userid);
        Task<IEnumerable<Session>> GetAllSessionsByTitle(string title,string userid);
        Task<bool> CreateSession(Session session,string userid);
        Task<bool> UpdateSession(int id,Session session);
        Task<bool> DeleteSession(int id);
    }
}
