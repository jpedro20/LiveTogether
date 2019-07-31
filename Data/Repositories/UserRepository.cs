using System.Linq;
using LiveTogether.Models;
using LiveTogether.Utils.Security;

namespace LiveTogether.Data.Repositories
{
    public interface IUserRepository
    {
        User Authenticate(string username, string password);
        User GetById(int id);
    }


    public class UserRepository : IUserRepository
    {
        private LiveTogetherContext _context;

        public UserRepository(LiveTogetherContext context)
        {
            _context = context;
        }


        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public User Authenticate(string username, string password)
        {
            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) {
                return null;
            }

            var user = _context.Users.SingleOrDefault(u => u.Username == username);

            if(user == null) {
                return null;
            }

            if(!AuthSecurity.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt)) {
                return null;
            }

            return user;
        }
    }
}