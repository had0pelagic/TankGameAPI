namespace TankGameAPI.Models.Tank
{
    public class TankInformationModel
    {
        public UserModel Owner { get; set; }
        public string Name { get; set; }
        public int XPosition { get; set; }
        public int YPosition { get; set; }
    }
}
