using System;

namespace Handball.Game
{
    public class TeamAlreadyExistsException : Exception
    {
        public TeamAlreadyExistsException()
            : base("Team already exists with given name.")
        {

        }
    }
}