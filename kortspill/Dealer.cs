using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;


namespace kortspill

{
    internal class Dealer
    {
        public static List<ICard> Deck = new List<ICard>();
        private static readonly object Lock = new object();

        public Dealer()
        {
            // Fill deck with cards
            foreach (CardType type in Enum.GetValues(typeof(CardType)))
            {
                if (type == CardType.Joker) continue;
                foreach (Enum value in Enum.GetValues(typeof(CardValue)))
                {
                    Deck.Add(CardFactory.CreateCard((CardValue) value, (CardType) type));
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

        public void List()
        {
            foreach (var card in Deck)
            {
                Console.WriteLine(card.GetCardName());
            }
        }

        public static void AcceptCardRequest(Player player)
        {
            lock (Lock)
            {
                Thread.Sleep(100); // Adjust the draw rhythm
                if (player.IsQuarantined)
                {
                    Console.WriteLine(player.GetName() + " requested a card, but is Quarantined!");
                    Console.WriteLine("He did not receive a card, but it no longer in Quarantine.");
                    player.IsQuarantined = false;
                    return;
                }
                DealTopCard(player);
            }
            
        }

        public static void DealTopCard(Player player)
        {
            ICard card = Deck[0];                // Card to deal
            player.GetHand().Add(card);          // Give card to player
            Deck.RemoveAt(0);               // Remove card from dealer
            Console.WriteLine(player.GetName() + " received " + card.GetCardName()); 
            GameManager.CheckCard(player, card); // Check if card has a special rule
            //GameManager.CheckIfWinner(player);   // Check if player won
        }

        public static void DealStartingHands()
        {
            foreach (Player player in GameManager.GetPlayers())
            {
                Deal4CardsToPlayer(player);
            }
        }

        private static void Deal4CardsToPlayer(Player player)
        {
            for (int i = 0; i < 4; i++)
            {
                player.GetHand().Add(Deck[0]);
                Deck.RemoveAt(0);
            }

            Console.WriteLine(player.GetName() + " gets 4 cards");
            foreach (var card in player.GetHand())
            {
                Console.WriteLine("- " + card.GetCardName());
            }

            CheckStarterHand(player);
        }

        private static void CheckStarterHand(Player player)
        {
            if (GameManager.HasWinningHand(player))
            {
                Console.WriteLine(player.GetName() + " was dealt a winning hand,");
                Console.WriteLine("which is not a legal starting hand.");
                player.DiscardHand();
                Deal4CardsToPlayer(player);
            }
        }

        public static ICard ReturnRandomCardFromDeck()
        {
            var rnd = new Random();
            ICard card = Deck[rnd.Next(GetDeckCount())];
            while (card.SpecialRule != null)
            {
                rnd = new Random();
                card = Deck[rnd.Next(GetDeckCount())];
            }
            return card;
        }

        public static int GetDeckCount()
        {
            return Deck.Count;
        }
    }
}

