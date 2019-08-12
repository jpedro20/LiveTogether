using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LiveTogether.Models;
using LiveTogether.Utils.Security;

namespace LiveTogether.Data.Repositories
{
    public interface IUsersRepository
    {
        Task<User> Authenticate(string username, string password);
        Task<User> GetById(int id);
    }


    public class UsersRepository : IUsersRepository
    {
        private readonly LiveTogetherContext _context;

        public UsersRepository(LiveTogetherContext context)
        {
            _context = context;
        }


        public async Task<User> GetById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> Authenticate(string username, string password)
        {
            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) {
                return null;
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

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