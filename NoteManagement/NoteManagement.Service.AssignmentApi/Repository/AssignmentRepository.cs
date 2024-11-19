using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NoteManagement.Service.AssignmentApi.Models;
using NoteManagement.Services.AssignmentApi.Data;
using NoteManagement.Services.AssignmentApi.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace NoteManagement.Services.AssignmentApi.Repository
{
    public class AssignmentRepository : IAssignmentRepository
    {
        private readonly AppDbContext _context;
        private readonly HttpClient _httpclient;

        public AssignmentRepository(AppDbContext context,HttpClient http)
        {
            _context = context;
            _httpclient=http;
        }

        

        public async Task<IEnumerable<Assignment>> GetAssignmentbyId(string userid)
        {
            return await _context.Assignments.Where(a=>a.UserId==userid).ToListAsync();
        }

        public async Task<IEnumerable<Assignment>> GetAssignmentsByDate(DateTime date)
        {
            return await _context.Assignments.Where(a => a.DateAssigned.Date == date.Date).ToListAsync();
        }

        public async Task<IEnumerable<Assignment>> GetAssignmentsByDeadline(string userid,DateTime date)
        {
            return await _context.Assignments.Where(a => a.Deadline.Date == date.Date && a.UserId==userid).ToListAsync();
        }

        public async Task<IEnumerable<Assignment>> GetAssignmentsByTitle(string title)
        {
            return await _context.Assignments.Where(a => a.Title.Contains(title)).ToListAsync();
        }

        public async Task<bool> SubmitAssignment(Assignment assignment, int id)
        {
            // Fetch assignment by id
            var a = await _context.Assignments.FirstOrDefaultAsync(a => a.Id == id);
            if (a == null)
            {
                Console.WriteLine("A is null");
                return false; // Assignment not found
            }

            Console.WriteLine("Entered in if");

            // Update assignment details
            a.Status = "Completed";
            //a.Description = assignment.Description;

            // Save changes to database
            await _context.SaveChangesAsync();

            // Check deadline
            if (DateTime.UtcNow <= a.Deadline.ToUniversalTime())
            {
                Console.WriteLine("Deadline is true");

                // Construct the Streak API URL
                var streakUrl = "https://localhost:5006/api/Streak/Increment/";
                Console.WriteLine(streakUrl);

                try
                {
                    // Serialize UserId as JSON content
                    var userIdContent = new StringContent($"\"{a.UserId}\"", Encoding.UTF8, "application/json");

                    // Call Streak API with the UserId in the body
                    var userApiResponse = await _httpclient.PutAsync(streakUrl, userIdContent);

                    if (userApiResponse.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Streak incremented successfully.");
                    }
                    else
                    {
                        Console.WriteLine($"Failed to increment streak. Status: {userApiResponse.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error while calling streak API: {ex.Message}");
                }
            }


            return true;
        }


        public async Task<Assignment> CreateAssignment(Assignment assignment)
        {
            var UserLink = "https://localhost:5007/api/User";
            var response = await _httpclient.GetAsync(UserLink);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to fetch users.");
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var userList = JsonSerializer.Deserialize<List<User>>(responseContent);

            
            foreach (var user in userList)
            {
                var userAssignment = new Assignment
                {
                    AssignmentId = assignment.AssignmentId,
                    Title = assignment.Title,
                    Status = "Incomplete", 
                    Deadline = assignment.Deadline,
                    DateAssigned = assignment.DateAssigned,
                    Description = assignment.Description,
                    UserId = user.identityUserId 
                };

                _context.Assignments.Add(userAssignment);
            }

            await _context.SaveChangesAsync();
            return assignment;
        }


        public async Task<bool> UpdateAssignment(int assignmentId, Assignment updatedAssignment)
        {
            var assignments = _context.Assignments.Where(a => a.AssignmentId == assignmentId).ToList();
            if (!assignments.Any())
            {
                return false; 
            }

            foreach (var assignment in assignments)
            {
                assignment.Title = updatedAssignment.Title;
                assignment.Status = updatedAssignment.Status;
                assignment.Deadline = updatedAssignment.Deadline;
                assignment.DateAssigned = updatedAssignment.DateAssigned;
                assignment.Description = updatedAssignment.Description;
            }

            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> DeleteAssignment(int assignmentId)
        {
            var assignments = _context.Assignments.Where(a => a.AssignmentId == assignmentId).ToList();
            if (!assignments.Any())
            {
                return false; 
            }

            _context.Assignments.RemoveRange(assignments);

            await _context.SaveChangesAsync();
            return true;
        }

    }
}
