using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace kortspill
{
    class Deck
    {
        private ArrayList deck = new ArrayList();

        public Deck()
        {
            foreach (Enum type in Enum.GetValues(typeof(CardType)))
            {
                foreach (Enum value in Enum.GetValues(typeof(CardValue)))
                {
                    deck.Add(CardFactory.CreateCard((CardValue) value, (CardType) type));
                }
            }
        }

        public void list()
        {
            foreach (Card card in deck)
            {
                Console.WriteLine(card.getCardName());
            }
        }
    }
}

