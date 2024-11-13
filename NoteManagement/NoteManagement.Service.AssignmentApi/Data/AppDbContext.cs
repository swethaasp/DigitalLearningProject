// AppDbContext.cs
using Microsoft.EntityFrameworkCore;
using NoteManagement.Services.AssignmentApi.Models;

namespace NoteManagement.Services.AssignmentApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Assignment> Assignments { get; set; }
    }
}
