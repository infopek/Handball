using System;

namespace Handball.Game
{
    public class NotEnoughPlayersException : Exception
    {
        public NotEnoughPlayersException()
            : base("Not enough players to start the game.")
        {

        }
    }
}
