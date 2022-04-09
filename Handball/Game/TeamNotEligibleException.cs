using System;

namespace Handball.Game
{
    public class TeamNotEligibleException : Exception
    {
        public TeamNotEligibleException()
            : base("Not enough players for a position.") { }
    }
}
