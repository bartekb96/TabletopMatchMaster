using Microsoft.Extensions.Configuration;
using TabletopMatchMaster.Console.Configurations;
using TabletopMatchMaster.Console.Models;
using TabletopMatchMaster.Logic.Services;

public class Program
{
    private const int columnWidth = 25;
    private static int numberOfArmies;

    public static void Main(string[] args)
    {

        var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

        var allyArmies = new TeamArmies();
        config.GetSection(TeamArmies.SectionName).Bind(allyArmies);

        if (allyArmies.GoodArmies.Count != allyArmies.EvilArmies.Count)
        {
            throw new InvalidOperationException("Liczba armii dobra i zła musi być równa.");
        }

        numberOfArmies = allyArmies.GoodArmies.Count;

        var oponentArmies = new TeamArmies();

        for (int i = 0; i < numberOfArmies; i++)
        {
            Console.WriteLine($"Podaj {i + 1} armię zła oponentów:");
            var army = Console.ReadLine();
            oponentArmies.EvilArmies.Add(army);
        }

        for (int i = 0; i < numberOfArmies; i++)
        {
            Console.WriteLine($"Podaj {i + 1} armię dobra oponentów:");
            var army = Console.ReadLine();
            oponentArmies.GoodArmies.Add(army);
        }

        int[,] allyGoodArmiesRaitingTable = new int[numberOfArmies, numberOfArmies];
        int[,] allyEvilArmiesRaitingTable = new int[numberOfArmies, numberOfArmies];

        for (int i = 0; i < numberOfArmies; i++)
        {
            for (int j = 0; j < numberOfArmies; j++)
            {
                Console.WriteLine($"Podaj ocenę gry {allyArmies.GoodArmies[i]} VS {oponentArmies.EvilArmies[j]}:");
                var ratingInput = Console.ReadLine();
                if (int.TryParse(ratingInput, out int rating))
                {
                    allyGoodArmiesRaitingTable[i, j] = rating;
                    RefreshRaitingTables(allyArmies, oponentArmies, allyGoodArmiesRaitingTable, allyEvilArmiesRaitingTable);
                }
            }
        }

        for (int i = 0; i < numberOfArmies; i++)
        {
            for (int j = 0; j < numberOfArmies; j++)
            {
                Console.WriteLine($"Podaj ocenę gry {allyArmies.EvilArmies[i]} VS {oponentArmies.GoodArmies[j]}:");
                var ratingInput = Console.ReadLine();
                if (int.TryParse(ratingInput, out int rating))
                {
                    allyEvilArmiesRaitingTable[i, j] = rating;
                    RefreshRaitingTables(allyArmies, oponentArmies, allyGoodArmiesRaitingTable, allyEvilArmiesRaitingTable);
                }
            }
        }

        var bijectionCalculator = new BijectionCalculator();

        var possibleAllyGoodParings = bijectionCalculator.GetBijections(allyArmies.GoodArmies, oponentArmies.EvilArmies);
        var possibleEvilGoodParings = bijectionCalculator.GetBijections(allyArmies.EvilArmies, oponentArmies.GoodArmies);

        var goodArmiesRaitings = new List<PairingEvaluation>();
        var evilArmiesRaitings = new List<PairingEvaluation>();

        foreach (var pairing in possibleAllyGoodParings)
        {
            var raiting = 0;
            foreach (var (allyGood, oponentEvil) in pairing)
            {
                var allyArmyIndex = allyArmies.GoodArmies.IndexOf(allyGood);
                var oponentArmyIndex = oponentArmies.EvilArmies.IndexOf(oponentEvil);
                raiting += allyGoodArmiesRaitingTable[allyArmyIndex, oponentArmyIndex];
            }

            goodArmiesRaitings.Add(new PairingEvaluation
            {
                Pairs = pairing,
                Rating = raiting
            });
        }

        goodArmiesRaitings = goodArmiesRaitings.OrderByDescending(p => p.Rating).ToList();

        Console.WriteLine("Najlepsze paringi dla naszego dobra");

        for (int i = 0; i < goodArmiesRaitings.Count; i++)
        {
            Console.WriteLine($"{i + 1}) Rate: {goodArmiesRaitings[i].Rating}");
            foreach (var (allyGood, oponentEvil) in goodArmiesRaitings[i].Pairs)
            {
                Console.WriteLine($"{allyGood} VS {oponentEvil}");
            }

            Console.WriteLine();
        }

        foreach (var pairing in possibleEvilGoodParings)
        {
            var raiting = 0;
            foreach (var (ally, oponent) in pairing)
            {
                var allyArmyIndex = allyArmies.EvilArmies.IndexOf(ally);
                var oponentArmyIndex = oponentArmies.GoodArmies.IndexOf(oponent);
                raiting += allyEvilArmiesRaitingTable[allyArmyIndex, oponentArmyIndex];
            }

            evilArmiesRaitings.Add(new PairingEvaluation
            {
                Pairs = pairing,
                Rating = raiting
            });
        }

        evilArmiesRaitings = evilArmiesRaitings.OrderByDescending(p => p.Rating).ToList();

        Console.WriteLine("Najlepsze paringi dla naszego zła");

        for (int i = 0; i < evilArmiesRaitings.Count; i++)
        {
            Console.WriteLine($"{i + 1}) Rate: {evilArmiesRaitings[i].Rating}");
            foreach (var (allyGood, oponentEvil) in evilArmiesRaitings[i].Pairs)
            {
                Console.WriteLine($"{allyGood} VS {oponentEvil}");
            }

            Console.WriteLine();
        }

        Console.ReadKey();
    }

    private static void RefreshRaitingTables(
        TeamArmies allyArmies,
        TeamArmies oponentArmies,
        int[,] allGoodArmiesRaitingTable,
        int[,] allEvilArmiesRaitingTable)
    {
        Console.Clear();
        Console.WriteLine("Tabela ocen naszego dobra:");

        Console.Write($"{string.Empty, -columnWidth}");

        for (int i = 0; i < numberOfArmies; i++)
        {
            Console.Write($"{oponentArmies.EvilArmies[i],-columnWidth}");
        }

        Console.Write("\r\n");

        for (int i = 0; i < numberOfArmies; i++)
        {
            Console.Write($"{allyArmies.GoodArmies[i],-columnWidth}");

            for (int j = 0; j < numberOfArmies; j++)
            {
                var ceelValue = allGoodArmiesRaitingTable[i, j] > 0 ? allGoodArmiesRaitingTable[i, j].ToString() : string.Empty;
                Console.Write($"{ceelValue,-columnWidth}");
            }

            Console.Write("\r\n");
        }

        Console.Write("\r\n");

        Console.WriteLine("Tabela ocen naszego zła:");

        Console.Write($"{string.Empty,-columnWidth}");

        for (int i = 0; i < numberOfArmies; i++)
        {
            Console.Write($"{oponentArmies.GoodArmies[i],-columnWidth}");
        }

        Console.Write("\r\n");

        for (int i = 0; i < numberOfArmies; i++)
        {
            Console.Write($"{allyArmies.EvilArmies[i],-columnWidth}");

            for (int j = 0; j < numberOfArmies; j++)
            {
                var ceelValue = allEvilArmiesRaitingTable[i, j] > 0 ? allEvilArmiesRaitingTable[i, j].ToString() : string.Empty;
                Console.Write($"{ceelValue,-columnWidth}");
            }

            Console.Write("\r\n");
        }

        Console.Write("\r\n");
    }
}