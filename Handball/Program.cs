using System;
using Handball.Player;
using Handball.Player.Position;
using Handball.Game;
using Handball.Collections.Generic;

namespace Handball
{
    public class Program
    {
        static void Main(string[] args)
        {
            Team t1 = new Team("Nice Team");
            t1.SignPlayer(new Goalkeeper("Carlos", 34, 70, 20, 100));
            t1.SignPlayer(new Shooter("Montra", 25, 64, 15, 230));
            t1.SignPlayer(new Winger("Lei", 21, 78, 32, 200));
            t1.SignPlayer(new Winger("Teri", 22, 45, 17, 140));
            t1.SignPlayer(new Shooter("Marcos", 18, 35, 24, 300));
            t1.SignPlayer(new Goalkeeper("Kerci", 26, 57, 34, 145));
            t1.SignPlayer(new Pivot("Poiri", 30, 81, 10, 250));
            t1.SignPlayer(new Shooter("Gehir", 27, 63, 7, 390));
            t1.SignPlayer(new Winger("Nebi", 27, 96, 12, 185));
            t1.SignPlayer(new Substitute("Ponvo", 26, 25, 5, 100));
            t1.SignPlayer(new Pivot("Perlu", 20, 46, 25, 135));

            Team t2 = new Team("Whata Team"); 
            t2.SignPlayer(new Goalkeeper("Semil", 20, 85, 30, 145));
            t2.SignPlayer(new Shooter("Gavil", 21, 56, 24, 200));
            t2.SignPlayer(new Winger("Pero", 17, 46, 21, 170));
            t2.SignPlayer(new Winger("Feci", 25, 15, 31, 210));
            t2.SignPlayer(new Shooter("Heakis", 30, 67, 21, 340));
            t2.SignPlayer(new Goalkeeper("Meazi", 30, 88, 26, 320));
            t2.SignPlayer(new Pivot("Lirka", 27, 45, 32, 265));
            t2.SignPlayer(new Shooter("Olkua", 28, 97, 16, 280));
            t2.SignPlayer(new Winger("Juekir", 23, 85, 38, 120));
            t2.SignPlayer(new Substitute("Cieuj", 22, 41, 26, 145));
            t2.SignPlayer(new Pivot("Leoak", 35, 36, 4, 100));

            Match match = new Match(t1, t2);
            match.Simulation();
        }
    }
}
