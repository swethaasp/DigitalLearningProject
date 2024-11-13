
using Microsoft.AspNetCore.Identity;
using NoteManagement.Srevices.AuthApi.Models;

namespace NoteManagement.Srevices.AuthApi.Service.IService
{
    public interface IJwtTokenGenerator
    {
        Task<string> GenerateToken(ApplicationUser applicationUser,UserManager<ApplicationUser> userManager);
    }
}
