using Mapster;
using TankGameAPI.Models.User;
using TankGameDomain;

namespace TankGameAPI.Mapping
{
    public class UserMappings : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<User, UserModel>()
                .Map(x => x.Username, y => y.Name);
            config.NewConfig<UserModel, User>()
                .Map(x => x.Name, y => y.Username);
        }
    }
}
