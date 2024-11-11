using Microsoft.AspNetCore.Identity;

namespace NoteManagement.Srevices.AuthApi.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string? Name { get; set; }
    }
}
