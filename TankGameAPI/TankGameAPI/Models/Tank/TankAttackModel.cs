namespace TankGameAPI.Models.Tank
{
    public class TankAttackModel
    {
        public string Name { get; set; }
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public int Rotation { get; set; } = 0;
    }
}
