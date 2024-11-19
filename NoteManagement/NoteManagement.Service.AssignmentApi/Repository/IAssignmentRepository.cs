using NoteManagement.Services.AssignmentApi.Models;

namespace NoteManagement.Services.AssignmentApi.Repository
{
    public interface IAssignmentRepository
    {
        Task<IEnumerable<Assignment>> GetAssignmentbyId(string userid);
        Task<IEnumerable<Assignment>> GetAssignmentsByDate(DateTime dateAssigned);
        Task<IEnumerable<Assignment>> GetAssignmentsByDeadline(string userid,DateTime deadline);
        Task<IEnumerable<Assignment>> GetAssignmentsByTitle(string title);
        public Task<bool> SubmitAssignment(Assignment assignment, int id);
        Task<Assignment> CreateAssignment(Assignment assignment);
        Task<bool> UpdateAssignment(int id,Assignment assignment);
        Task<bool> DeleteAssignment(int id);
    }
}
