// File: Services/ISessionRepository.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NoteManagement.Services.SessionApi.Models;

namespace NoteManagement.Services.SessionApi.Services
{
    public interface ISessionRepository
    {
        Task<IEnumerable<Session>> GetAllSessionsByUser(string userId);
        Task<IEnumerable<Session>> GetAllSessionsByDate(DateTime date);
        Task<IEnumerable<Session>> GetAllSessionsByTitle(string title);
        Task<bool> CreateSession(Session session);
        Task<bool> UpdateSession(int id,Session session);
        Task<bool> DeleteSession(int id);
    }
}
