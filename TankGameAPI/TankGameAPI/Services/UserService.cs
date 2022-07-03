using Mapster;
using TankGameAPI.Models;
using TankGameDomain;
using TankGameInfrastructure;

namespace TankGameAPI.Services
{
    public class UserService : IUserService
    {
        private readonly Context _context;

        public UserService(Context context)
        {
            _context = context;
        }

        public async Task<string> CreateUser(CreateUserModel model)
        {
            var user = model.Adapt<User>();

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user.Name;
        }

        public Task<List<UserModel>> GetUser()
        {
            throw new NotImplementedException();
        }
    }
}
