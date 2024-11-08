// File: Services/ISessionRepository.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NoteManagement.Services.SessionApi.Models;

namespace NoteManagement.Services.SessionApi.Services
{
    public interface ISessionRepository
    {
        Task<IEnumerable<Session>> GetAllSessionsByUser(int userId);
        Task<IEnumerable<Session>> GetAllSessionsByDate(DateTime date);
        Task<IEnumerable<Session>> GetAllSessionsByTitle(string title);
        Task<Session> CreateSession(Session session);
        Task<Session> UpdateSession(Session session);
        Task<bool> DeleteSession(int id);
    }
}
