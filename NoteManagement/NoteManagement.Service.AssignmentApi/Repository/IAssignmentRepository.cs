using NoteManagement.Services.AssignmentApi.Models;

namespace NoteManagement.Services.AssignmentApi.Repository
{
    public interface IAssignmentRepository
    {
        Task<IEnumerable<Assignment>> GetAssignmentsByUser(string userId);
        Task<IEnumerable<Assignment>> GetAssignmentsByDate(DateTime dateAssigned);
        Task<IEnumerable<Assignment>> GetAssignmentsByDeadline(DateTime deadline);
        Task<IEnumerable<Assignment>> GetAssignmentsByTitle(string title);
        Task<Assignment> CreateAssignment(Assignment assignment);
        Task<bool> UpdateAssignment(int id,Assignment assignment);
        Task<bool> DeleteAssignment(int id);
    }
}
