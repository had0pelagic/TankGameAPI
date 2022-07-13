using TankGameAPI.Models;

namespace TankGameAPI.Services
{
    public interface IUserService
    {
        public Task<string> CreateUser(CreateUserModel model);
        public Task<string> RemoveUser(UserModel model);
        public Task<List<UserModel>> GetUser();

    }
}
