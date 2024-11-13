using NoteManagement.Services.AssignmentApi.Models;

public interface IAssignmentRepository
{
    Task<IEnumerable<Assignment>> GetAllAssignments();  // Fetch all assignments
    Task<Assignment> GetAssignmentById(int id);
    Task<IEnumerable<Assignment>> GetAssignmentsByUser(int userId);
    Task<IEnumerable<Assignment>> GetAssignmentsByDate(DateTime date);
    Task<IEnumerable<Assignment>> GetAssignmentsByDeadline(DateTime date);
    Task<IEnumerable<Assignment>> GetAssignmentsByTitle(string title);
    Task<Assignment> CreateAssignment(Assignment assignment);
    Task UpdateAssignment(Assignment assignment);
    Task<bool> DeleteAssignment(int id);
}
