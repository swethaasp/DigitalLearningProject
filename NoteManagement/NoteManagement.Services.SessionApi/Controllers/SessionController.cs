using Microsoft.AspNetCore.Mvc;
using NoteManagement.Services.SessionApi.Models;
using NoteManagement.Services.SessionApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoteManagement.Services.SessionApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly ISessionRepository _repository;

        public SessionController(ISessionRepository repository)
        {
            _repository = repository;
        }

        
        [HttpGet("user")]
        public async Task<ActionResult<IEnumerable<Session>>> GetAllSessionsByUser()
        {
            var userid = HttpContext.Request.Headers["X-User-Id"].FirstOrDefault();
            if (userid == null)
            {
                return Unauthorized("No Userid in token");
            }
            var sessions = await _repository.GetAllSessionsByUser(userid);
            if (sessions == null || !sessions.Any())
                return NoContent(); 

            return Ok(sessions); 
        }

        [HttpGet("date/{date}")]
        public async Task<ActionResult<IEnumerable<Session>>> GetAllSessionsByDate(DateTime date)
        {
            var userid = HttpContext.Request.Headers["X-User-Id"].FirstOrDefault();
            if (userid == null)
            {
                return Unauthorized("No Userid in token");
            }
            var sessions = await _repository.GetAllSessionsByDate(date,userid);
            if (sessions == null || !sessions.Any())
                return NoContent(); 

            return Ok(sessions); 
        }

        [HttpGet("title/{title}")]
        public async Task<ActionResult<IEnumerable<Session>>> GetAllSessionsByTitle(string title)
        {
            var userid = HttpContext.Request.Headers["X-User-Id"].FirstOrDefault();
            if (userid == null)
            {
                return Unauthorized("No Userid in token");
            }
            var sessions = await _repository.GetAllSessionsByTitle(title,userid);
            if (sessions == null || !sessions.Any())
                return NoContent(); 
            return Ok(sessions); 
        }

        [HttpPost]
        public async Task<ActionResult<Session>> CreateSession(Session session)
        {
            var userid = HttpContext.Request.Headers["X-User-Id"].FirstOrDefault();
            if (userid == null)
            {
                return Unauthorized("No Userid in token");
            }
            var createdSession = await _repository.CreateSession(session,userid);
            return CreatedAtAction(nameof(GetAllSessionsByUser), new { userId = session.UserId }, createdSession);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSession(int id, Session session)
        {
            

            await _repository.UpdateSession(id,session);
            return NoContent(); 
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSession(int id)
        {
            var deleted = await _repository.DeleteSession(id);
            if (!deleted)
                return NotFound();

            return NoContent(); 
        }
    }
}
