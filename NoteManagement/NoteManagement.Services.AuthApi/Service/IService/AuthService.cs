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
                    //create role if it is not in existance
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user,roleName);
                return true;

            }
            return false;

        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user= _db.ApplicationUser.FirstOrDefault(u=>u.UserName.ToLower()== loginRequestDto.UserName.ToLower());
            bool isValid=await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
            //if role found ,generate token
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
            {   // Prepare user data for User API
                var userData = new User
                {
                    IdentityUserId = user.Id,
                    DisplayName = registrationRequestDto.Name
                };

                // Call User API to save additional user information
                var userApiUrl = "https://localhost:7080/api/User"; // Replace with actual URL
                var userContent = new StringContent(JsonSerializer.Serialize(userData), Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(userApiUrl, userContent);

                if (!response.IsSuccessStatusCode)
                {
                    return  "Failed to save user info in User API.";
                }

                
            
                var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);
                if (result.Succeeded)
                {
                    var userToReturn = _db.ApplicationUser.First(user => user.UserName == registrationRequestDto.Email);

                    //working on this
                    var roleExist = await _roleManager.RoleExistsAsync("User");
                    if (!roleExist)
                    {
                        await _roleManager.CreateAsync(new IdentityRole("User"));
                    }


                    var resp = await _userManager.AddToRoleAsync(userToReturn, "User");

                    if (!resp.Succeeded)
                    {
                        return "Failed to save user info in User API.";
                    }


                    UserDto userDto = new()
                    {
                        Email = userToReturn.Email,
                        Id = userToReturn.Id,
                        Name = userToReturn.Name,
                        PhoneNumber = userToReturn.PhoneNumber
                    };



                    return "";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }

            }
            catch (Exception ex)
            {

            }
            return ("Error Encountered");
            
        }
    }
}
