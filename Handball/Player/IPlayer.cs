using System;

namespace Handball.Player
{
    public interface IPlayer : IComparable<IPlayer>
    {
        string Name { get; set; }
        int Age { get; set; }
        int Strength { get; set; }
        int Speed { get; set; }
        int Endurance { get; set; }
        int Goals { get; set; }
        bool IsBenched { get; set; }
        Team Team { get; set; }

        int IComparable<IPlayer>.CompareTo(IPlayer other)
        {
            return Name.CompareTo(other.Name);
        }
    }
}
