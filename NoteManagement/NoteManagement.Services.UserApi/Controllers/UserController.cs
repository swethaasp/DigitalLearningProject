using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoteManagement.Services.UserApi.Models;
using NoteManagement.Services.UserApi.Services;
using System.Security.Claims;

namespace NoteManagement.Services.UserApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _user;

        public UserController(IUserServices user)
        {
            _user = user;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var response = _user.getall();
            return Ok(response);
        }


        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            // Retrieve user ID from claims


            //var idClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            //if (idClaim == null)
            //{
            //    return Unauthorized("User ID not found in token claims.");
            //}

            //string userId = idClaim.Value;

            string userId=id;

            User u1 = _user.GetUser(userId);

            if (u1 == null)
            {
                return NotFound("User not found.");
            }

            return Ok(u1);
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            if (_user.CreateUser(user))
            {
                return CreatedAtAction(nameof(Get), new { id = user.IdentityUserId }, user);
            }

            return BadRequest("Error creating user.");
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, User user)
        {
            //if (id != user.IdentityUserId)
            //{
            //    return BadRequest("User ID mismatch.");
            //}

            var userExists = _user.GetUser(id);
            if (userExists == null)
            {
                return NotFound("User not found.");
            }

            if (_user.UpdateUser(user))
            {
                return Ok("Updated Successfully");
            }

            return BadRequest("Error updating user.");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var userExists = _user.GetUser(id);
            if (userExists == null)
            {
                return NotFound("User not found.");
            }

            if (_user.DeleteUser(id))
            {
                return NoContent();
            }

            return BadRequest("Error deleting user.");
        }
    }
}
