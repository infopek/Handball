using Handball.Player;
using Handball.Collections.Generic;

namespace Handball.Game
{
    public class Match
    {
        private Team _teamA;
        private Team _teamB;
        private IPlayer[] _benchA;
        private IPlayer[] _benchB;
        private IPlayer[] _playfieldA;
        private IPlayer[] _playfieldB;

        public void Start()
        {
            if (_teamA.TeamSize < 7 || _teamB.TeamSize < 7)
                throw new NotEnoughPlayersException();

            FillTeams();
        }
        public void Simulation()
        {
            Start();
        }
        private void FillTeams()
        {
            bool eligible = false;
        }
        private void Backtrack(int level, ref bool eligible, ref LinkedList<IPlayer> currLineup)
        {

        }
    }  
}
