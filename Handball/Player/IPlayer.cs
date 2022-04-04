namespace Handball.Player
{
    public interface IPlayer
    {
        string Name { get; set; }
        int Age { get; set; }
        int Strength { get; set; }
        int Speed { get; set; }
        int Endurance { get; set; }
        Team Team { get; set; }
    }
}
