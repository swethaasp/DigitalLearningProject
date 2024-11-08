using Microsoft.EntityFrameworkCore;
using NoteManagement.Services.SessionApi.Models;

namespace NoteManagement.Services.SessionApi.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Session> Sessions { get; set; }
        

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    // Configure relationships and any other entity configurations here
        //    modelBuilder.Entity<Session>()
        //        .HasOne(s => s.Assignment)
        //        .WithMany()
        //        .HasForeignKey(s => s.AssignmentId)
        //        .OnDelete(DeleteBehavior.SetNull); // You can customize this delete behavior
        //}
    }
}
