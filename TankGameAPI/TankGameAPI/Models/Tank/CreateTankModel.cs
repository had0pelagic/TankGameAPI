namespace TankGameAPI.Models.Tank
{
    public class CreateTankModel
    {
        public UserModel Owner { get; set; }
        public string Name { get; set; }
    }
}
