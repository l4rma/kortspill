using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace kortspill
{
    class GameManager
    {
        private static List<Thread> threads = new List<Thread>();
        private static List<Player> players = new List<Player>();
        private object _lock;
        public static bool gameOver = false;

        public static void endGame()
        {
            gameOver = true;

            Thread.Sleep(1000);

            printWinner();
        }

        private static void printWinner()
        {
            Console.WriteLine(" ");

            foreach (Player player in players)
            {
                if (player.winner)
                {
                    Console.WriteLine("|-----------------|");
                    Console.WriteLine("|-" + player.getName() + " won the game!-|");
                    Console.WriteLine("|-----------------|");
                    Console.WriteLine();
                    Console.WriteLine("Winning hand:");
                    foreach (Card card in player.getHand())
                    {
                        Console.WriteLine("-" + card.getCardName());
                    }

                }
            }
        }
        public void Init()
        {
            Deck deck = new Deck();
            string input = null;
            int num = -1;
            while (!int.TryParse(input, out num)) // Check if input is int
            {
                Console.Clear();
                Console.WriteLine("How many players?");
                input = Console.ReadLine();
            }
            Console.Clear();
            createPlayers(num);
            createThreads(num);
            startThreads(num);
        }

        private void createPlayers(int num)
        {
            //Create players
            for (int i = 0; i < num; i++)
            {
                players.Add(PlayerFactory.CreatePlayer());

            }
        }
        private void createThreads(int num)
        {
            //Create threads
            for (int i = 0; i < num; i++)
            {
                threads.Add(new Thread(new ThreadStart(players[i].play)));
            }
        }
        private void startThreads(int num)
        {
            //Start threads
            for (int i = 0; i < num; i++)
            {
                threads[i].Start();
            }
        }


    }
}
