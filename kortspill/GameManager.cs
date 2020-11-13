using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
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

            // Make 4 random cards remaining in the deck special
            MakeSpecialCards();

            Console.WriteLine("|-----------------------------------|");
            Console.WriteLine("| The Game will begin in 3 seconds! |");
            Console.WriteLine("|-----------------------------------|");
            Console.WriteLine();
            Thread.Sleep(3000);

            CreateThreads(numberOfPlayers);
            
            StartThreads(numberOfPlayers);
        }

        private void MakeSpecialCards()
        {
            Console.WriteLine("|-------------------------------------|");
            Console.WriteLine("| Cards with special rules this game: |");
            Console.WriteLine("|-------------------------------------|");

            string[] rules = {"the Bomb", "the Quarantine", "the Joker", "the Vulture"};
            for (var i = 0; i < 4; i++)
            {
                AddRuleToCard(rules[i]);
            }
            
        }

        private static void AddRuleToCard(string rule)
        {
            var card = Dealer.ReturnRandomCardFromDeck();
            card.SpecialRule = rule;
            Console.WriteLine("- " + card.GetCardName());
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
            Console.WriteLine("|--------------------------------------------------|");
            Console.WriteLine("| Welcome to 4 of a Kind - Thread Battle Card Rush |");
            Console.WriteLine("|--------------------------------------------------|");
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
            Console.WriteLine("|--------------------|");
            Console.WriteLine("| Players this game: |");
            Console.WriteLine("|--------------------|");
            //Create players
            for (var i = 0; i < num; i++)
            {
                Players.Add(PlayerFactory.CreatePlayer());
                Console.WriteLine("- " + Players[i].GetName());
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
            Console.WriteLine("\n"+ player.GetName() + " reads rules: ");
            Console.WriteLine("This card is " + card.SpecialRule + "!");
            switch (card.SpecialRule)
            {
                case "the Bomb":
                    Console.WriteLine("Discard all cards, and receive 4 new cards.\n");
                    
                    player.DiscardHand();
                    for (int i = 0; i < 4; i++)
                    {
                        Dealer.DealTopCard(player);
                    }
                    break;
                case "the Vulture":
                    Console.WriteLine("Hand size increased by 1, receive an extra card.\n");
                    player.SetMaxHandSize(player.GetMaxHandSize()+1);
                    Dealer.DealTopCard(player);
                    break;
                case "the Quarantine":
                    Console.WriteLine("You will not receive a card on you next card request\n");
                    player.IsQuarantined = true;
                    break;
                case "the Joker":
                    Console.WriteLine("It will serve as all card types\n");
                    break;
                default:
                    Console.WriteLine("Error: no matching rule name");
                    break;
            }
        }

        public static void CheckIfWinner(Player player)
        {
            if (HasWinningHand(player))
            {
                player.Winner = true;
                GameManager.EndGame();
            }
        }

        public static bool HasWinningHand(Player player)
        {
            int sameSuitNeeded = 4;
            foreach (var card in player.GetHand().Where(card => card.SpecialRule == "the Joker")) sameSuitNeeded--;

            return player.Count(CardType.Spades) >= sameSuitNeeded || player.Count(CardType.Diamonds) >= sameSuitNeeded || player.Count(CardType.Hearts) >= sameSuitNeeded ||
                   player.Count(CardType.Clubs) >= sameSuitNeeded;
        }
    }
}
