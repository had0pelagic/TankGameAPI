using Mapster;
using MapsterMapper;
using TankGameAPI.Models;
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
            var user = _mapper.Map<User>(model);

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
