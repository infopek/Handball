using Handball.Collections.Generic;

namespace Handball.Player
{
    public class Team
    {
        private LinkedList<IPlayer> _players;

        public string Name { get; set; }
        public int TeamSize { get; private set; } = 0;
        public LinkedList<IPlayer> Players { get => _players; }

        public Team(string name)
        {
            Name = name;
            _players = new LinkedList<IPlayer>();
        }

        public void SignPlayer(IPlayer player)
        {
            player.Team = this;
            _players.Insert(player);
            TeamSize++;
        }
        public void RemovePlayer(IPlayer player)
        {
            player.Team = null;
            _players.Delete(player);
            TeamSize--;
        }
        public void ListPlayers(TraverseHandler<IPlayer> handler) => _players.Traverse(handler);
    }
}