using Microsoft.EntityFrameworkCore;
using NoteManagement.Services.UserApi.Models;

namespace NoteManagement.Services.UserApi.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
