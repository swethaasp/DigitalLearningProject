using Microsoft.EntityFrameworkCore;
using NoteManagement.Services.AssignmentApi.Data;
using NoteManagement.Services.AssignmentApi.Models;

namespace NoteManagement.Services.AssignmentApi.Repository
{
    public class AssignmentRepository : IAssignmentRepository
    {
        private readonly AppDbContext _context;

        public AssignmentRepository(AppDbContext context)
        {
            _context = context;
        }

        

        public async Task<IEnumerable<Assignment>> GetAssignmentsByUser(string userId)
        {
            return await _context.Assignments.Where(a => a.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<Assignment>> GetAssignmentsByDate(DateTime date)
        {
            return await _context.Assignments.Where(a => a.DateAssigned.Date == date.Date).ToListAsync();
        }

        public async Task<IEnumerable<Assignment>> GetAssignmentsByDeadline(DateTime date)
        {
            return await _context.Assignments.Where(a => a.Deadline.Date == date.Date).ToListAsync();
        }

        public async Task<IEnumerable<Assignment>> GetAssignmentsByTitle(string title)
        {
            return await _context.Assignments.Where(a => a.Title.Contains(title)).ToListAsync();
        }

        public async Task<Assignment> CreateAssignment(Assignment assignment)
        {
            _context.Assignments.Add(assignment);
            await _context.SaveChangesAsync();
            return assignment;
        }

        public async Task<bool> UpdateAssignment(int id,Assignment assignment)
        {
            var asmnt =_context.Assignments.FirstOrDefault(a => a.Id==id);
            if (asmnt==null)
            {
                return false;
            }
            asmnt.Title=assignment.Title;
            asmnt.Status=assignment.Status;
            asmnt.Deadline=assignment.Deadline;
            asmnt.DateAssigned=assignment.DateAssigned;
            asmnt.Description=assignment.Description;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAssignment(int id)
        {
            var assignment = await _context.Assignments.FindAsync(id);
            if (assignment == null)
            {
                return false;
            }

            _context.Assignments.Remove(assignment);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
