using Microsoft.EntityFrameworkCore;
using NoteManagement.Services.StreaksApi.Models;

namespace NoteManagement.Services.StreaksApi.Data
{
   
    public class StreakDbContext : DbContext
    {
        public StreakDbContext(DbContextOptions<StreakDbContext> options) : base(options) { }
        public DbSet<Streak> streaks { get; set; }
    }
}
