using NoteManagement.Srevices.AuthApi.Models.Dto;

namespace NoteManagement.Srevices.AuthApi.Service.IService
{
    public interface IAuthService
    {
        Task<string> Register(RegistrationRequestDto registrationRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);

        Task<bool> AssignRole(string email, string rolename);
    }
}
