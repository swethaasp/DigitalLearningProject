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
        [HttpGet("byuser/{userId}")]
        public async Task<IActionResult> GetNotesByUser(int userId)
        {
            var notes = await _noteRepository.GetNotesByUser(userId);
            return Ok(notes);
        }

        // GET: api/Note/bycreateddate/{date}
        [HttpGet("bycreateddate/{date}")]
        public async Task<IActionResult> GetNoteByCreatedDate(DateTime date) // Updated method name
        {
            var note = await _noteRepository.GetNoteByCreatedDate(date);
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
            var notes = await _noteRepository.GetNoteByModifiedDate(date);
            return Ok(notes);
        }

        // POST: api/Note
        [HttpPost]
        public async Task<IActionResult> CreateNote([FromBody] Note note)
        {
            var createdNote = await _noteRepository.CreateNote(note);
            return CreatedAtAction(nameof(GetNotesByUser), new { userId = createdNote.UserId }, createdNote);
        }

        // PUT: api/Note/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNote(int id, [FromBody] Note note)
        {
            if (id != note.Id)
            {
                return BadRequest();
            }

            await _noteRepository.UpdateNote(note);
            return NoContent();
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
