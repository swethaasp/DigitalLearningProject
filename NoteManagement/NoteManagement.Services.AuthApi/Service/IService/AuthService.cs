using Microsoft.AspNetCore.Identity;
using NoteManagement.Srevices.AuthApi.Data;
using NoteManagement.Srevices.AuthApi.Models;

using NoteManagement.Srevices.AuthApi.Models.Dto;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using NoteManagement.Services.AuthApi.Models.Dto;


namespace NoteManagement.Srevices.AuthApi.Service.IService
{
    public class AuthService : IAuthService
    {
        public readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient,AppDbContext db,IJwtTokenGenerator jwtTokenGenerator,UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
            _roleManager = roleManager;
            _httpClient=httpClient;
        }

        public async Task<bool> AssignRole(string email, string roleName)
        { 
            var user = _db.ApplicationUser.FirstOrDefault(u=>u.Email.ToLower()==email.ToLower());
            if (user != null)
            {
                if(!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user,roleName);
                if (roleName.ToLower() != "user")
                {
                    await _userManager.RemoveFromRoleAsync(user, "User");
                }
                return true;

            }
            return false;

        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user= _db.ApplicationUser.FirstOrDefault(u=>u.UserName.ToLower()== loginRequestDto.UserName.ToLower());
            bool isValid=await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
            var token = await _jwtTokenGenerator.GenerateToken(user, _userManager);
            if (isValid == false || user == null)
            {
                return new LoginResponseDto() { User = null, Token = "" };
            }
            UserDto userDto = new()
            {
                Email = user.Email,
                Id=user.Id,
                Name=user.Name,
                PhoneNumber=user.PhoneNumber
            };
            LoginResponseDto loginResponseDto = new LoginResponseDto()
            {
                User = userDto,
                Token = token
            };
            return loginResponseDto;

        }

        public async Task<string> Register(RegistrationRequestDto registrationRequestDto)
        {
            ApplicationUser user = new()
            {
                UserName = registrationRequestDto.Email,
                Email = registrationRequestDto.Email,
                NormalizedEmail = registrationRequestDto.Email.ToUpper(),
                Name = registrationRequestDto.Name,
                PhoneNumber = registrationRequestDto.PhoneNumber,
            };

            try
            {
                var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);
                if (!result.Succeeded)
                {
                    return result.Errors.FirstOrDefault()?.Description ?? "User creation failed.";
                }

                var userData = new User
                {
                    IdentityUserId = user.Id,
                    DisplayName = registrationRequestDto.Name
                };

                var streakData = new StreakDto
                {
                    IdentityUserId = user.Id,
                    Streak = 0
                };

                var userApiUrl = "https://localhost:5007/api/User";
                var userContent = new StringContent(JsonSerializer.Serialize(userData), Encoding.UTF8, "application/json");
                var userApiResponse = await _httpClient.PostAsync(userApiUrl, userContent);
                if (!userApiResponse.IsSuccessStatusCode)
                {
                    await _userManager.DeleteAsync(user);  
                    return "Failed to save user info in User API.";
                }

            
                var streakApiUrl = "https://localhost:5006/api/Streak/Create";
                var streakContent = new StringContent(JsonSerializer.Serialize(streakData), Encoding.UTF8, "application/json");
                var streakApiResponse = await _httpClient.PostAsync(streakApiUrl, streakContent);
                if (!streakApiResponse.IsSuccessStatusCode)
                {
                    await _userManager.DeleteAsync(user); 
                    return "Failed to save Streak info in Streak API.";
                }


                if (!await _roleManager.RoleExistsAsync("User"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("User"));
                }

                var roleResult = await _userManager.AddToRoleAsync(user, "User");
                if (!roleResult.Succeeded)
                {
                    await _userManager.DeleteAsync(user);  
                    return "Failed to assign user role.";
                }

                return "";
            }
            catch (Exception ex)
            {
                return $"Registration failed: {ex.Message}";
            }
        }

    }
}
