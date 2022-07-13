namespace TankGameDomain
{
    public class Tank
    {
        public Guid Id { get; set; }
        public User Owner { get; set; }
        public string Name { get; set; }
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public int Rotation { get; set; } = 0;
    }
}
