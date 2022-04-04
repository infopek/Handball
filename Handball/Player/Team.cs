using Handball.Collections.Generic;

namespace Handball.Player
{
    public class Team
    {
        private LinkedList<IPlayer> _players;

        public string Name { get; set; }
        public int TeamSize { get; private set; } = 0;

        public Team(string name)
        {
            Name = name;
            _players = new LinkedList<IPlayer>(new PlayerComparer());
        }

        public void SignPlayer(IPlayer player)
        {
            _players.Insert(player);
            TeamSize++;
        }
        public void RemovePlayer(IPlayer player)
        {
            _players.Delete(player);
            TeamSize--;
        }
        public void ListPlayers(TraverseHandler<IPlayer> handler) => _players.Traverse(handler);
    }
}