using Microsoft.AspNetCore.Mvc;
using NoteManagement.Services.NoteApi.Models;
using NoteManagement.Services.NoteApi.Repository;

namespace NoteManagement.Services.NoteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteRepository _noteRepository;

        public NoteController(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        // GET: api/Note/byuser/{userId}
        [HttpGet("byuser")]
        public async Task<IActionResult> GetNotesByUser()
        {
            var userid = HttpContext.Request.Headers["X-User-Id"].FirstOrDefault();
            if (userid == null)
            {
                return Unauthorized("No Userid in token");
            }
            var notes = await _noteRepository.GetNotesByUser(userid);
            return Ok(notes);
        }

        // GET: api/Note/bycreateddate/{date}
        [HttpGet("bycreateddate/{date}")]
        public async Task<IActionResult> GetNoteByCreatedDate(DateTime date) // Updated method name
        {
            var userid = HttpContext.Request.Headers["X-User-Id"].FirstOrDefault();
            if (userid == null)
            {
                return Unauthorized("No Userid in token");
            }
            var note = await _noteRepository.GetNoteByCreatedDate(date,userid);
            if (note == null)
            {
                return NotFound();
            }
            return Ok(note);
        }


        // GET: api/Note/bymodifieddate/{date}
        [HttpGet("bymodifieddate/{date}")]
        public async Task<IActionResult> GetNotesByModifiedDate(DateTime date)
        {
            var userid = HttpContext.Request.Headers["X-User-Id"].FirstOrDefault();
            if (userid == null)
            {
                return Unauthorized("No Userid in token");
            }
            var notes = await _noteRepository.GetNoteByModifiedDate(date,userid);
            return Ok(notes);
        }

        // POST: api/Note
        [HttpPost]
        public async Task<IActionResult> CreateNote([FromBody] Note note)
        {
            var userid = HttpContext.Request.Headers["X-User-Id"].FirstOrDefault();
            if (userid == null)
            {
                return Unauthorized("No Userid in token");
            }
            var createdNote = await _noteRepository.CreateNote(note,userid);
            return CreatedAtAction(nameof(GetNotesByUser), new { userId = createdNote.UserId }, createdNote);
        }

        // PUT: api/Note/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNote(int id, [FromBody] Note note)
        {
        
            bool res=await _noteRepository.UpdateNote(id,note);
            if (res)
            {
                return Ok();
            }
            return BadRequest();
        }

        // DELETE: api/Note/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            var result = await _noteRepository.DeleteNote(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
