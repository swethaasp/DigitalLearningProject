using Microsoft.AspNetCore.Mvc;
using NoteManagement.Services.AssignmentApi.Models;
using NoteManagement.Services.AssignmentApi.Repository;

namespace NoteManagement.Services.AssignmentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentController : ControllerBase
    {
        private readonly IAssignmentRepository _assignmentRepository;

        public AssignmentController(IAssignmentRepository assignmentRepository)
        {
            _assignmentRepository = assignmentRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAssignmentById(int id)
        {
            var assignment = await _assignmentRepository.GetAssignmentById(id);
            if (assignment == null) return NotFound();
            return Ok(assignment);
        }

        [HttpGet("byuser/{userId}")]
        public async Task<IActionResult> GetAssignmentsByUser(int userId)
        {
            var assignments = await _assignmentRepository.GetAssignmentsByUser(userId);
            return Ok(assignments);
        }

        [HttpGet("bydate/{date}")]
        public async Task<IActionResult> GetAssignmentsByDate(DateTime date)
        {
            var assignments = await _assignmentRepository.GetAssignmentsByDate(date);
            return Ok(assignments);
        }

        [HttpGet("bydeadline/{date}")]
        public async Task<IActionResult> GetAssignmentsByDeadline(DateTime date)
        {
            var assignments = await _assignmentRepository.GetAssignmentsByDeadline(date);
            return Ok(assignments);
        }

        [HttpGet("bytitle/{title}")]
        public async Task<IActionResult> GetAssignmentsByTitle(string title)
        {
            var assignments = await _assignmentRepository.GetAssignmentsByTitle(title);
            return Ok(assignments);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAssignment([FromBody] Assignment assignment)
        {
            var createdAssignment = await _assignmentRepository.CreateAssignment(assignment);
            return CreatedAtAction(nameof(GetAssignmentById), new { id = createdAssignment.Id }, createdAssignment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAssignment(int id, [FromBody] Assignment assignment)
        {
            if (id != assignment.Id) return BadRequest();
            await _assignmentRepository.UpdateAssignment(assignment);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssignment(int id)
        {
            var result = await _assignmentRepository.DeleteAssignment(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
