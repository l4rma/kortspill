using System;
using System.Collections.Generic;
using System.Text;

namespace kortspill
{
    internal class CardFactory
    {
        public static ICard CreateCard(CardValue value, CardType type)
        {
            ICard card;
            switch (type)
            {
                case CardType.Spades:
                    card = new SpadesDecorator(new Card(value));
                    break;
                case CardType.Clubs:
                    card = new ClubsDecorator(new Card(value));
                    break;
                case CardType.Hearts:
                    card = new HeartsDecorator(new Card(value));
                    break;
                case CardType.Diamonds:
                    card = new DiamondsDecorator(new Card(value));
                    break;
                default:
                    card = new Card(value);
                    Console.WriteLine("ERROR: Card has not set type and will be clubs...");
                    break;
            }
            return card;
        }
    }
}
