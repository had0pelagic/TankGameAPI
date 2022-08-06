using TankGameAPI.Models.User;

namespace TankGameAPI.Models.Tank
{
    public class MoveTankModel
    {
        public UserModel Owner { get; set; }
        public TankModel Tank { get; set; }
    }
}
