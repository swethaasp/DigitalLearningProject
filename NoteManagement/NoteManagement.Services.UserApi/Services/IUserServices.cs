using NoteManagement.Services.UserApi.Models;

namespace NoteManagement.Services.UserApi.Services
{
    public interface IUserServices
    {
        public User GetUser(string id);
        public bool CreateUser(User user);
        public bool UpdateUser(User user);
        public bool DeleteUser(string id);

    }
}
