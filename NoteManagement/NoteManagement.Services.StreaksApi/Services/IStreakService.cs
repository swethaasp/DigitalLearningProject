using NoteManagement.Services.StreaksApi.Models;

namespace NoteManagement.Services.StreaksApi.Services
{
    public interface IStreakService
    {
        public Task<bool> CreateStreak(Streak obj);
        public   Task<IEnumerable<Streak>> Getall();

        public Task<Streak?> GetStreakById(string UserId);

        public Task<bool> IncrementStreak(string UserId);

        public Task<bool> Reset(string UserId);
    }
}
