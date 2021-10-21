using Football_Manager.Data;
using Football_Manager.Models;
using System;
using System.Diagnostics;
using System.Linq;

namespace ContosoUniversity.Data
{
    public static class DbInitializer
    {
        public static void Initialize(FootballDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Players.Any())
            {
                return;   // DB has been seeded
            }

            Debug.WriteLine("test" + context);

            var players = new Player[]
            {
            new Player{ShirtNo=8,Name="Juan Mata",Appearances=268,Goals=58},
            new Player{ShirtNo=6,Name="Paul Pogba",Appearances=130,Goals=28},
            new Player{ShirtNo=4,Name="Phil Jones",Appearances=200,Goals=2},
            new Player{ShirtNo=1,Name="David de Gea",Appearances=335,Goals=0}
            };
            foreach (Player p in players)
            {
                context.Players.Add(p);
            }
            context.SaveChanges();

            var positions = new Position[]
            {
                new Position{Name="Forward",DisplayOrder=1},
                new Position{Name="Midfielder",DisplayOrder=2},
                new Position{Name="Defender",DisplayOrder=3},
                new Position{Name="Goalkeeper",DisplayOrder=4}
            };

            foreach (Position p in positions)
            {
                context.Positions.Add(p);
            }
            context.SaveChanges();
        }
    }
}