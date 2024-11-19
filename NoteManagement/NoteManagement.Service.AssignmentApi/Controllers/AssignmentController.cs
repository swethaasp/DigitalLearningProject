using Microsoft.AspNetCore.Mvc;
using NoteManagement.Services.AssignmentApi.Models;
using NoteManagement.Services.AssignmentApi.Repository;
using Swashbuckle.AspNetCore.SwaggerUI;

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

        
        

        // Get assignments by user ID
        [HttpGet]
        public async Task<IActionResult> GetAssignmentsbyid()
        {
            var userid = HttpContext.Request.Headers["X-User-Id"].FirstOrDefault();
            if (userid == null)
            {
                return Unauthorized("No Userid in token");
            }
            var assignments = await _assignmentRepository.GetAssignmentbyId(userid);
            return Ok(assignments);
        }

        // Get assignments by date assigned
        [HttpGet("bydate/{date}")]
        public async Task<IActionResult> GetAssignmentsByDate(DateTime date)
        {
            var assignments = await _assignmentRepository.GetAssignmentsByDate(date);
            return Ok(assignments);
        }

        // Get assignments by deadline
        [HttpGet("bydeadline/{date}")]


        public async Task<IActionResult> GetAssignmentsByDeadline(DateTime date)
        {

            var userid = HttpContext.Request.Headers["X-User-Id"].FirstOrDefault();
            if (userid == null)
            {
                return Unauthorized("No Userid in token");
            }
            var assignments = await _assignmentRepository.GetAssignmentsByDeadline(userid,date);
            return Ok(assignments);
        }

        // Get assignments by title
        [HttpGet("bytitle/{title}")]
        public async Task<IActionResult> GetAssignmentsByTitle(string title)
        {
            var assignments = await _assignmentRepository.GetAssignmentsByTitle(title);
            return Ok(assignments);
        }

        // Create new assignment
        [HttpPost]
        public async Task<IActionResult> CreateAssignment([FromBody] Assignment assignment)
        {
            var createdAssignment = await _assignmentRepository.CreateAssignment(assignment);
            return Ok();
        }

        // Update existing assignment
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAssignment(int id, [FromBody] Assignment assignment)
        {
           
            await _assignmentRepository.UpdateAssignment(id,assignment);
            return NoContent();
        }

        [HttpPut("Submit/{id}")]

        public async Task<IActionResult> putSubmit(int id, [FromBody] Assignment assignment)
        {
           
            var res=await _assignmentRepository.SubmitAssignment(assignment, id);
            if (res == true)
            {
                return Ok();
            }
            return BadRequest();

        }
        

        // Delete assignment
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssignment(int id)
        {
            var result = await _assignmentRepository.DeleteAssignment(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
