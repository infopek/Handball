using Handball.Player;
using Handball.Player.Position;

using System.Collections.Generic;

namespace Handball.Game
{
    public class Match
    {
        public delegate void MatchEventHandler();
        public event MatchEventHandler Goal;
        public event MatchEventHandler Injury;
        public event MatchEventHandler YellowCard;
        public event MatchEventHandler RedCard;

        private const int MIN_SIZE = 10;
        private const int MAX_SIZE = 16;
        private const int FIELD_SIZE = 7;

        private const float BENCH_PROB = 1.0f / FIELD_SIZE;
        private const float GOAL_PROB = 0.12f;
        private const float INJ_PROB = 0.03f;
        private const float YC_PROB = 0.06f;
        private const float RC_PROB = 0.01f;

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

            // Match simulation, lasts 1 minute
            //for (int i = 1; i <= 60; i++)
            //{
            //    if (i % 15 == 0)
            //    {
            //        // Time for substitution
            //        MakeSubs(3);
            //    }
            //    if (Chance(GOAL_PROB))
            //    {
            //        // Goal has been scored
            //        Goal?.Invoke();
            //    }
            //    if (Chance(INJ_PROB))
            //    {
            //        // Someone has been injured
            //        Injury?.Invoke();
            //    }
            //    if (Chance(YC_PROB))
            //    {
            //        // A yellow card has been handed out
            //        YellowCard?.Invoke();
            //    }
            //    if (Chance(RC_PROB))
            //    {
            //        // A red card has been handed out
            //        RedCard?.Invoke();
            //    }

            //    System.Threading.Thread.Sleep(1000);    // wait 1 second
            //}
        }
        private void Start()
        {
            // Checking if teams have sufficient amount of players
            if (MIN_SIZE <= _teamA.TeamSize && _teamA.TeamSize <= MAX_SIZE)
            {
                _benchA = new IPlayer[_teamA.TeamSize - FIELD_SIZE];
                _playfieldA = ChooseField(_teamA);
            }
            else throw new NotEnoughPlayersException();

            if (MIN_SIZE <= _teamB.TeamSize && _teamB.TeamSize <= MAX_SIZE)
            {
                _benchB = new IPlayer[_teamB.TeamSize - FIELD_SIZE];
                _playfieldB = ChooseField(_teamB);
            }
            else throw new NotEnoughPlayersException();
        }
        /// <summary>
        /// Simulates probability accurately enough.
        /// </summary>
        private bool Chance(float prob)
        {
            return ((float)StaticRandom.Next(int.MaxValue) / int.MaxValue) <= prob;
        }
        /// <summary>
        /// Removes <paramref name="subCount"/> player(s) from the playing field at random and benches them, 
        /// then chooses <paramref name="subCount"/> player(s) from the bench and moves them to the playing field.
        /// Note: This method makes the substitutions for both teams.
        /// </summary>
        /// <param name="subCount">Number of substitutions to be made.</param>
        private void MakeSubs(int subCount)
        {
            int subIndex = 0;
            int index = 0;
            IPlayer[] subs = new IPlayer[subCount];
            while (subIndex != 3)
            {
                // Keep looping through the playing field until we've found the 3 candidates
                index = (index + 1) % FIELD_SIZE;
                if (Chance(BENCH_PROB))
                {
                    subs[subIndex] = _playfieldA[index];
                    subIndex++;
                }
            }

            subIndex = 0;
            index = 0;
        }
        private IPlayer[] ChooseField(Team team)
        {
            List<IPlayer>[] R = new List<IPlayer>[FIELD_SIZE];
            for (int i = 0; i < FIELD_SIZE; i++)
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

            IPlayer[] E = new IPlayer[FIELD_SIZE];
            IPlayer[] optimalField = new IPlayer[FIELD_SIZE];
            bool eligible = false;
            Backtrack(R, 0, ref eligible, ref E, ref optimalField);

            if (eligible) return optimalField;
            else throw new TeamNotEligibleException();
        }
        /// <summary>
        /// Recursive backtracking for selecting the best players from an array of lists.
        /// </summary>
        /// <param name="E">The array containing a temporary, often partial solution.</param>
        private void Backtrack(List<IPlayer>[] R, int level, ref bool eligible, ref IPlayer[] E, ref IPlayer[] optSol)
        {
            int i = 0;
            while (i < R[level].Count)
            {
                if (Fk(level, R[level][i], E))
                {
                    E[level] = R[level][i];
                    if (level == R.Length - 1)
                    {
                        if (!eligible || Fitness(E) > Fitness(optSol))
                        {
                            // Copy contents of temp solution to optimal solution
                            for (int j = 0; j < R.Length; j++)
                            {
                                optSol[j] = E[j];
                            }
                        }
                        eligible = true;
                    }
                    else
                    {
                        Backtrack(R, level + 1, ref eligible, ref E, ref optSol);
                    }
                }
                i++;
            }
        }
        /// <summary>
        /// Determines the 'availability' of the <paramref name="curr"/> player, duplicate-wise.
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
        /// <summary>
        /// Calculates the fitness of a given <paramref name="lineup"/> from 3 skills: strength, speed and endurance.
        /// </summary>
        /// <returns>The sum of these skills.</returns>
        private int Fitness(IPlayer[] lineup)
        {
            int fitness = 0;
            foreach (var player in lineup)
            {
                fitness += player.Strength + player.Speed + player.Endurance;
            }

            return fitness;
        }
    }
}
