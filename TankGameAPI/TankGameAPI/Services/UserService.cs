using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using TankGameAPI.Models;
using TankGameAPI.Utils.Messages;
using TankGameDomain;
using TankGameInfrastructure;

namespace TankGameAPI.Services
{
    public class UserService : IUserService
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        public UserService(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<string> CreateUser(CreateUserModel model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Name == model.Name);

            if (user != null)
            {
                throw new Exception(Messages.User.Exists);
            }

            user = _mapper.Map<User>(model);

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
