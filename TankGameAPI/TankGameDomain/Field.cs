namespace TankGameDomain
{
    public class Field
    {
        public Guid Id { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int LeftBorder { get; set; }
        public int RightBorder { get; set; }
        public int TopBorder { get; set; }
        public int BottomBorder { get; set; }
        public List<Obstacle> Obstacles { get; set; }
    }
}
