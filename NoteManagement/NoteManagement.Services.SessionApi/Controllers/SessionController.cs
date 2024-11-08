// File: Controllers/SessionController.cs
using Microsoft.AspNetCore.Mvc;
using NoteManagement.Services.SessionApi.Models;
using NoteManagement.Services.SessionApi.Services;
using System;
using System.Collections.Generic;
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

        // GET: api/Session/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Session>>> GetAllSessionsByUser(int userId)
        {
            var sessions = await _repository.GetAllSessionsByUser(userId);
            if (sessions == null || !sessions.Any())
                return NoContent(); // HTTP 204 if no sessions found

            return Ok(sessions); // HTTP 200
        }

        // GET: api/Session/date/{date}
        [HttpGet("date/{date}")]
        public async Task<ActionResult<IEnumerable<Session>>> GetAllSessionsByDate(DateTime date)
        {
            var sessions = await _repository.GetAllSessionsByDate(date);
            if (sessions == null || !sessions.Any())
                return NoContent(); // HTTP 204 if no sessions found

            return Ok(sessions); // HTTP 200
        }

        // GET: api/Session/title/{title}
        [HttpGet("title/{title}")]
        public async Task<ActionResult<IEnumerable<Session>>> GetAllSessionsByTitle(string title)
        {
            var sessions = await _repository.GetAllSessionsByTitle(title);
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
            if (id != session.Id)
                return BadRequest("Session ID mismatch"); // HTTP 400

            await _repository.UpdateSession(session);
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
