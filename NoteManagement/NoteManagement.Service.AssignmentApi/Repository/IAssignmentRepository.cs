using NoteManagement.Services.AssignmentApi.Models;
using System.Threading.Tasks;

namespace NoteManagement.Services.AssignmentApi.Repository
{
    public interface IAssignmentRepository
    {
        Task<Assignment> GetAssignmentById(int id);
        Task<IEnumerable<Assignment>> GetAssignmentsByUser(int userId);
        Task<IEnumerable<Assignment>> GetAssignmentsByDate(DateTime dateAssigned);
        Task<IEnumerable<Assignment>> GetAssignmentsByDeadline(DateTime deadline);
        Task<IEnumerable<Assignment>> GetAssignmentsByTitle(string title);
        Task<Assignment> CreateAssignment(Assignment assignment);
        Task<Assignment> UpdateAssignment(Assignment assignment);
        Task<bool> DeleteAssignment(int id);
    }
}
