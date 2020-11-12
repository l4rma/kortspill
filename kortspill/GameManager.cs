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
        private static readonly object Lock = new object();

        public static void EndGame()
        {
            lock (Lock)
            {
                GameOver = true;

                Thread.Sleep(1000);

                PrintWinner();
            }
            
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
                    Console.WriteLine("-" + card.GetCardName());
                }
            }
        }
        public void Init()
        {
            var dealer = new Dealer();

            var numberOfPlayers = ReturnNumberOfPlayers();

            CreatePlayers(numberOfPlayers);

            Dealer.DealStartingHands();
            Console.WriteLine("|-----------------------------------|");
            Console.WriteLine("| The Game will begin in 3 seconds! |");
            Console.WriteLine("|-----------------------------------|");
            Console.WriteLine();
            Thread.Sleep(3000);

            // Make 4 random cards remaining in the deck special
            MakeSpecialCards();
            
            CreateThreads(numberOfPlayers);
            
            StartThreads(numberOfPlayers);
        }

        private void MakeSpecialCards()
        {
            CreateTheBomb();
            CreateTheVulture();
            CreateTheQuarantine();
            CreateTheJoker();
        }

        private static void CreateTheJoker()
        {
            Dealer.ReturnRandomCardFromDeck().SpecialRule = "the Joker";
        }

        private static void CreateTheQuarantine()
        {
            Dealer.ReturnRandomCardFromDeck().SpecialRule = "the Quarantine";
        }

        private static void CreateTheVulture()
        {
            Dealer.ReturnRandomCardFromDeck().SpecialRule = "the Vulture";
        }

        private static void CreateTheBomb()
        {
            Dealer.ReturnRandomCardFromDeck().SpecialRule = "the Bomb";
        }

        private static int ReturnUserInput()
        {
            Console.Clear();
            WelcomeMessage();
            Console.WriteLine("|-------------------|");
            Console.WriteLine("| How many players? |");
            Console.WriteLine("|-------------------|");
            var input = Console.ReadLine();

            // Check if user input is valid
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

        private static void WelcomeMessage()
        {
            Console.WriteLine("|------------------------------------|");
            Console.WriteLine("| Welcome to Thread Battle Card Rush |");
            Console.WriteLine("|------------------------------------|");
        }

        private static int ReturnNumberOfPlayers()
        {
            var numberOfPlayers = -1;
            // Decide number of players
            while (numberOfPlayers == -1)
            {
                numberOfPlayers = ReturnUserInput();
            }
            return numberOfPlayers;
        }

        private static void CreatePlayers(int num)
        {
            //Create players
            for (var i = 0; i < num; i++)
            {
                Players.Add(PlayerFactory.CreatePlayer());
                Console.WriteLine("|---------------------|");
                Console.WriteLine("| " + Players[i].GetName() + " joins the game |");
            }
            Console.WriteLine("|---------------------|");
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

        public static List<Player> GetPlayers()
        {
            return Players;
        }

        public static void CheckCard(Player player, ICard card)
        {
            if (card.SpecialRule == null) return;
            
            ExecuteRule(player, card);
            
        }

        private static void ExecuteRule(Player player, ICard card)
        {
            Console.WriteLine(card.GetCardName() + " is " + card.SpecialRule + "!");
            switch (card.SpecialRule)
            {
                case "the Bomb":
                    Console.WriteLine("Discard hand, and receive 4 new cards.");
                    player.DiscardHand();
                    for (int i = 0; i < 4; i++)
                    {
                        Dealer.DealTopCard(player);
                    }
                    break;
                case "the Vulture":
                    Console.WriteLine("Hand size increased by 1, receive an extra card.");
                    player.SetMaxHandSize(player.GetMaxHandSize()+1);
                    Dealer.DealTopCard(player);
                    break;
                case "the Quarantine":
                    Console.WriteLine("You will not receive a card on you next card request");
                    player.IsQuarantined = true;
                    break;
                case "the Joker":
                    Console.WriteLine("It will serve as all card types");
                    break;
                default:
                    Console.WriteLine("Error: no matching rule name");
                    break;
            }
        }
    }
}
