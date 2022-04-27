using System;
using System.Collections.Generic;

using Handball.Player;
using Handball.Player.Position;
using Handball.Utils;

namespace Handball.Game
{
    public class Match
    {
        public delegate void MatchEventHandler(IPlayer player);

        public event MatchEventHandler Goal;
        public event MatchEventHandler Save;
        public event MatchEventHandler Injury;
        public event MatchEventHandler YellowCard;
        public event MatchEventHandler RedCard;

        private const int MIN_SIZE = 10;
        private const int MAX_SIZE = 16;
        private const int FIELD_SIZE = 7;

        private const float GOAL_PROB = 0.12f;
        private const float SAVE_PROB = 0.2f;
        private const float INJ_PROB = 0.03f;
        private const float YC_PROB = 0.06f;
        private const float RC_PROB = 0.01f;

        private IPlayer[] benchA;
        private IPlayer[] benchB;
        private IPlayer[] playfieldA;
        private IPlayer[] playfieldB;
        private Team teamA;
        private Team teamB;

        public Match(Team t1, Team t2)
        {
            if (t1.Name != t2.Name)
            {
                teamA = t1;
                teamB = t2;
            }
            else throw new TeamAlreadyExistsException();
        }

        public void Simulation()
        {
            Start();

            // Match simulation, lasts 1 minute
            for (int i = 1; i <= 60; i++)
            {
                if (i % 15 == 0)
                {
                    // Both teams make 3 substitutions
                    MakeSubs(3, ref playfieldA, ref benchA);
                    MakeSubs(3, ref playfieldB, ref benchB);
                }

                if (StaticRandom.Chance(GOAL_PROB))
                {
                    IPlayer player = StaticRandom.Choice(playfieldA, playfieldB);
                    player.Goals++;
                    Goal?.Invoke(player);
                }
                else if (StaticRandom.Chance(SAVE_PROB))
                {
                    IPlayer[] chosenField = StaticRandom.Chance(0.5f) ? playfieldA : playfieldB;
                    foreach (var player in chosenField)
                    {
                        if (player is Goalkeeper)
                        {
                            Goalkeeper keeper = (Goalkeeper)player;
                            keeper.Saves++;
                            Save?.Invoke(keeper);
                        }
                    }
                }

                if (StaticRandom.Chance(INJ_PROB))
                {
                    IPlayer player = StaticRandom.Choice(playfieldA, playfieldB);
                    Injury?.Invoke(player);
                }
                if (StaticRandom.Chance(YC_PROB))
                {
                    IPlayer player = StaticRandom.Choice(playfieldA, playfieldB);
                    YellowCard?.Invoke(player);
                }
                if (StaticRandom.Chance(RC_PROB))
                {
                    IPlayer player = StaticRandom.Choice(playfieldA, playfieldB);
                    RedCard?.Invoke(player);
                }

                System.Threading.Thread.Sleep(10);    // wait 1 second
            }
        }
        /// <summary>
        /// Removes <paramref name="subCount"/> player(s) from the playing field at random and benches them, 
        /// then chooses <paramref name="subCount"/> player(s) from the bench and moves them to the playing field.
        /// </summary>
        /// <param name="subCount">Number of substitutions to be made.</param>
        private void MakeSubs(int subCount, ref IPlayer[] field, ref IPlayer[] bench)
        {
            // Choose who to substitute from the playing field
            int[] fieldIndices = StaticRandom.GetNUnique(subCount, 0, field.Length);

            // Initialize the table for backtracking
            List<IPlayer>[] R = new List<IPlayer>[subCount];
            for (int i = 0; i < subCount; i++)
            {
                R[i] = new();
                foreach (var benched in bench)
                {
                    if (benched.GetType() == field[fieldIndices[i]].GetType())
                    {
                        // Possible candidate for a sub
                        R[i].Add(benched);
                    }
                }
            }

            // Use backtracking to find the 3 best players from bench
            IPlayer[] E = new IPlayer[subCount];
            IPlayer[] newPlayers = new IPlayer[subCount];
            bool eligible = false;
            Backtrack(R, 0, ref eligible, ref E, ref newPlayers);

            if (eligible)
            {
                // Found everyone, make the substitution
                for (int i = 0; i < subCount; i++)
                {
                    IPlayer temp = field[fieldIndices[i]];
                    field[fieldIndices[i]] = newPlayers[i];
                    newPlayers[i] = temp;

                    field[fieldIndices[i]].IsBenched = true;
                    newPlayers[i].IsBenched = false;
                }
            }
            else throw new TeamNotEligibleException();
        }
        /// <summary>
        /// Initializes the playing fields and benches for both teams.
        /// </summary>
        private void Start()
        {
            // Check if teams have sufficient amount of players, initialize the playing fields
            if (MIN_SIZE <= teamA.TeamSize && teamA.TeamSize <= MAX_SIZE)
                playfieldA = ChooseField(teamA);
            else throw new NotEnoughPlayersException();
            if (MIN_SIZE <= teamB.TeamSize && teamB.TeamSize <= MAX_SIZE)
                playfieldB = ChooseField(teamB);
            else throw new NotEnoughPlayersException();

            // Initialize the bench for team A
            int benchACounter = 0;
            benchA = new IPlayer[teamA.TeamSize - FIELD_SIZE];
            foreach (var member in teamA.Players)
            {
                if (member.IsBenched)
                {
                    benchA[benchACounter] = member;
                    benchACounter++;
                }
            }

            // Initialize the bench for team B
            int benchBCounter = 0;
            benchB = new IPlayer[teamB.TeamSize - FIELD_SIZE];
            foreach (var member in teamB.Players)
            {
                if (member.IsBenched)
                {
                    benchB[benchBCounter] = member;
                    benchBCounter++;
                }
            }
        }
        /// <summary>
        /// Initializes the playing field for the given <paramref name="team"/>.
        /// </summary>
        private IPlayer[] ChooseField(Team team)
        {
            // Initialize a table for backtracking
            List<IPlayer>[] R = new List<IPlayer>[FIELD_SIZE];
            for (int i = 0; i < FIELD_SIZE; i++)
            {
                R[i] = new();
            }

            // Each position has a fixed place in the array of lists
            foreach (var player in team.Players)
            {
                if (player is Goalkeeper)       R[0].Add(player);
                else if (player is Center)      R[1].Add(player);
                else if (player is CircleRunner)R[2].Add(player);
                else if (player is LeftWinger)  R[3].Add(player);
                else if (player is RightWinger) R[4].Add(player);
                else if (player is LeftBack)    R[5].Add(player);
                else if (player is RightBack)   R[6].Add(player);
            }

            IPlayer[] E = new IPlayer[FIELD_SIZE];
            IPlayer[] optimalField = new IPlayer[FIELD_SIZE];
            bool eligible = false;
            Backtrack(R, 0, ref eligible, ref E, ref optimalField);

            if (eligible)
            {
                foreach (var player in optimalField)
                {
                    player.IsBenched = false;
                }
                return optimalField;
            }
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
                i++;
            }
        }
        ///// <summary>
        ///// Determines the 'availability' of the <paramref name="curr"/> player, duplicate-wise.
        ///// </summary>
        //private bool Fk(int level, IPlayer curr, IPlayer[] E)
        //{
        //    for (int i = 0; i < level; i++)
        //    {
        //        if (E[i].Equals(curr))
        //            return false;
        //    }
        //    return true;
        //}
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
