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

        public static void EndGame()
        {
            GameOver = true;

            ConsoleLog.PrintWinner();
        }

        public void Init() //INFO: Facade Pattern
        {
            var dealer = new Dealer();

            var numberOfPlayers = ReturnNumberOfPlayers();

            CreatePlayers(numberOfPlayers);

            Dealer.DealStartingHands();

            // Make 4 random cards remaining in the deck special
            MakeSpecialCards();

            ConsoleLog.TextBox("The Game will begin in 3 seconds!");

            Thread.Sleep(3000);

            CreateThreads(numberOfPlayers);
            
            StartThreads(numberOfPlayers);
        }

        private void MakeSpecialCards()
        {
            ConsoleLog.TextBox("Cards with special rules this game:");

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
            ConsoleLog.TextBox("Welcome to 4 of a Kind - Thread Battle Card Rush");
            ConsoleLog.TextBox("How many players?");
            var input = Console.ReadLine();

            // Check if user input is valid
            if (!int.TryParse(input, out var num))
            {
                ConsoleLog.TextBox("You must write a number");
                Thread.Sleep(1500);
            }
            else
            {
                if (num > 4 || num < 2)
                {
                    ConsoleLog.TextBox("You must choose between 2-4");
                    Thread.Sleep(1500);
                    return -1;
                }
                return num;
            }

            return -1;
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
            ConsoleLog.TextBox("Players this game:");
            //Create players
            for (var i = 0; i < num; i++)
            {
                Players.Add(PlayerFactory.CreatePlayer());
                Console.WriteLine("- " + Players[i].Name);
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
            Console.WriteLine("\n"+ player.Name + " reads rules: ");
            Console.WriteLine("This card is " + card.SpecialRule + "!");
            switch (card.SpecialRule)
            {
                case "the Bomb":
                    Console.WriteLine("Discard all cards, and receive 4 new cards.");
                    
                    player.DiscardHand();
                    for (int i = 0; i < 4; i++)
                    {
                        Dealer.DealTopCard(player);
                    }
                    break;
                case "the Vulture":
                    Console.WriteLine("Hand size increased by 1, receive an extra card.");
                    player.MaxHandSize++;
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
            Console.WriteLine();
        }

        public static void CheckStarterHand(Player player)
        {
            if (GameManager.HasWinningHand(player))
            {
                Console.WriteLine(player.Name + " was dealt a winning hand,");
                Console.WriteLine("which is not a legal starting hand.");
                player.DiscardHand();
                Dealer.Deal4CardsToPlayer(player);
            }
        }

        public static void CheckIfWinner(Player player)
        {
            if (HasWinningHand(player))
            {
                if (GameOver) return; //TODO: Kan fjernes?

                player.Winner = true;
                
                GameManager.EndGame();
            }
        }

        public static bool HasWinningHand(Player player)
        {
            int sameSuitNeeded = 4;
            foreach (var card in player.Hand.Where(card => card.SpecialRule == "the Joker")) sameSuitNeeded--;

            //TODO: Fix properly
            return player.Count(Suit.Spades) >= sameSuitNeeded || player.Count(Suit.Diamonds) >= sameSuitNeeded || player.Count(Suit.Hearts) >= sameSuitNeeded ||
                   player.Count(Suit.Clubs) >= sameSuitNeeded;
        }
    }
}
