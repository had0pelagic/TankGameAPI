using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using TankGameAPI.Models;
using TankGameAPI.Utils;
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
                throw new InvalidClientException(Messages.User.Exists);
            }

            user = _mapper.Map<User>(model);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user.Name;
        }

        public async Task<string> RemoveUser(UserModel model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Name == model.Username) ?? throw new InvalidClientException(Messages.User.NotFound);
            var tank = await _context.Tanks.Include(x => x.Owner).Where(x => x.Owner.Name == model.Username).FirstOrDefaultAsync() ?? throw new InvalidClientException(Messages.Tank.NotFound);

            _context.Tanks.Remove(tank);
            _context.Entry(tank).State = EntityState.Deleted;
            _context.Users.Remove(user);
            _context.Entry(user).State = EntityState.Deleted;

            await _context.SaveChangesAsync();

            return $"{model.Username} and tank {tank.Name} was removed";
        }

        public async Task<bool> IsUserValid(UserModel model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Name == model.Username);

            return user == null ? false : true;
        }
    }
}
