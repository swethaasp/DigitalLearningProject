using System.Reflection.Metadata.Ecma335;

namespace NoteManagement.Srevices.AuthApi.Models.Dto
{
    public class LoginRequestDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
