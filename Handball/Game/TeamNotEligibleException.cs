using System;

namespace Handball.Game
{
    public class TeamNotEligibleException : Exception
    {
        public TeamNotEligibleException()
            : base("A team doesn't have sufficient amount of players or can't fill in all the positions.")
        {

        }
    }
}
