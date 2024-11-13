namespace NoteManagement.Srevices.AuthApi.Models.Dto
{
    public class ResponseDto
    {
        public object? Result { get; set; }
        public bool Issuccess { get; set; } = true;
        public string Message { get; set; } = "";
    }
}
