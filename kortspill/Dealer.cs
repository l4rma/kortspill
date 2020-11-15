using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;


namespace Kortspill

{
    internal class Dealer
    {
        public static List<ICard> Deck = new List<ICard>();
        private static readonly object Lock = new object();

        public Dealer()
        {
            // Fill deck with cards
            foreach (Suit type in Enum.GetValues(typeof(Suit)))
            {
                if (type == Suit.Joker) continue;
                foreach (Enum value in Enum.GetValues(typeof(Value)))
                {
                    Deck.Add(CardFactory.CreateCard((Value) value, (Suit) type));
                }
            }
            // Mix up the order of the cards in the deck
            Deck = Shuffle(Deck);
        }

        private static List<ICard> Shuffle(List<ICard> deck)
        {
            var rnd = new Random();
            var n = deck.Count;
            while (n > 1)
            {
                n--;
                var k = rnd.Next(n + 1);
                var verdi = deck[k];
                deck[k] = deck[n];
                deck[n] = verdi;
            }
            return deck;
        }

        public static void AcceptCardRequest(IPlayer player)
        {
            lock (Lock)
            {
                
                Thread.Sleep(GameManager.DrawSpeed); // Adjust the draw rhythm
                if (player.IsQuarantined)
                {
                    Console.WriteLine("\n" + player.Name + " requested a card, but is Quarantined!\n" +
                                      "He did not receive a card, but it no longer in Quarantine.\n");
                    player.IsQuarantined = false;
                    return;
                }
                DealTopCard(player);
            }
        }

        public static void DealTopCard(IPlayer player)
        {
            ICard card = Deck[0];                // Select top card
            if (GameManager.GameOver) return;    // Stop other players from getting more cards after someone wins
            player.Hand.Add(card);               // Give card to player
            Deck.RemoveAt(0);               // Remove card from dealer
            Console.WriteLine(player.Name + " received " + card.GetCardName()); // Update "UI"
            GameManager.CheckCard(player, card); // Check if card has a special rule
        }

        public static void DealStartingHands()
        {
            foreach (var player in GameManager.Players)
            {
                AddStartingCardsToPlayerDeck(player, GameManager.NumberOfCardsInStartingHand);
            }
        }

        public static void AddStartingCardsToPlayerDeck(IPlayer player, int n)
        {
            for (int i = 0; i < n; i++)
            {
                player.Hand.Add(Deck[0]);
                Deck.RemoveAt(0);
            }

            ConsoleLog.TextBox(player.Name + " gets " + n + " cards:");
            foreach (var card in player.Hand)
            {
                Console.WriteLine("- " + card.GetCardName());
            }
            Console.WriteLine();
            //Thread.Sleep(2000); Pause for readability
            GameManager.CheckStarterHand(player);
        }

        public static ICard ReturnRandomCardFromDeck()
        {
            var rnd = new Random();
            ICard card = Deck[rnd.Next(Deck.Count)];
            while (card.SpecialRule != null)
            {
                rnd = new Random();
                card = Deck[rnd.Next(Deck.Count)];
            }
            return card;
        }
    }
}

