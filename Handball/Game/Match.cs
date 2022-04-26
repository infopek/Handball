using Handball.Player;
using Handball.Collections.Generic;
using Handball.Player.Position;
using System.Collections.Generic;

namespace Handball.Game
{
    public class Match
    {
        // Backtracking
        private int N;
        private int[] M;
        private List<IPlayer>[] R;

        private const int _minSize = 10;
        private const int _maxSize = 16;
        private const int _numPlayersOnField = 7;

        private IPlayer[] _benchA;
        private IPlayer[] _benchB;
        private IPlayer[] _playfieldA;
        private IPlayer[] _playfieldB;
        private Team _teamA;
        private Team _teamB;

        public Match(Team t1, Team t2)
        {
            if (t1.Name != t2.Name)
            {
                _teamA = t1;
                _teamB = t2;
            }
            else throw new TeamAlreadyExistsException();
        }
        
        public void Simulation()
        {
            Start();
        }
        private void Start()
        {
            // Checking if teams have sufficient amount of players
            if (_minSize <= _teamA.TeamSize && _teamA.TeamSize <= _maxSize)
            {
                _benchA = new IPlayer[_teamA.TeamSize - _numPlayersOnField];
                _playfieldA = ChooseField(_teamA);
            }
            else throw new NotEnoughPlayersException();

            if (_minSize <= _teamB.TeamSize && _teamB.TeamSize <= _maxSize)
            {
                _benchB = new IPlayer[_teamB.TeamSize - _numPlayersOnField];
                _playfieldB = ChooseField(_teamB);
            }
            else throw new NotEnoughPlayersException();
        }
        private IPlayer[] ChooseField(Team team)
        {
            N = _numPlayersOnField;
            M = new int[N];

            R = new List<IPlayer>[N];
            for (int i = 0; i < N; i++)
            {
                R[i] = new List<IPlayer>();
            }

            // Each position has a fixed place in the array of lists
            foreach (var player in team.Players)
            {
                if (player is Goalkeeper)
                    R[0].Add(player);
                else if (player is Substitute)
                    R[1].Add(player);
                else if (player is Pivot)
                    R[2].Add(player);
                else if (player is Winger)
                {
                    R[3].Add(player);
                    R[4].Add(player);
                }
                else if (player is Shooter)
                {
                    R[5].Add(player);
                    R[6].Add(player);
                }
            }
            // The number of players to choose from for a position
            for (int i = 0; i < N; i++)
            {
                M[i] = R[i].Count;
            }

            bool eligible = false;
            IPlayer[] E = new IPlayer[N];
            Backtrack(0, ref eligible, ref E);

            if (eligible) return E;
            else throw new NotEnoughPlayersException();
        }
        private void Backtrack(int level, ref bool eligible, ref IPlayer[] E)
        {
            int i = 0;
            while (!eligible & i < M[level])
            {
                if (Fk(level, R[level][i], E))
                {
                    E[level] = R[level][i];
                    if (level == N - 1)
                        eligible = true;
                    else
                        Backtrack(level + 1, ref eligible, ref E);
                }
                i++;
            }
        }
        /// <summary>
        /// Determines whether or not the current player is the best choice skill-wise.
        /// </summary>
        private bool Ft(int level, IPlayer curr)
        {
            for (int i = 0; i < M[level]; i++)
            {
                if (CompareSkills(R[level][i], curr))
                    return false;
            }
            return true;
        }
        /// <summary>
        /// Determines the 'availability' of the current player, duplicate-wise.
        /// Note: Duplicates can really only occur when there are duplicate positions.
        /// </summary>
        private bool Fk(int level, IPlayer curr, IPlayer[] E)
        {
            for (int i = 0; i < level; i++)
            {
                if (E[i].Equals(curr))               
                    return false;               
            }
            return true;
        }
        /// <returns>True if p1 has greater skill combination, false otherwise.</returns>
        private bool CompareSkills(IPlayer p1, IPlayer p2)
        {
            int p1Sum = p1.Strength + p1.Speed + p1.Endurance;
            int p2Sum = p2.Strength + p2.Speed + p2.Endurance;
            return p1Sum > p2Sum;
        }

    }  
}
