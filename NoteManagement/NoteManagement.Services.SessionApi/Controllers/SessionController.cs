// File: Controllers/SessionController.cs
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

        // GET: api/Session
        
        // GET: api/Session/user/{userId}
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
                return NoContent(); // HTTP 204 if no sessions found

            return Ok(sessions); // HTTP 200
        }

        // GET: api/Session/date/{date}
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
                return NoContent(); // HTTP 204 if no sessions found

            return Ok(sessions); // HTTP 200
        }

        // GET: api/Session/title/{title}
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
                return NoContent(); // HTTP 204 if no sessions found

            return Ok(sessions); // HTTP 200
        }

        // POST: api/Session
        [HttpPost]
        public async Task<ActionResult<Session>> CreateSession(Session session)
        {
            var createdSession = await _repository.CreateSession(session);
            return CreatedAtAction(nameof(GetAllSessionsByUser), new { userId = session.UserId }, createdSession); // HTTP 201 Created
        }

        // PUT: api/Session/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSession(int id, Session session)
        {
            

            await _repository.UpdateSession(id,session);
            return NoContent(); // HTTP 204 No Content
        }

        // DELETE: api/Session/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSession(int id)
        {
            var deleted = await _repository.DeleteSession(id);
            if (!deleted)
                return NotFound(); // HTTP 404 if session not found

            return NoContent(); // HTTP 204 No Content
        }
    }
}
