namespace Handball.Player.Position
{
    public class Shooter : IPlayer
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public int Strength { get; set; }
        public int Speed { get; set; }
        public int Endurance { get; set; }
        public Team Team { get; set; }
        public int Goals { get; set; } = 0;
    }
}
