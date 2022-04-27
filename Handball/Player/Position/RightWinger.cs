namespace Handball.Player.Position
{
    public class RightWinger : IPlayer
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public int Strength { get; set; }
        public int Speed { get; set; }
        public int Endurance { get; set; }
        public Team Team { get; set; }

        public int Goals { get; set; } = 0;
        public bool IsBenched { get; set; } = true;

        public RightWinger(string name, int age, int strength, int speed, int endurance)
        {
            Name = name;
            Age = age;
            Strength = strength;
            Speed = speed;
            Endurance = endurance;
            Team = null;
        }

        public override bool Equals(object other)
        {
            return this.GetHashCode() == other.GetHashCode();
        }
        public override int GetHashCode()
        {
            int hash = 0;
            hash += Name.GetHashCode() * 7;
            hash += Age.GetHashCode() * 11;
            hash += Strength.GetHashCode() * 13;
            hash += Speed.GetHashCode() * 17;
            hash += Endurance.GetHashCode() * 19;
            return hash;
        }
    }
}
