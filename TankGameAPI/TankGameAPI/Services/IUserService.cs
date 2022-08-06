using TankGameAPI.Models.User;

namespace TankGameAPI.Services
{
    public interface IUserService
    {
        public Task<string> CreateUser(CreateUserModel model);
        public Task<string> RemoveUser(UserModel model);
        public Task<bool> IsUserValid(UserModel model);
    }
}
