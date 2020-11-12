using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace kortspill
{
    internal class GameManager
    {
        private static readonly List<Thread> Threads = new List<Thread>();
        private static readonly List<Player> Players = new List<Player>();
        public static bool GameOver = false;

        public static void EndGame()
        {
            GameOver = true;

            Thread.Sleep(1000);

            PrintWinner();
        }

        private static void PrintWinner()
        {
            Console.WriteLine(" ");

            foreach (var player in Players.Where(player => player.Winner))
            {
                Console.WriteLine("|--------------------|");
                Console.WriteLine("| " + player.GetName() + " won the game! |");
                Console.WriteLine("|--------------------|");
                Console.WriteLine();
                Console.WriteLine("Winning hand:");
                foreach (var card in player.GetHand())
                {
                    Console.WriteLine("-" + card.getCardName());
                }
            }
        }
        public void Init()
        {
            var dealer = new Dealer();
            var numberOfPlayers = -1;
            while (numberOfPlayers == -1)
            {
                numberOfPlayers = GetNumberOfPlayers();
            }
            Console.Clear();
            CreatePlayers(numberOfPlayers);
            CreateThreads(numberOfPlayers);
            StartThreads(numberOfPlayers);
        }

        private static int GetNumberOfPlayers()
        {
            Console.Clear();
            Console.WriteLine("How many players?");
            var input = Console.ReadLine();

            if (!int.TryParse(input, out var num))
            {
                Console.WriteLine("You must write a number");
                Thread.Sleep(1500);
            }
            else
            {
                if (num > 4 || num < 2)
                {
                    Console.WriteLine("You must choose between 2-4");
                    Thread.Sleep(1500);
                    return -1;
                }
                return num;
            }

            return -1;
        }

        private static void CreatePlayers(int num)
        {
            //Create players
            for (var i = 0; i < num; i++)
            {
                Players.Add(PlayerFactory.CreatePlayer());

            }
        }
        private static void CreateThreads(int num)
        {
            //Create threads
            for (var i = 0; i < num; i++)
            {
                Threads.Add(new Thread(new ThreadStart(Players[i].Play)));
            }
        }
        private static void StartThreads(int num)
        {
            //Start threads
            for (var i = 0; i < num; i++)
            {
                Threads[i].Start();
            }
        }
    }
}
