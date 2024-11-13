using Microsoft.EntityFrameworkCore;
using NoteManagement.Services.StreaksApi.Data;
using NoteManagement.Services.StreaksApi.Models;

namespace NoteManagement.Services.StreaksApi.Services
{
    public class StreaksService : IStreakService
    {
        private readonly StreakDbContext _db;

        public StreaksService(StreakDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task<bool> CreateStreak(Streak obj)
        {
            Streak str = new Streak
            {
                IdentityUserId = obj.IdentityUserId,
                Streaks = obj.Streaks,
            };
            try
            {
                await _db.streaks.AddAsync(str);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;

        }

        public async Task<IEnumerable<Streak>> Getall()
        {
            return await _db.streaks.ToListAsync();
        }

        public async Task<Streak?> GetStreakById(string userId)
        {
            return await _db.streaks.FirstOrDefaultAsync(u => u.IdentityUserId == userId);
        }

        public async Task<bool> IncrementStreak(string userId)
        {
            var obj = await _db.streaks.FirstOrDefaultAsync(s => s.IdentityUserId == userId);
            if (obj == null)
            {
                return false;
            }

            try
            {
                obj.Streaks++;
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> Reset(string userId)
        {
            var obj = await _db.streaks.FirstOrDefaultAsync(s => s.IdentityUserId == userId);
            if (obj == null)
            {
                return false;
            }

            try
            {
                obj.Streaks = 0;
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
