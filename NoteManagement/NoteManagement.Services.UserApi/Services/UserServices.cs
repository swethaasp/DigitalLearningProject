using NoteManagement.Services.UserApi.Data;
using NoteManagement.Services.UserApi.Models;

namespace NoteManagement.Services.UserApi.Services
{
    public class UserServices : IUserServices
    {
        private readonly UserDbContext _db;

        public UserServices(UserDbContext db)
        {
            _db = db;
        }

        public bool CreateUser(User user)
        {
            try
            {
                _db.Users.Add(user);
                _db.SaveChanges(); 
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteUser(string id)
        {
            try
            {
                var user = _db.Users.FirstOrDefault(u=>u.IdentityUserId==id);
                if (user != null)
                {
                    _db.Users.Remove(user);
                    _db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                
                return false;
            }
        }

        public User? GetUser(string id)
        {
            return _db.Users.FirstOrDefault(u=>u.IdentityUserId==id); 
        }

        public bool UpdateUser(User user)
        {
            try
            {
                var u1 = _db.Users.FirstOrDefault(u=>u.IdentityUserId==user.IdentityUserId);
                if (u1 != null)
                {
                    u1.DisplayName = user.DisplayName;
                    _db.SaveChanges(); 
                    return true;
                }
                return false; 
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
