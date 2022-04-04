using System.Collections.Generic;

namespace Handball.Player
{
    public class PlayerComparer : IComparer<IPlayer>
    {
        public int Compare(IPlayer x, IPlayer y)
        {
            return x.Name.CompareTo(y.Name);
        }
    }
}
