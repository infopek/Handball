namespace Handball.Player.Position
{
    public class Goalkeeper : IPlayer
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public int Strength { get; set; }
        public int Speed { get; set; }
        public int Endurance { get; set; }
        public Team Team { get; set; }
        public int Saves { get; set; } = 0;
    }
}
