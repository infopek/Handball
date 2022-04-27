using System;
using Handball.Player;
using Handball.Player.Position;
using Handball.Game;

namespace Handball
{
    public class Program
    {
        static void Main(string[] args)
        {
            Team t1 = new Team("Nice Team");
            t1.SignPlayer(new Goalkeeper("Carlos", 34, 70, 20, 100));
            t1.SignPlayer(new Goalkeeper("Kerci", 26, 57, 34, 145));
            t1.SignPlayer(new LeftBack("Montra", 25, 64, 15, 230));
            t1.SignPlayer(new LeftBack("Gehir", 27, 63, 7, 390));
            t1.SignPlayer(new RightBack("Marcos", 18, 35, 24, 300));
            t1.SignPlayer(new RightBack("Lurkil", 28, 85, 30, 265));
            t1.SignPlayer(new LeftWinger("Lei", 21, 78, 32, 200));
            t1.SignPlayer(new LeftWinger("Nebi", 27, 96, 12, 185));
            t1.SignPlayer(new RightWinger("Teri", 22, 45, 17, 140));
            t1.SignPlayer(new RightWinger("Mikue", 30, 75, 6, 170));
            t1.SignPlayer(new CircleRunner("Poiri", 30, 81, 10, 250));
            t1.SignPlayer(new CircleRunner("Perlu", 20, 46, 25, 135));
            t1.SignPlayer(new Center("Ponvo", 26, 25, 5, 100));
            t1.SignPlayer(new Center("Iloka", 34, 30, 3, 120));

            Team t2 = new Team("Whata Team");
            t2.SignPlayer(new Goalkeeper("Semil", 20, 85, 30, 145));
            t2.SignPlayer(new Goalkeeper("Meazi", 30, 88, 26, 320));
            t2.SignPlayer(new LeftBack("Gavil", 21, 56, 24, 200));
            t2.SignPlayer(new LeftBack("Heakis", 30, 67, 21, 340));
            t2.SignPlayer(new RightBack("Olkua", 28, 97, 16, 280));
            t2.SignPlayer(new RightBack("Kerju", 16, 74, 23, 240));
            t2.SignPlayer(new LeftWinger("Pero", 17, 46, 21, 170));
            t2.SignPlayer(new LeftWinger("Feci", 25, 15, 31, 210));
            t2.SignPlayer(new RightWinger("Juekir", 23, 85, 38, 120));
            t2.SignPlayer(new RightWinger("Menuka", 20, 50, 14, 165));
            t2.SignPlayer(new CircleRunner("Lirka", 27, 45, 32, 265));
            t2.SignPlayer(new CircleRunner("Leoak", 35, 36, 4, 100));
            t2.SignPlayer(new Center("Cieuj", 22, 41, 26, 145));
            t2.SignPlayer(new Center("Akile", 24, 56, 16, 210));

            Match match = new Match(t1, t2);
            match.Goal += Match_Goal;
            match.Save += Match_Save;
            match.Injury += Match_Injury;
            match.YellowCard += Match_YellowCard;
            match.RedCard += Match_RedCard;
            match.Simulation();
            Console.WriteLine("Done!");
        }

        private static void Match_RedCard(IPlayer player)
        {
            Console.WriteLine($"{ player.Name } has been handed a red card!");
        }
        private static void Match_YellowCard(IPlayer player)
        {
            Console.WriteLine($"{ player.Name } has been handed a yellow card!");
        }
        private static void Match_Injury(IPlayer player)
        {
            Console.WriteLine($"{ player.Name } has been injured!");
        }
        private static void Match_Save(IPlayer player)
        {
            Console.WriteLine($"{ player.Name } has saved a shot!");
        }
        private static void Match_Goal(IPlayer player)
        {
            Console.WriteLine($"{ player.Name } has scored a goal!");
        }
    }
}
