using Microsoft.EntityFrameworkCore;
using NoteManagement.Services.NoteApi.Models;

namespace NoteManagement.Services.NoteApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Note> Notes { get; set; }  // Define the DbSet for Note entity
    }
}
