using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace kortspill
{
    class Deck
    {
        private List<Card> deck = new List<Card>();

        public Deck()
        {
            foreach (Enum type in Enum.GetValues(typeof(CardType)))
            {
                foreach (Enum value in Enum.GetValues(typeof(CardValue)))
                {
                    deck.Add(CardFactory.CreateCard((CardValue) value, (CardType) type));
                }
            }

            deck = Shuffle(deck);
        }

        private List<Card> Shuffle(List<Card> deck)
        {
            Random rnd = new Random();
            int n = deck.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                Card verdi = deck[k];
                deck[k] = deck[n];
                deck[n] = verdi;
            }
            return deck;
            
        }

        public void List()
        {
            foreach (Card card in deck)
            {
                Console.WriteLine(card.getCardName());
            }
        }

        public void DealTopCard(Player player)
        {
            player.hand.Add(deck[0]);
            deck.RemoveAt(0);
        }
    }
}

