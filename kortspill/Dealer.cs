using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;


namespace kortspill

{
    internal class Dealer
    {
        public static List<ICard> Deck = new List<ICard>();
        private static readonly object Lock = new object();

        public Dealer()
        {
            // Fill deck with cards
            foreach (Enum type in Enum.GetValues(typeof(CardType)))
            {
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
                Console.WriteLine(card.getCardName());
            }
        }

        public static void DealTopCard(Player player)
        {
            lock (Lock)
            {
                player.GetHand().Add(Deck[0]);
                Console.WriteLine(player.GetName() + " drew " + Deck[0].getCardName());
                Deck.RemoveAt(0);
                
            }
        }
    }
}

